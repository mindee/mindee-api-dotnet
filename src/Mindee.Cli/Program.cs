using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using Microsoft.Extensions.Hosting;
using Mindee.Cli.Commands;
using Mindee.Extensions.DependencyInjection;

var runner = BuildCommandLine()
    .UseHost(_ => Host.CreateDefaultBuilder(args), (builder) =>
    {
        builder
        .ConfigureServices((hostContext, services) =>
        {
            services.AddMindeeClient();
        })
        .UseCommandHandler<PredictInvoiceCommand, PredictInvoiceCommand.Handler>()
        .UseCommandHandler<PredictReceiptCommand, PredictReceiptCommand.Handler>()
        .UseCommandHandler<PredictPassportCommand, PredictPassportCommand.Handler>()
        .UseCommandHandler<PredictCustomCommand, PredictCustomCommand.Handler>();
    })
    .UseDefaults().Build();

return await runner.InvokeAsync(args);

static CommandLineBuilder BuildCommandLine()
{
    var root = new RootCommand();
    root.AddCommand(new PredictInvoiceCommand());
    root.AddCommand(new PredictReceiptCommand());
    root.AddCommand(new PredictPassportCommand());
    root.AddCommand(new PredictCustomCommand());

    root.AddGlobalOption(new Option<bool>("--silent", "Disables diagnostics output"));
    root.Handler = CommandHandler.Create(() =>
    {
        root.Invoke("-h");
    });

    return new CommandLineBuilder(root);
}
