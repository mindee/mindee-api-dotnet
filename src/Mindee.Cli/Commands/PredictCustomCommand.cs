using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mindee.Http;
using Mindee.Input;
using Mindee.Parsing.Common;
using Mindee.Product.Generated;

namespace Mindee.Cli.Commands
{
    internal class PredictCustomCommand : Command
    {
        public PredictCustomCommand()
            : base("custom", "Invokes a builder API")
        {
            AddOption(new Option<OutputType>(
                new[] { "-o", "--output" },
                "Specify how to output the data. \n" +
                "- summary: a basic summary (default)\n" +
                "- raw: full JSON object\n"));
            AddOption(new Option<string>(
                new[] { "-v", "--version" },
                "Version of the custom API, default 1"));
            AddOption(new Option<bool>(
                new[] { "--sync" },
                "Whether to use synchronous mode"));
            AddArgument(new Argument<string>("account", "The path of the file to parse"));
            AddArgument(new Argument<string>("endpoint", "The path of the file to parse"));
            AddArgument(new Argument<string>("path", "The path of the file to parse"));
        }

        public IConsole Console { get; set; } = null!;

        public new class Handler : ICommandHandler
        {
            private readonly JsonSerializerOptions _jsonSerializerOptions;
            private readonly ILogger<Handler> _logger;
            private readonly MindeeClient _mindeeClient;

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
                _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
            }

            public string Path { get; set; } = null!;
            public string Endpoint { get; set; } = null!;
            public string Account { get; set; } = null!;
            public string Version { get; set; } = "1";
            public bool Sync { get; set; } = false;
            public OutputType Output { get; set; } = OutputType.Summary;

            public int Invoke(InvocationContext context)
            {
                return InvokeAsync(context).GetAwaiter().GetResult();
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                if (Sync)
                {
                    return await ParseAsync(context);
                }

                return await EnqueueAndParseAsync(context);
            }

            private async Task<int> ParseAsync(InvocationContext context)
            {
                var response = await _mindeeClient.ParseAsync<GeneratedV1>(
                    new LocalInputSource(Path),
                    new CustomEndpoint(
                        Endpoint,
                        Account,
                        Version));

                if (response == null)
                {
                    context.Console.Out.Write("null");
                    return 1;
                }

                PrintToConsole(context.Console.Out, response);
                return 0;
            }

            private async Task<int> EnqueueAndParseAsync(InvocationContext context)
            {
                var response = await _mindeeClient.EnqueueAndParseAsync<GeneratedV1>(
                    new LocalInputSource(Path),
                    new CustomEndpoint(
                        Endpoint,
                        Account,
                        Version),
                    null,
                    null,
                    new AsyncPollingOptions());

                if (response == null)
                {
                    context.Console.Out.Write("null");
                    return 1;
                }

                PrintToConsole(context.Console.Out, response);
                return 0;
            }

            private void PrintToConsole(
                IStandardStreamWriter console,
                PredictResponse<GeneratedV1> response)
            {
                if (Output == OutputType.Raw)
                {
                    console.Write(response.RawResponse + "\n");
                }
                else if (Output == OutputType.Summary)
                {
                    console.Write(response.Document.Inference.Prediction.ToString());
                }
                else
                {
                    console.Write(response.Document.ToString());
                }
            }
        }
    }
}
