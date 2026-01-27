using System.CommandLine;
using System.Text.Json;
using Mindee.Input;
using Mindee.Parsing;
using Mindee.Parsing.Common;

namespace Mindee.Cli.Commands
{
    internal struct CommandOptions(string name, string description, bool allWords, bool fullText, bool sync, bool async)
    {
        public readonly string Name = name;
        public readonly string Description = description;
        public readonly bool AllWords = allWords;
        public readonly bool FullText = fullText;
        public readonly bool Async = async;
        public readonly bool Sync = sync;
    }

    internal struct ParseOptions(string path, bool allWords, bool fullText, OutputType output)
    {
        public readonly string Path = path;
        public readonly bool AllWords = allWords;
        public readonly bool FullText = fullText;
        public readonly OutputType Output = output;
    }

    class PredictCommand<TInferenceModel, TDoc, TPage> : Command
        where TDoc : IPrediction, new()
        where TPage : IPrediction, new()
        where TInferenceModel : Inference<TDoc, TPage>, new()
    {
        private readonly Option<OutputType> _outputOption;
        private readonly Option<bool>? _allWordsOption;
        private readonly Option<bool>? _fullTextOption;
        private readonly Option<bool>? _asyncOption;
        private readonly Argument<string> _pathArgument;

        public PredictCommand(CommandOptions options)
            : base(options.Name, options.Description)
        {
            _outputOption = new Option<OutputType>("--output", "-o")
            {
                Description = "Specify how to output the data. \n" +
                    "- summary: a basic summary (default)\n" +
                    "- raw: full JSON object\n",
                DefaultValueFactory = _ => OutputType.Summary
            };
            Options.Add(_outputOption);

            if (options.AllWords)
            {
                _allWordsOption = new Option<bool>("--all-words", "-w")
                {
                    Description = "To get all the words in the current document. False by default.",
                    DefaultValueFactory = _ => false
                };
                Options.Add(_allWordsOption);
            }

            if (options.FullText)
            {
                _fullTextOption = new Option<bool>("--full-text", "-f")
                {
                    Description = "To get all the words in the current document. False by default.",
                    DefaultValueFactory = _ => false
                };
                Options.Add(_fullTextOption);
            }

            switch (options.Async)
            {
                case true when !options.Sync:
                    {
                        _asyncOption = new Option<bool>("async")
                        {
                            Hidden = true,
                            DefaultValueFactory = _ => true
                        };
                        Options.Add(_asyncOption);
                        break;
                    }
                case true when options.Sync:
                    _asyncOption = new Option<bool>("--async")
                    {
                        Description = "Process the file asynchronously. False by default.",
                        DefaultValueFactory = _ => false
                    };
                    Options.Add(_asyncOption);
                    break;
            }

            _pathArgument = new Argument<string>("path") { Description = "The path of the file to parse" };
            Arguments.Add(_pathArgument);
        }

        public void ConfigureAction(MindeeClient mindeeClient)
        {
            this.SetAction(parseResult =>
            {
                var path = parseResult.GetValue(_pathArgument)!;
                var allWords = _allWordsOption != null && parseResult.GetValue(_allWordsOption);
                var fullText = _fullTextOption != null && parseResult.GetValue(_fullTextOption);
                var output = parseResult.GetValue(_outputOption);
                var isAsync = _asyncOption != null && parseResult.GetValue(_asyncOption);

                var handler = new Handler(mindeeClient);
                return handler.InvokeAsync(path, allWords, fullText, output, isAsync).GetAwaiter().GetResult();
            });
        }

        public class Handler(MindeeClient mindeeClient)
        {
            private readonly JsonSerializerOptions _jsonSerializerOptions = new()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            public async Task<int> InvokeAsync(string path, bool allWords, bool fullText, OutputType output, bool isAsync)
            {
                var options = new ParseOptions(path, allWords, fullText, output);
                if (isAsync)
                {
                    return await EnqueueAndParseAsync(options);
                }

                return await ParseAsync(options);
            }

            private async Task<int> ParseAsync(ParseOptions options)
            {
                var response = await mindeeClient.ParseAsync<TInferenceModel>(
                    new LocalInputSource(options.Path),
                    new PredictOptions(options.AllWords, options.FullText));

                if (response == null)
                {
                    await Console.Out.WriteAsync("null");
                    return 1;
                }

                PrintToConsole(Console.Out, options, response);
                return 0;
            }

            private async Task<int> EnqueueAndParseAsync(ParseOptions options)
            {
                var response = await mindeeClient.EnqueueAndParseAsync<TInferenceModel>(
                    new LocalInputSource(options.Path),
                    new PredictOptions(options.AllWords, options.FullText),
                    null,
                    new AsyncPollingOptions());

                PrintToConsole(Console.Out, options, response);
                return 0;
            }

            private void PrintToConsole(
                TextWriter console,
                ParseOptions options,
                PredictResponse<TInferenceModel> response)
            {
                if (options.Output == OutputType.Raw)
                {
                    console.Write(JsonSerializer.Serialize(response, _jsonSerializerOptions));
                }
                else
                {
                    if (options.AllWords && response.Document.Ocr != null)
                    {
                        console.Write("#############\nDocument Text\n#############\n::\n");
                        var ocr = response.Document.Ocr.ToString().Replace("\n", "\n  ");
                        console.Write("  " + ocr + "\n\n");
                    }
                    else if (options.FullText && response.Document.Inference.Extras.FullTextOcr != null)
                    {
                        console.Write("#############\nDocument Text\n#############\n::\n");
                        var ocr = response.Document.Inference.Extras.FullTextOcr.Replace("\n", "\n  ");
                        console.Write("  " + ocr + "\n\n");
                    }

                    switch (options.Output)
                    {
                        case OutputType.Full:
                            console.Write(response.Document.ToString());
                            break;
                        case OutputType.Summary:
                            console.Write(response.Document.Inference.Prediction.ToString());
                            break;
                        case OutputType.Raw:
                        default:
                            break;
                    }
                }
            }
        }
    }
}
