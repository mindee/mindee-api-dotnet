using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mindee.Parsing.Common;

namespace Mindee.Cli
{
    enum OutputType
    {
        Raw,
        Summary,
    }

    enum AsyncType
    {
        Never,
        Only,
        Both
    }

    internal struct CommandOptions
    {
        public readonly string Name;
        public readonly string Description;
        public readonly bool AllWords;
        public readonly AsyncType Async;

        public CommandOptions(string name, string description, bool allWords, AsyncType async)
        {
            this.Name = name;
            this.Description = description;
            this.AllWords = allWords;
            this.Async = async;
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
            switch (options.Async)
            {
                case AsyncType.Both:
                    AddOption(new Option<bool>(new string[]
                        {
                            "--async"
                        },
                        description: "Process the file asynchronously. False by default."));
                    break;
                case AsyncType.Only:
                    var option = new Option<bool>(alias: "async", getDefaultValue: () => true);
                    option.IsHidden = true;
                    AddOption(option);
                    break;
            }
            AddArgument(new Argument<string>("path", "The path of the file to parse"));
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly MindeeClient _mindeeClient;

            public string Path { get; set; } = null!;
            public bool AllWords { get; set; } = false;
            public OutputType Output { get; set; } = OutputType.Summary;
            public bool Async { get; set; } = false;

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
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
                _logger.LogInformation("Synchronous parsing of {} ...", typeof(TInferenceModel).Name);

                var response = await _mindeeClient
                    .LoadDocument(new FileInfo(options.Path))
                    .ParseAsync<TInferenceModel>(withAllWords: AllWords);

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
                    context.Console.Out.Write(JsonSerializer.Serialize(response, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }));
                }

                return 0;
            }

            private async Task<int> EnqueueAndParseAsync(InvocationContext context, ParseOptions options)
            {
                _logger.LogInformation("Asynchronous parsing of {} ...", typeof(TInferenceModel).Name);

                // Don't try to get the document more than this many times
                const int maxRetries = 10;

                // Wait this many seconds between each try
                const int intervalSec = 5;

                var enqueueResponse = await _mindeeClient
                    .LoadDocument(new FileInfo(options.Path))
                    .EnqueueAsync<TInferenceModel>(withAllWords: AllWords);

                string jobId = enqueueResponse.Job.Id;
                _logger.LogInformation("Enqueued with job ID: {}", jobId);

                int retryCount = 1;
                AsyncPredictResponse<TInferenceModel> response;

                while (retryCount < maxRetries + 1)
                {
                    Thread.Sleep(intervalSec * 1000);
                    _logger.LogInformation("Attempting to retrieve: {} of {}", retryCount, maxRetries);

                    response = await _mindeeClient.ParseQueuedAsync<TInferenceModel>(jobId);
                    if (response.Document != null)
                    {
                        if (options.Output == OutputType.Summary)
                        {
                            context.Console.Out.Write(response.Document.Inference.Prediction.ToString());
                        }
                        else
                        {
                            context.Console.Out.Write(JsonSerializer.Serialize(response, new JsonSerializerOptions
                            {
                                WriteIndented = true
                            }));
                        }
                        return 0;
                    }
                    retryCount++;
                }
                return 1;
            }
        }
    }
}
