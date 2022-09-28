﻿using System.CommandLine;
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
            var configuration = hostContext.Configuration;
            services.AddInvoiceParsing(configuration);
        })
        .UseCommandHandler<PredictInvoiceCommand, PredictInvoiceCommand.Handler>();
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
        };
        return todolist;
    }
}