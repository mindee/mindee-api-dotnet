using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mindee.Input;
using Mindee.Parsing;
using Mindee.Parsing.Common;

namespace Mindee.Cli
{
    enum OutputType
    {
        Raw,
        Summary,
    }

    internal struct CommandOptions
    {
        public readonly string Name;
        public readonly string Description;
        public readonly bool AllWords;
        public readonly bool Async;
        public readonly bool Sync;

        public CommandOptions(string name, string description, bool allWords, bool sync, bool async)
        {
            this.Name = name;
            this.Description = description;
            this.AllWords = allWords;
            this.Async = async;
            this.Sync = sync;
        }
    }

    internal struct ParseOptions
    {
        public readonly string Path;
        public readonly bool AllWords;
        public readonly OutputType Output;

        public ParseOptions(string path, bool allWords, OutputType output)
        {
            this.Path = path;
            this.AllWords = allWords;
            this.Output = output;
        }
    }

    internal class PredictCommand<TInferenceModel, TDoc, TPage> : Command
        where TDoc : IPrediction, new()
        where TPage : IPrediction, new()
        where TInferenceModel : Inference<TDoc, TPage>, new()
    {
        public PredictCommand(CommandOptions options)
            : base(name: options.Name, description: options.Description)
        {
            AddOption(new Option<OutputType>(new string[]
                {
                    "-o", "--output", "output"
                },
                description: "Specify how to output the data. \n" +
                "- summary: a basic summary (default)\n" +
                "- raw: full JSON object\n"));
            if (options.AllWords)
            {
                var option = new Option<bool>(new string[]
                    {
                        "-w", "--all-words", "allWords"
                    },
                    description: "To get all the words in the current document. False by default.");
                AddOption(option);
            }
            if (options.Async && !options.Sync)
            {
                // Inject an "option" not changeable by the user.
                // This will set the `Handler.Async` property to always be `true`.
                var option = new Option<bool>(name: "async", getDefaultValue: () => true);
                option.IsHidden = true;
                AddOption(option);
            }
            else if (options.Async && options.Sync)
            {
                AddOption(new Option<bool>(new string[]
                    {
                        "--async"
                    },
                    description: "Process the file asynchronously. False by default."));
            }
            AddArgument(new Argument<string>("path", "The path of the file to parse"));
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly MindeeClient _mindeeClient;
            private readonly JsonSerializerOptions _jsonSerializerOptions;

            public string Path { get; set; } = null!;
            public bool AllWords { get; set; } = false;
            public OutputType Output { get; set; } = OutputType.Summary;
            public bool Async { get; set; } = false;

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
                _jsonSerializerOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
            }

            public int Invoke(InvocationContext context)
            {
                return InvokeAsync(context).GetAwaiter().GetResult();
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                ParseOptions options = new ParseOptions(path: Path, allWords: AllWords, output: Output);
                if (Async)
                {
                    return await EnqueueAndParseAsync(context, options);
                }
                return await ParseAsync(context, options);
            }

            private async Task<int> ParseAsync(InvocationContext context, ParseOptions options)
            {
                var response = await _mindeeClient.ParseAsync<TInferenceModel>(
                    new LocalInputSource(options.Path),
                    new PredictOptions(allWords: AllWords));

                if (response == null)
                {
                    context.Console.Out.Write("null");
                    return 1;
                }

                if (options.Output == OutputType.Summary)
                {
                    context.Console.Out.Write(response.Document.Inference.Prediction.ToString());
                }
                else
                {
                    context.Console.Out.Write(JsonSerializer.Serialize(response, _jsonSerializerOptions));
                }
                return 0;
            }

            private async Task<int> EnqueueAndParseAsync(InvocationContext context, ParseOptions options)
            {
                var response = await _mindeeClient.EnqueueAndParseAsync<TInferenceModel>(
                    new LocalInputSource(options.Path),
                    new PredictOptions(allWords: AllWords));

                if (options.Output == OutputType.Summary)
                {
                    context.Console.Out.Write(response.Document.Inference.Prediction.ToString());
                }
                else
                {
                    context.Console.Out.Write(JsonSerializer.Serialize(response, _jsonSerializerOptions));
                }
                return 0;
            }
        }
    }
}
