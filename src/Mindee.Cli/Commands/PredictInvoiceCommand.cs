using System.CommandLine.Invocation;
using System.CommandLine;
using Microsoft.Extensions.Logging;
using Mindee.Domain.Parsing;
using System.Text.Json;

namespace Mindee.Cli.Commands
{
    internal class PredictInvoiceCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictInvoiceCommand()
            : base(name: "invoice", "Invokes the invoice API")
        {
            AddArgument(new Argument<string>("filePath", "The path of the file to parse"));
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly MindeeClient _mindeeClient;

            public string FilePath { get; set; } = null!;

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
            }
            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict an invoice..");

                var invoicePrediction = await _mindeeClient
                    .LoadDocument(File.OpenRead(FilePath), Path.GetFileName(FilePath))
                    .ParseInvoiceAsync();

                _logger.LogInformation("See the associated JSON below :");
                _logger.LogInformation(JsonSerializer.Serialize(invoicePrediction));

                return 0;
            }
        }
    }
}
