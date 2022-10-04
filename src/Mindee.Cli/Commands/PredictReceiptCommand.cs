using System.CommandLine.Invocation;
using System.CommandLine;
using Microsoft.Extensions.Logging;
using Mindee.Domain.Parsing;
using System.Text.Json;

namespace Mindee.Cli.Commands
{
    internal class PredictReceiptCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictReceiptCommand()
            : base(name: "receipt", "Invokes the receipt API")
        {
            AddArgument(new Argument<string>("filePath", "The path of the file to parse"));
            AddOption(new Option<bool>("-words", "To get all the words in the current document"));
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly MindeeClient _mindeeClient;

            public string FilePath { get; set; } = null!;
            public bool Words { get; set; } = false;

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
            }
            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict a receipt..");

                var invoicePrediction = await _mindeeClient
                    .LoadDocument(File.OpenRead(FilePath), Path.GetFileName(FilePath))
                    .ParseReceiptAsync(Words);

                _logger.LogInformation("See the associated JSON below :");
                _logger.LogInformation(JsonSerializer.Serialize(invoicePrediction));

                return 0;
            }
        }
    }
}
