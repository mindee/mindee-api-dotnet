using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mindee.Parsing;

namespace Mindee.Cli.Commands
{
    internal class PredictCustomCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictCustomCommand()
            : base(name: "custom", "Invokes a builder API")
        {
            AddOption(new Option<string>(
                new string[] { "-o", "--output" },
                "Specify how to output the data. \n" +
                "- summary: a basic summary (default)\n" +
                "- raw: full JSON object\n"));
            AddOption(new Option<string>(
                new string[] { "-v", "--version" }, "Version of the custom API, default 1"));
            AddArgument(new Argument<string>("account", "The path of the file to parse"));
            AddArgument(new Argument<string>("endpoint", "The path of the file to parse"));
            AddArgument(new Argument<string>("path", "The path of the file to parse"));
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly MindeeClient _mindeeClient;

            public string Path { get; set; } = null!;
            public string Endpoint { get; set; } = null!;
            public string Account { get; set; } = null!;
            public string Version { get; set; } = "1";
            public string Output { get; set; } = "summary";

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict a custom document..");

                var prediction = await _mindeeClient
                    .LoadDocument(new FileInfo(Path))
                    .ParseAsync(new CustomEndpoint(
                        Endpoint,
                        Account,
                        Version));

                if (Output == "summary")
                {
                    context.Console.Out.Write(prediction != null ? prediction.Inference.DocumentPrediction.ToString()! : "null");
                }
                else
                {
                    context.Console.Out.Write(JsonSerializer.Serialize(prediction, new JsonSerializerOptions { WriteIndented = true }));
                }

                return 0;
            }
        }
    }
}
