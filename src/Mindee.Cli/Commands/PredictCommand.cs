using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
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
        public PredictCommand(CommandOptions options)
            : base(options.Name, options.Description)
        {
            AddOption(new Option<OutputType>(["-o", "--output", "output"],
                "Specify how to output the data. \n" +
                "- summary: a basic summary (default)\n" +
                "- raw: full JSON object\n"));
            if (options.AllWords)
            {
                var option = new Option<bool>(["-w", "--all-words", "allWords"],
                    "To get all the words in the current document. False by default.");
                AddOption(option);
            }

            if (options.FullText)
            {
                var option = new Option<bool>(["-f", "--full-text", "fullText"],
                    "To get all the words in the current document. False by default.");
                AddOption(option);
            }

            switch (options.Async)
            {
                case true when !options.Sync:
                {
                    // Inject an "option" not changeable by the user.
                    // This will set the `Handler.Async` property to always be `true`.
                    var option = new Option<bool>("async", () => true) { IsHidden = true };
                    AddOption(option);
                    break;
                }
                case true when options.Sync:
                    AddOption(new Option<bool>(["--async"],
                        "Process the file asynchronously. False by default."));
                    break;
            }

            AddArgument(new Argument<string>("path", "The path of the file to parse"));
        }

        public new class Handler(MindeeClient mindeeClient) : ICommandHandler
        {
            private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

            public string Path { get; set; } = null!;
            public bool AllWords { get; set; } = false;
            public bool FullText { get; set; } = false;
            public OutputType Output { get; set; } = OutputType.Full;
            public bool Async { get; set; } = false;

            public int Invoke(InvocationContext context)
            {
                return InvokeAsync(context).GetAwaiter().GetResult();
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                var options =
                    new ParseOptions(Path, AllWords, FullText, Output);
                if (Async)
                {
                    return await EnqueueAndParseAsync(context, options);
                }

                return await ParseAsync(context, options);
            }

            private async Task<int> ParseAsync(InvocationContext context, ParseOptions options)
            {
                var response = await mindeeClient.ParseAsync<TInferenceModel>(
                    new LocalInputSource(options.Path),
                    new PredictOptions(AllWords, FullText));

                if (response == null)
                {
                    context.Console.Out.Write("null");
                    return 1;
                }

                PrintToConsole(context.Console.Out, options, response);
                return 0;
            }

            private async Task<int> EnqueueAndParseAsync(InvocationContext context, ParseOptions options)
            {
                var response = await mindeeClient.EnqueueAndParseAsync<TInferenceModel>(
                    new LocalInputSource(options.Path),
                    new PredictOptions(AllWords, FullText),
                    null,
                    new AsyncPollingOptions());

                PrintToConsole(context.Console.Out, options, response);
                return 0;
            }

            private void PrintToConsole(
                IStandardStreamWriter console,
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
