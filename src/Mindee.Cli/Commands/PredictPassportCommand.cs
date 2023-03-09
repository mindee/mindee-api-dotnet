using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mindee.Parsing.Passport;

namespace Mindee.Cli.Commands
{
    internal class PredictPassportCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictPassportCommand()
            : base(name: "passport", "Invokes the passport API")
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
            public string Output { get; set; } = "raw";

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict a passport..");

                var prediction = await _mindeeClient
                    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
                    .ParseAsync<PassportV1Inference>();

                if (Output == "summary")
                {
                    context.Console.Out.Write(prediction != null ? prediction.Inference.DocumentPrediction.ToString()! : "null");
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
