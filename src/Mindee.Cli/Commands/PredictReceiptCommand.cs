using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mindee.Parsing.Receipt;

namespace Mindee.Cli.Commands
{
    internal class PredictReceiptCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictReceiptCommand()
            : base(name: "receipt", "Invokes the receipt API")
        {
            AddArgument(new Argument<string>("path", "The path of the file to parse"));
            AddOption(new Option<bool>(new string[] { "-w", "--with-words", "withWords" }, "To get all the words in the current document. False by default."));
            AddOption(new Option<string>(new string[] { "-o", "--output", "output" }, "Choose the displayed result format. " +
                "Options values : 'raw' to get result as json, 'summary' to get a prettier format. 'raw' by default."));
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

                var prediction = await _mindeeClient
                    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
                    .ParseAsync<ReceiptV4Prediction>(WithWords);

                if (Output == "summary")
                {
                    context.Console.Out.Write(prediction != null ? prediction.Inference.Prediction.ToString()! : "null");
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
