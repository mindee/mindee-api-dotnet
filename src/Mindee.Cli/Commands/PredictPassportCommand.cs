using System.CommandLine.Invocation;
using System.CommandLine;
using Microsoft.Extensions.Logging;
using Mindee.Parsing;
using System.Text.Json;

namespace Mindee.Cli.Commands
{
    internal class PredictPassportCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictPassportCommand()
            : base(name: "passport", "Invokes the passport API")
        {
            AddArgument(new Argument<string>("filePath", "The path of the file to parse"));
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
                _logger.LogInformation("About to predict a passport..");

                var prediction = await _documentParser.WithPassportType(File.OpenRead(FilePath), Path.GetFileName(FilePath));

                _logger.LogInformation("See the associated JSON below :");
                _logger.LogInformation(JsonSerializer.Serialize(prediction));

                return 0;
            }
        }
    }
}
