using System.CommandLine.Invocation;
using System.CommandLine;
using Microsoft.Extensions.Logging;
using Mindee.Prediction;

namespace Mindee.Cli.Commands
{
    internal class PredictInvoiceCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictInvoiceCommand()
            : base(name: "invoice", "Invokes the invoice API")
        {
            AddArgument(new Argument<string>("path", "The path of the file to parse"));

            AddOption(new Option<string>(
                new string[] { "--invoice-key", "-ik" }, "Invoice api key, if not set, will use system property")
                );
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly IInvoiceParsing _invoiceParsing;

            public Handler(ILogger<Handler> logger, IInvoiceParsing invoiceParsing)
            {
                _logger = logger;
                _invoiceParsing = invoiceParsing;
            }
            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict an invoice..");

                await _invoiceParsing.ExecuteAsync(Stream.Null, "f");

                return 0;
            }
        }
    }
}
