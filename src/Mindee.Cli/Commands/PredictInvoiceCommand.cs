using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mindee.Parsing.Invoice;

namespace Mindee.Cli.Commands
{
    internal class PredictInvoiceCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictInvoiceCommand()
            : base(name: "invoice", "Invokes the invoice API")
        {
            AddOption(new Option<bool>(new string[] { "-w", "--with-words", "withWords" },
                "To get all the words in the current document. False by default."));
            AddOption(new Option<string>(new string[] { "-o", "--output", "output" },
                "Specify how to output the data. \n" +
                "- summary: a basic summary (default)\n" +
                "- raw: full JSON object\n"));
            AddArgument(new Argument<string>("path", "The path of the file to parse"));
            AddOption(new Option<bool>(new string[] { "--async", "async" }, "Choose to enqueue the predict request."));
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly MindeeClient _mindeeClient;

            public string Path { get; set; } = null!;
            public bool WithWords { get; set; } = false;
            public bool Async { get; set; } = false;
            public string Output { get; set; } = "summary";

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict an invoice.");

                if (Async)
                {
                    var response = await _mindeeClient
                        .LoadDocument(new FileInfo(Path))
                        .EnqueueAsync<InvoiceV4Inference>(WithWords);

                    context.Console.Out.Write(JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));
                }
                else
                {
                    var response = await _mindeeClient
                        .LoadDocument(new FileInfo(Path))
                        .ParseAsync<InvoiceV4Inference>(WithWords);

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
                }

                return 0;
            }
        }
    }
}
