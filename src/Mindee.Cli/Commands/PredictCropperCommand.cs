using System.CommandLine;
using System.CommandLine.Invocation;
using Microsoft.Extensions.Logging;
using Mindee.Product.Cropper;

namespace Mindee.Cli.Commands
{
    internal class PredictCropperCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictCropperCommand()
            : base(name: "cropper", "Invokes the cropper API")
        {
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
            public string Output { get; set; } = "summary";

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict use cropper.");

                var response = await _mindeeClient
                    .LoadDocument(new FileInfo(Path))
                    .ParseAsync<CropperV1>();

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
