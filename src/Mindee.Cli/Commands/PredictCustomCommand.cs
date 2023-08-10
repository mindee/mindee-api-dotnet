using System.CommandLine;
using System.CommandLine.Invocation;
using Microsoft.Extensions.Logging;
using Mindee.Http;
using Mindee.Input;

namespace Mindee.Cli.Commands
{
    internal class PredictCustomCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictCustomCommand()
            : base(name: "custom", "Invokes a builder API")
        {
            AddOption(new Option<OutputType>(
                aliases: new string[]
                {
                    "-o", "--output"
                },
                description: "Specify how to output the data. \n" +
                "- summary: a basic summary (default)\n" +
                "- raw: full JSON object\n"));
            AddOption(new Option<string>(
                aliases: new string[]
                {
                    "-v", "--version"
                },
                description: "Version of the custom API, default 1"));
            AddArgument(new Argument<string>(name: "account", description: "The path of the file to parse"));
            AddArgument(new Argument<string>(name: "endpoint", description: "The path of the file to parse"));
            AddArgument(new Argument<string>(name: "path", description: "The path of the file to parse"));
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

            public int Invoke(InvocationContext context)
            {
                return InvokeAsync(context).GetAwaiter().GetResult();
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict a custom document.");

                var response = await _mindeeClient.ParseAsync(
                    new LocalInputSource(Path),
                    new CustomEndpoint(
                        Endpoint,
                        Account,
                        Version));

                if (response == null)
                {
                    context.Console.Out.Write("null");
                    return 1;
                }

                if (Output == "summary")
                {
                    context.Console.Out.Write(response.Document.Inference.Prediction.ToString());
                }
                else
                {
                    context.Console.Out.Write(response.Document.ToString());
                }

                return 0;
            }
        }
    }
}
