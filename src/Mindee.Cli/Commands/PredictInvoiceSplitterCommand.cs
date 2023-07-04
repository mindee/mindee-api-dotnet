using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Mindee.Product.InvoiceSplitter;

namespace Mindee.Cli.Commands
{
    internal class PredictInvoiceSplitterCommand : Command
    {
        public IConsole Console { get; set; } = null!;

        public PredictInvoiceSplitterCommand()
            : base(name: "invoice_splitter", "Invokes the invoice splitter API")
        {
            AddArgument(new Argument<string>("path", "The path of the file to parse"));
            AddOption(new Option<string>(new string[] { "-o", "--output", "output" }, "Choose the displayed result format." +
                "Options values : 'raw' to get result as json, 'summary' to get a prettier format. 'raw' by default."));
            AddOption(new Option<bool>(new string[] { "--async", "withAsync" }, "Choose to enqueue the predict request."));
        }

        public new class Handler : ICommandHandler
        {
            private readonly ILogger<Handler> _logger;
            private readonly MindeeClient _mindeeClient;

            public string Path { get; set; } = null!;
            public bool WithAsync { get; set; } = false;
            public string Output { get; set; } = "raw";

            public Handler(ILogger<Handler> logger, MindeeClient mindeeClient)
            {
                _logger = logger;
                _mindeeClient = mindeeClient;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                _logger.LogInformation("About to predict an invoice splitter..");

                if (WithAsync)
                {
                    var predictEnqueuedResponse = await _mindeeClient
                        .LoadDocument(new FileInfo(Path))
                        .EnqueueAsync<InvoiceSplitterV1>();

                    context.Console.Out.Write(JsonSerializer.Serialize(predictEnqueuedResponse, new JsonSerializerOptions { WriteIndented = true }));

                    Thread.Sleep(5000);

                    var jobResponse = await _mindeeClient.ParseQueuedAsync<InvoiceSplitterV1>(predictEnqueuedResponse.Job.Id);

                    context.Console.Out.Write(JsonSerializer.Serialize(jobResponse, new JsonSerializerOptions { WriteIndented = true }));
                }
                else
                {
                    var response = await _mindeeClient
                        .LoadDocument(new FileInfo(Path))
                        .ParseAsync<InvoiceSplitterV1>();

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
