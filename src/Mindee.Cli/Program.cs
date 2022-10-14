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
        builder.UseEnvironment("CLI")
        .ConfigureServices((hostContext, services) =>
        {
            services.AddMindeeClient();
        })
        .UseCommandHandler<PredictInvoiceCommand, PredictInvoiceCommand.Handler>()
        .UseCommandHandler<PredictReceiptCommand, PredictReceiptCommand.Handler>()
        .UseCommandHandler<PredictPassportCommand, PredictPassportCommand.Handler>();
    })
    .UseDefaults().Build();

return await runner.InvokeAsync(args);

static CommandLineBuilder BuildCommandLine()
{
    var root = new RootCommand();
    root.AddCommand(BuildPredictCommands());

    root.AddGlobalOption(new Option<bool>("--silent", "Disables diagnostics output"));
    root.Handler = CommandHandler.Create(() =>
    {
        root.Invoke("-h");
    });

    return new CommandLineBuilder(root);

    static Command BuildPredictCommands()
    {
        var todolist = new Command("predict-ots", "To predict with ots API")
        {
            new PredictInvoiceCommand(),
            new PredictReceiptCommand(),
            new PredictPassportCommand(),
        };
        return todolist;
    }
}
