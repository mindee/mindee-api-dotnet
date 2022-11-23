using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mindee.Parsing.Cropper;

namespace Mindee.Cli.Commands
{
    internal class PredictCropperCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictCropperCommand()
            : base(name: "cropper", "Invokes the cropper API")
        {
            AddArgument(new Argument<string>("path", "The path of the file to parse"));
            AddOption(new Option<string>(new string[] { "-o", "--output", "output" }, "Choose the displayed result format." +
                "Options values : 'raw' to get result as json, 'summary' to get a prettier format. 'raw' by default."));
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly MindeeClient _mindeeClient;

            public string Path { get; set; } = null!;
            public string Output { get; set; } = "raw";

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict use cropper..");

                var invoicePrediction = await _mindeeClient
                    .LoadDocument(new FileInfo(Path))
                    .ParseAsync<CropperV1Prediction>();

                if (Output == "summary")
                {
                    context.Console.Out.Write(invoicePrediction != null ? invoicePrediction.Inference.Prediction.ToString()! : "null");
                }
                else
                {
                    context.Console.Out.Write(JsonSerializer.Serialize(invoicePrediction, new JsonSerializerOptions { WriteIndented = true }));
                }

                return 0;
            }
        }
    }
}
