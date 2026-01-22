using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using Microsoft.Extensions.Hosting;
using Mindee.Cli.Commands;
// ReSharper disable once RedundantUsingDirective
using Mindee.Extensions.DependencyInjection;
using PredictBankAccountDetailsCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Fr.BankAccountDetails.BankAccountDetailsV2,
    Mindee.Product.Fr.BankAccountDetails.BankAccountDetailsV2Document,
    Mindee.Product.Fr.BankAccountDetails.BankAccountDetailsV2Document
>;
using PredictBankCheckCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Us.BankCheck.BankCheckV1,
    Mindee.Product.Us.BankCheck.BankCheckV1Page,
    Mindee.Product.Us.BankCheck.BankCheckV1Document
>;
using PredictBarcodeReaderCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.BarcodeReader.BarcodeReaderV1,
    Mindee.Product.BarcodeReader.BarcodeReaderV1Document,
    Mindee.Product.BarcodeReader.BarcodeReaderV1Document
>;
using PredictCarteGriseCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Fr.CarteGrise.CarteGriseV1,
    Mindee.Product.Fr.CarteGrise.CarteGriseV1Document,
    Mindee.Product.Fr.CarteGrise.CarteGriseV1Document
>;
using PredictCropperCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Cropper.CropperV1,
    Mindee.Product.Cropper.CropperV1Page,
    Mindee.Product.Cropper.CropperV1Document
>;
using PredictFinancialDocumentCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.FinancialDocument.FinancialDocumentV1,
    Mindee.Product.FinancialDocument.FinancialDocumentV1Document,
    Mindee.Product.FinancialDocument.FinancialDocumentV1Document
>;
using PredictHealthCardCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Fr.HealthCard.HealthCardV1,
    Mindee.Product.Fr.HealthCard.HealthCardV1Document,
    Mindee.Product.Fr.HealthCard.HealthCardV1Document
>;
using PredictIdCardCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Fr.IdCard.IdCardV2,
    Mindee.Product.Fr.IdCard.IdCardV2Page,
    Mindee.Product.Fr.IdCard.IdCardV2Document
>;
using PredictInternationalIdCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.InternationalId.InternationalIdV2,
    Mindee.Product.InternationalId.InternationalIdV2Document,
    Mindee.Product.InternationalId.InternationalIdV2Document
>;
using PredictInvoiceCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Invoice.InvoiceV4,
    Mindee.Product.Invoice.InvoiceV4Document,
    Mindee.Product.Invoice.InvoiceV4Document
>;
using PredictInvoiceSplitterCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.InvoiceSplitter.InvoiceSplitterV1,
    Mindee.Product.InvoiceSplitter.InvoiceSplitterV1Document,
    Mindee.Product.InvoiceSplitter.InvoiceSplitterV1Document
>;
using PredictMultiReceiptsDetectorCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.MultiReceiptsDetector.MultiReceiptsDetectorV1,
    Mindee.Product.MultiReceiptsDetector.MultiReceiptsDetectorV1Document,
    Mindee.Product.MultiReceiptsDetector.MultiReceiptsDetectorV1Document
>;
using PredictPassportCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Passport.PassportV1,
    Mindee.Product.Passport.PassportV1Document,
    Mindee.Product.Passport.PassportV1Document
>;
using PredictPayslipCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Fr.Payslip.PayslipV3,
    Mindee.Product.Fr.Payslip.PayslipV3Document,
    Mindee.Product.Fr.Payslip.PayslipV3Document
>;
using PredictReceiptCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Receipt.ReceiptV5,
    Mindee.Product.Receipt.ReceiptV5Document,
    Mindee.Product.Receipt.ReceiptV5Document
>;

