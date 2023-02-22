using System.CommandLine;
using System.CommandLine.Invocation;
using Microsoft.Extensions.Logging;
using Mindee.Parsing.Financial;

namespace Mindee.Cli.Commands
{
    internal class PredictFinancialDocumentCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictFinancialDocumentCommand()
            : base(name: "financial-document", "Invokes the Financial Document API")
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
            public string Output { get; set; } = "summary";

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict an Financial Document..");

                var response = await _mindeeClient
                    .LoadDocument(new FileInfo(Path))
                    .ParseAsync<FinancialV1Inference>(WithWords);

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
