﻿using System.CommandLine.Invocation;
using System.CommandLine;
using Microsoft.Extensions.Logging;
using Mindee.Prediction;
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

            AddOption(new Option<string>(
                new string[] { "--invoice-key", "-ik" }, "Invoice api key, if not set, will use system property")
                );
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly DocumentParser _documentParser;

            public string FilePath { get; set; } = null!;

            public Handler(ILogger<Handler> logger, DocumentParser documentParser)
            {
                _logger = logger;
                _documentParser = documentParser;
            }
            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict an invoice..");

                var invoicePrediction = await _documentParser.FromInvoice(File.OpenRead(FilePath), Path.GetFileName(FilePath));

                _logger.LogInformation("See the associated JSON below :");
                _logger.LogInformation(JsonSerializer.Serialize(invoicePrediction));

                return 0;
            }
        }
    }
}