var runner = BuildCommandLine()
    .UseHost(_ => Host.CreateDefaultBuilder(args), builder =>
    {
        builder
            .ConfigureServices((_, services) =>
            {
                services.AddMindeeClient();
                services.AddMindeeClientV2();
            })
            .UseCommandHandler<PredictBarcodeReaderCommand, PredictBarcodeReaderCommand.Handler>()
            .UseCommandHandler<PredictCropperCommand, PredictCropperCommand.Handler>()
            .UseCommandHandler<PredictFinancialDocumentCommand, PredictFinancialDocumentCommand.Handler>()
            .UseCommandHandler<PredictBankAccountDetailsCommand, PredictBankAccountDetailsCommand.Handler>()
            .UseCommandHandler<PredictCarteGriseCommand, PredictCarteGriseCommand.Handler>()
            .UseCommandHandler<PredictHealthCardCommand, PredictHealthCardCommand.Handler>()
            .UseCommandHandler<PredictIdCardCommand, PredictIdCardCommand.Handler>()
            .UseCommandHandler<PredictPayslipCommand, PredictPayslipCommand.Handler>()
            .UseCommandHandler<PredictInternationalIdCommand, PredictInternationalIdCommand.Handler>()
            .UseCommandHandler<PredictInvoiceCommand, PredictInvoiceCommand.Handler>()
            .UseCommandHandler<PredictInvoiceSplitterCommand, PredictInvoiceSplitterCommand.Handler>()
            .UseCommandHandler<PredictMultiReceiptsDetectorCommand, PredictMultiReceiptsDetectorCommand.Handler>()
            .UseCommandHandler<PredictPassportCommand, PredictPassportCommand.Handler>()
            .UseCommandHandler<PredictReceiptCommand, PredictReceiptCommand.Handler>()
            .UseCommandHandler<PredictBankCheckCommand, PredictBankCheckCommand.Handler>()
            ;
    })
    .UseHelp()
    .UseParseErrorReporting()
    .CancelOnProcessTermination()
    .UseEnvironmentVariableDirective()
    .UseParseDirective()
    .UseSuggestDirective()
    .UseTypoCorrections()
    .UseExceptionHandler()
    .Build();

return await runner.InvokeAsync(args);

static CommandLineBuilder BuildCommandLine()
{
    var root = new RootCommand();
    root.AddCommand(new PredictBarcodeReaderCommand(new CommandOptions(
        "barcode-reader", "Barcode Reader",
        false, false, true, false)));
    root.AddCommand(new PredictCropperCommand(new CommandOptions(
        "cropper", "Cropper",
        false, false, true, false)));
    root.AddCommand(new PredictFinancialDocumentCommand(new CommandOptions(
        "financial-document", "Financial Document",
        true, false, true, true)));
    root.AddCommand(new PredictBankAccountDetailsCommand(new CommandOptions(
        "fr-bank-account-details", "FR Bank Account Details",
        false, false, true, false)));
    root.AddCommand(new PredictCarteGriseCommand(new CommandOptions(
        "fr-carte-grise", "FR Carte Grise",
        false, false, true, false)));
    root.AddCommand(new PredictHealthCardCommand(new CommandOptions(
        "fr-health-card", "FR Health Card",
        false, false, false, true)));
    root.AddCommand(new PredictIdCardCommand(new CommandOptions(
        "fr-carte-nationale-d-identite", "FR Carte Nationale d'Identité",
        false, false, true, false)));
    root.AddCommand(new PredictPayslipCommand(new CommandOptions(
        "fr-payslip", "FR Payslip",
        false, false, false, true)));
    root.AddCommand(new PredictInternationalIdCommand(new CommandOptions(
        "international-id", "International ID",
        false, true, false, true)));
    root.AddCommand(new PredictInvoiceCommand(new CommandOptions(
        "invoice", "Invoice",
        true, false, true, true)));
    root.AddCommand(new PredictInvoiceSplitterCommand(new CommandOptions(
        "invoice-splitter", "Invoice Splitter",
        false, false, false, true)));
    root.AddCommand(new PredictMultiReceiptsDetectorCommand(new CommandOptions(
        "multi-receipts-detector", "Multi Receipts Detector",
        false, false, true, false)));
    root.AddCommand(new PredictPassportCommand(new CommandOptions(
        "passport", "Passport",
        false, false, true, false)));
    root.AddCommand(new PredictReceiptCommand(new CommandOptions(
        "receipt", "Receipt",
        true, false, true, true)));
    root.AddCommand(new PredictBankCheckCommand(new CommandOptions(
        "us-bank-check", "US Bank Check",
        false, false, true, false)));

    root.AddGlobalOption(new Option<bool>("--silent", "Disables diagnostics output"));
    root.SetHandler(() =>
    {
        root.InvokeAsync("--help");
    });

    return new CommandLineBuilder(root);
}
