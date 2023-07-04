using System.CommandLine;
using System.CommandLine.Invocation;
using Microsoft.Extensions.Logging;
using Mindee.Product.Receipt;

namespace Mindee.Cli.Commands
{
    internal class PredictReceiptCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictReceiptCommand()
            : base(name: "receipt", "Invokes the receipt API")
        {
            AddOption(new Option<bool>(new string[] { "-w", "--with-words", "withWords" },
                "To get all the words in the current document. False by default."));
            AddOption(new Option<string>(new string[] { "-o", "--output", "output" },
                "Specify how to output the data. \n" +
                "- summary: a basic summary (default)\n" +
                "- raw: full JSON object\n"));
            AddArgument(new Argument<string>("path", "The path of the file to parse"));
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly MindeeClient _mindeeClient;

            public string Path { get; set; } = null!;
            public bool WithWords { get; set; } = false;
            public string Output { get; set; } = "raw";

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict a receipt..");

                var response = await _mindeeClient
                    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
                    .ParseAsync<ReceiptV4>(WithWords, true);

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
