using System.CommandLine;
using System.Text.Json;
using Mindee.Input;
using Mindee.V2.Parsing;
using Mindee.V2.Product.Classification;
using Mindee.V2.Product.Classification.Params;
using Mindee.V2.Product.Crop;
using Mindee.V2.Product.Crop.Params;
using Mindee.V2.Product.Extraction;
using Mindee.V2.Product.Extraction.Params;
using Mindee.V2.Product.Ocr;
using Mindee.V2.Product.Ocr.Params;
using Mindee.V2.Product.Split;
using Mindee.V2.Product.Split.Params;
using V2Client = Mindee.V2.Client;

namespace Mindee.Cli.Commands.V2
{
    internal struct InferenceCommandOptions(
        string name,
        string description,
        bool rag,
        bool rawText,
        bool confidence,
        bool polygon,
        bool textContext
    )
    {
        public readonly string Name = name;
        public readonly string Description = description;
        public readonly bool Rag = rag;
        public readonly bool RawText = rawText;
        public readonly bool Confidence = confidence;
        public readonly bool Polygon = polygon;
        public readonly bool TextContext = textContext;
    }

    internal struct InferenceOptions(
        string product,
        string path,
        string modelId,
        string? alias,
        bool rag,
        bool ocr,
        bool confidence,
        bool polygon,
        string? textContext,
        OutputType output)
    {
        public readonly string Path = path;
        public readonly string Product = product;
        public readonly string ModelId = modelId;
        public readonly string? Alias = alias;
        public readonly bool Rag = rag;
        public readonly bool RawText = ocr;
        public readonly bool Confidence = confidence;
        public readonly bool Polygon = polygon;
        public readonly string? TextContext = textContext;
        public readonly OutputType Output = output;
    }

    class InferenceCommand : Command
    {
        private readonly Option<OutputType> _outputOption;
        private readonly string _productName;
        private readonly Option<string?>? _aliasOption;
        private readonly Option<bool>? _ragOption;
        private readonly Option<bool>? _rawTextOption;
        private readonly Option<bool>? _confidenceOption;
        private readonly Option<bool>? _polygonsOption;
        private readonly Option<string?>? _textContextOption;
        private readonly Option<string> _modelIdOption;
        private readonly Argument<string> _pathArgument;

        public InferenceCommand(InferenceCommandOptions options)
            : base(options.Name, options.Description)
        {
            _modelIdOption =
                new Option<string>("--model-id", "-m") { Description = "ID of the model to use", Required = true };
            Options.Add(_modelIdOption);
            var apiKeyOption = new Option<string>("--api-key", "-k") { Description = "Mindee V2 API key." };
            Options.Add(apiKeyOption); // Will not be used at this step, only here for help display purposes.

            _productName = options.Name;
            _aliasOption = new Option<string?>("--alias", "-a")
            {
                Description = "Alias for the file",
                DefaultValueFactory = _ => null
            };
            Options.Add(_aliasOption);

            if (options.Rag)
            {
                _ragOption = new Option<bool>("--rag", "-g")
                {
                    Description = "Enable RAG context. Only valid for 'extraction' product.",
                    DefaultValueFactory = _ => false
                };
                Options.Add(_ragOption);
            }

            _outputOption = new Option<OutputType>("--output", "-o")
            {
                Description = "Specify how to output the data. \n" +
                              "- summary: a basic summary (default)\n" +
                              "- full: detail extraction results, including options\n" +
                              "- raw: full JSON object\n",
                DefaultValueFactory = _ => OutputType.Summary
            };
            Options.Add(_outputOption);

            if (options.RawText)
            {
                _rawTextOption = new Option<bool>("--raw-text", "-r")
                {
                    Description =
                        "To get all the words in the current document. Only supported in some plans. False by default.",
                    DefaultValueFactory = _ => false
                };
                Options.Add(_rawTextOption);
            }

            if (options.Confidence)
            {
                _confidenceOption = new Option<bool>("--confidence", "-c")
                {
                    Description =
                        "To retrieve confidence scores from the extraction. Only supported in some plans. False by default.",
                    DefaultValueFactory = _ => false
                };
                Options.Add(_confidenceOption);
            }

            if (options.Polygon)
            {
                _polygonsOption = new Option<bool>("--polygon", "-p")
                {
                    Description =
                        "To retrieve bounding boxes from the extraction. Only supported in some plans. False by default.",
                    DefaultValueFactory = _ => false
                };
                Options.Add(_polygonsOption);
            }

            if (options.TextContext)
            {
                _textContextOption = new Option<string?>("--text-context", "-t")
                {
                    Description =
                        "To add text context to your API call. Only supported in some plans. False by default.",
                    DefaultValueFactory = _ => null
                };
                Options.Add(_textContextOption);
            }

            _pathArgument = new Argument<string>("path") { Description = "The path of the file to parse" };
            Arguments.Add(_pathArgument);
        }

