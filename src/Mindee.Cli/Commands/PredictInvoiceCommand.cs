using System;
using System.Collections.Generic;
using System.CommandLine.Invocation;
using System.CommandLine;
using Microsoft.Extensions.Logging;

namespace Mindee.Cli.Commands
{
    internal class PredictInvoiceCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictInvoiceCommand()
            : base(name: "ots invoice", "Invokes the invoice API")
        {
            this.AddOption(new Option<string>(
                new string[] { "--invoice-key", "-ik" }, "Invoice api key, if not set, will use system property"));
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> logger;

            public Handler(ILogger<Handler> logger)
            {
                this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }
            public Task<int> InvokeAsync(InvocationContext context)
            {
                this.logger.LogInformation("About to insert TodoList into storage");
            }
        }
    }
}
