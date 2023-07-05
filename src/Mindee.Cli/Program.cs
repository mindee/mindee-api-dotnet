using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using Microsoft.Extensions.Hosting;
using Mindee.Cli;
using Mindee.Cli.Commands;
using Mindee.Extensions.DependencyInjection;
using PredictCropperCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Cropper.CropperV1,
    Mindee.Product.Cropper.CropperV1Document,
    Mindee.Product.Cropper.CropperV1Document
>;
using PredictFinancialDocumentCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.FinancialDocument.FinancialDocumentV1,
    Mindee.Product.FinancialDocument.FinancialDocumentV1Document,
    Mindee.Product.FinancialDocument.FinancialDocumentV1Document
>;
using PredictFrIdCardCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Fr.IdCard.IdCardV1,
    Mindee.Product.Fr.IdCard.IdCardV1Page,
    Mindee.Product.Fr.IdCard.IdCardV1Document
>;
using PredictInvoiceCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Invoice.InvoiceV4,
    Mindee.Product.Invoice.InvoiceV4Document,
    Mindee.Product.Invoice.InvoiceV4Document
>;
using PredictInvoiceSplitterCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.InvoiceSplitter.InvoiceSplitterV1,
    Mindee.Product.InvoiceSplitter.InvoiceSplitterV1Document,
    Mindee.Product.InvoiceSplitter.InvoiceSplitterV1Document
>;
using PredictPassportCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Passport.PassportV1,
    Mindee.Product.Passport.PassportV1Document,
    Mindee.Product.Passport.PassportV1Document
>;
using PredictReceiptCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Receipt.ReceiptV4,
    Mindee.Product.Receipt.ReceiptV4Document,
    Mindee.Product.Receipt.ReceiptV4Document
>;

var runner = BuildCommandLine()
    .UseHost(_ => Host.CreateDefaultBuilder(args), (builder) =>
    {
        builder
            .ConfigureServices((hostContext, services) =>
            {
                services.AddMindeeClient();
            })
            .UseCommandHandler<PredictInvoiceCommand, PredictInvoiceCommand.Handler>()
            .UseCommandHandler<PredictFinancialDocumentCommand, PredictFinancialDocumentCommand.Handler>()
            .UseCommandHandler<PredictReceiptCommand, PredictReceiptCommand.Handler>()
            .UseCommandHandler<PredictPassportCommand, PredictPassportCommand.Handler>()
            .UseCommandHandler<PredictCropperCommand, PredictCropperCommand.Handler>()
            .UseCommandHandler<PredictCustomCommand, PredictCustomCommand.Handler>()
            .UseCommandHandler<PredictInvoiceSplitterCommand, PredictInvoiceSplitterCommand.Handler>()
            .UseCommandHandler<PredictFrIdCardCommand, PredictFrIdCardCommand.Handler>()
            ;
    })
    .UseDefaults().Build();

return await runner.InvokeAsync(args);

static CommandLineBuilder BuildCommandLine()
{
    var root = new RootCommand();
    root.AddCommand(new PredictCustomCommand());
    root.AddCommand(new PredictInvoiceCommand(new CommandOptions(
        name: "invoice", description: "Invoice V4",
        allWords: true, async: AsyncType.Never
        )));
    root.AddCommand(new PredictFinancialDocumentCommand(new CommandOptions(
        name: "financial-document", description: "Financial Document V1",
        allWords: true, async: AsyncType.Never
        )));
    root.AddCommand(new PredictReceiptCommand(new CommandOptions(
        name: "receipt", description: "Receipt V4",
        allWords: true, async: AsyncType.Never
        )));
    root.AddCommand(new PredictPassportCommand(new CommandOptions(
        name: "passport", description: "Passport V1",
        allWords: false, async: AsyncType.Never
        )));
    root.AddCommand(new PredictCropperCommand(new CommandOptions(
        name: "cropper", description: "Cropper V1",
        allWords: false, async: AsyncType.Never
        )));
    root.AddCommand(new PredictInvoiceSplitterCommand(new CommandOptions(
        name: "invoice-splitter", description: "Invoice Splitter V1",
        allWords: false, async: AsyncType.Only
        )));
    root.AddCommand(new PredictFrIdCardCommand(new CommandOptions(
        name: "fr-id-card", description: "FR ID Card V1",
        allWords: false, async: AsyncType.Never
        )));

    root.AddGlobalOption(new Option<bool>("--silent", "Disables diagnostics output"));
    root.Handler = CommandHandler.Create(() =>
    {
        root.Invoke("-h");
    });

    return new CommandLineBuilder(root);
}