        public void ConfigureAction(V2Client mindeeClientV2)
        {
            this.SetAction(parseResult =>
            {
                var path = parseResult.GetValue(_pathArgument)!;
                var modelId = parseResult.GetValue(_modelIdOption)!;
                var rag = _ragOption != null && parseResult.GetValue(_ragOption);
                string? alias = null;
                if (_aliasOption != null)
                {
                    alias = parseResult.GetValue(_aliasOption);
                }

                var rawText = _rawTextOption != null && parseResult.GetValue(_rawTextOption);
                var confidence = _confidenceOption != null && parseResult.GetValue(_confidenceOption);
                var polygon = _polygonsOption != null && parseResult.GetValue(_polygonsOption);
                string? textContext = null;
                if (_textContextOption != null)
                {
                    textContext = parseResult.GetValue(_textContextOption);
                }

                var output = parseResult.GetValue(_outputOption);

                var handler = new Handler(mindeeClientV2);
                return handler
                    .InvokeAsync(_productName, modelId, path, alias, rag, rawText, confidence, polygon, textContext,
                        output)
                    .GetAwaiter().GetResult();
            });
        }

        public class Handler(V2Client mindeeClient)
        {
            private readonly JsonSerializerOptions _jsonSerializerOptions = new()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            public async Task<int> InvokeAsync(string product, string modelId, string path, string? alias,
                bool rag, bool rawText, bool confidence, bool polygon, string? textContext, OutputType output)
            {
                var options = new InferenceOptions(product, path, modelId, alias, rag, rawText, confidence, polygon,
                    textContext, output);
                return await EnqueueAndGetResultAsync(options, product);
            }

            private async Task<int> EnqueueAndGetResultAsync(InferenceOptions options, string productName)
            {
                var inputSource = new LocalInputSource(options.Path);
                BaseResponse response = productName switch
                {
                    "classification" => await mindeeClient.EnqueueAndGetResultAsync<ClassificationResponse>(
                        inputSource, new ClassificationParameters(options.ModelId, options.Alias)),
                    "crop" => await mindeeClient.EnqueueAndGetResultAsync<CropResponse>(inputSource,
                        new CropParameters(options.ModelId, options.Alias)),
                    "extraction" => await mindeeClient.EnqueueAndGetResultAsync<ExtractionResponse>(
                        inputSource,
                        new ExtractionParameters(options.ModelId, null, null, options.Rag, options.RawText,
                            options.Polygon, options.Confidence, options.TextContext)),
                    "ocr" => await mindeeClient.EnqueueAndGetResultAsync<OcrResponse>(inputSource,
                        new OcrParameters(options.ModelId, options.Alias)),
                    "split" => await mindeeClient.EnqueueAndGetResultAsync<SplitResponse>(inputSource,
                        new SplitParameters(options.ModelId, options.Alias)),
                    _ => throw new ArgumentOutOfRangeException(nameof(options.Product))
                };

                PrintToConsole(Console.Out, options, response);
                return 0;
            }


            private void PrintToConsole(
                TextWriter console,
                InferenceOptions options,
                BaseResponse response)
            {
                var validTypes = new[]
                {
                    typeof(ClassificationResponse), typeof(CropResponse), typeof(OcrResponse),
                    typeof(SplitResponse), typeof(ExtractionResponse)
                };

                if (!validTypes.Contains(response.GetType()))
                {
                    return;
                }

                dynamic dynResponse = response;

                switch (options.Output)
                {
                    case OutputType.Full:
                        if (options.RawText && dynResponse.Inference.ActiveOptions.RawText)
                        {
                            console.Write("#############\nRaw Text\n#############\n::\n");
                            var rawText = dynResponse.Inference.Result.RawText.ToString().Replace("\n", "\n  ");
                            console.Write("  " + rawText + "\n\n");
                        }
                        if (options.Rag && dynResponse.Inference.ActiveOptions.Rag)
                        {
                            console.Write("#############\nRetrieval-Augmented Generation\n#############\n::\n");
                            var rawText = dynResponse.Inference.Result.Rag.ToString().Replace("\n", "\n  ");
                            console.Write("  " + rawText + "\n\n");
                        }
                        console.Write(dynResponse.Inference.ToString());
                        break;
                    case OutputType.Summary:
                        console.Write(dynResponse.Inference.Result.ToString());
                        break;
                    case OutputType.Raw:
                        using (var jsonDocument = JsonDocument.Parse(response.RawResponse))
                        {
                            console.WriteLine(JsonSerializer.Serialize(jsonDocument, _jsonSerializerOptions));
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Unknown output type: {options.Output}.");
                }
            }
        }
    }
}
