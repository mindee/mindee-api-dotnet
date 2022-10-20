using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mindee.Domain;
using Mindee.Parsing;

namespace Mindee.Cli.Commands
{
    internal class PredictCustomCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictCustomCommand()
            : base(name: "custom", "Invokes a builder API")
        {
            AddArgument(new Argument<string>("path", "The path of the file to parse"));
            AddArgument(new Argument<string>("organizationName", "The name of the organization"));
            AddArgument(new Argument<string>("productName", "The name of the product"));
            AddArgument(new Argument<string>("version", "Version of the custom API builder"));
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly MindeeClient _mindeeClient;

            public string Path { get; set; } = null!;
            public string ProductName { get; set; } = null!;
            public string OrganizationName { get; set; } = null!;
            public string Version { get; set; } = null!;

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
                    .ParseAsync(new Endpoint(
                        ProductName,
                        Version, 
                        OrganizationName));

                context.Console.Out.Write(JsonSerializer.Serialize(prediction, new JsonSerializerOptions { WriteIndented = true }));

                return 0;
            }
        }
    }
}
