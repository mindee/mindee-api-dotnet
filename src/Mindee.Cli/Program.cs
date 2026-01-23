using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mindee;
using Mindee.Cli.Commands;
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

// Setup dependency injection
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddMindeeClient();
        services.AddMindeeClientV2();
    })
    .Build();

var root = BuildCommandLine(host.Services);

return await root.Parse(args).InvokeAsync();

static RootCommand BuildCommandLine(IServiceProvider services)
{
    var root = new RootCommand();
    var mindeeClient = services.GetRequiredService<MindeeClient>();

    var barcodeReaderCmd = new PredictBarcodeReaderCommand(new CommandOptions(
        "barcode-reader", "Barcode Reader",
        false, false, true, false));
    barcodeReaderCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(barcodeReaderCmd);

    var cropperCmd = new PredictCropperCommand(new CommandOptions(
        "cropper", "Cropper",
        false, false, true, false));
    cropperCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(cropperCmd);

    var financialDocumentCmd = new PredictFinancialDocumentCommand(new CommandOptions(
        "financial-document", "Financial Document",
        true, false, true, true));
    financialDocumentCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(financialDocumentCmd);

    var bankAccountDetailsCmd = new PredictBankAccountDetailsCommand(new CommandOptions(
        "fr-bank-account-details", "FR Bank Account Details",
        false, false, true, false));
    bankAccountDetailsCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(bankAccountDetailsCmd);

    var carteGriseCmd = new PredictCarteGriseCommand(new CommandOptions(
        "fr-carte-grise", "FR Carte Grise",
        false, false, true, false));
    carteGriseCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(carteGriseCmd);

    var healthCardCmd = new PredictHealthCardCommand(new CommandOptions(
        "fr-health-card", "FR Health Card",
        false, false, false, true));
    healthCardCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(healthCardCmd);

    var idCardCmd = new PredictIdCardCommand(new CommandOptions(
        "fr-carte-nationale-d-identite", "FR Carte Nationale d'Identité",
        false, false, true, false));
    idCardCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(idCardCmd);

    var payslipCmd = new PredictPayslipCommand(new CommandOptions(
        "fr-payslip", "FR Payslip",
        false, false, false, true));
    payslipCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(payslipCmd);

    var internationalIdCmd = new PredictInternationalIdCommand(new CommandOptions(
        "international-id", "International ID",
        false, true, false, true));
    internationalIdCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(internationalIdCmd);

    var invoiceCmd = new PredictInvoiceCommand(new CommandOptions(
        "invoice", "Invoice",
        true, false, true, true));
    invoiceCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(invoiceCmd);

    var invoiceSplitterCmd = new PredictInvoiceSplitterCommand(new CommandOptions(
        "invoice-splitter", "Invoice Splitter",
        false, false, false, true));
    invoiceSplitterCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(invoiceSplitterCmd);

    var multiReceiptsDetectorCmd = new PredictMultiReceiptsDetectorCommand(new CommandOptions(
        "multi-receipts-detector", "Multi Receipts Detector",
        false, false, true, false));
    multiReceiptsDetectorCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(multiReceiptsDetectorCmd);

    var passportCmd = new PredictPassportCommand(new CommandOptions(
        "passport", "Passport",
        false, false, true, false));
    passportCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(passportCmd);

    var receiptCmd = new PredictReceiptCommand(new CommandOptions(
        "receipt", "Receipt",
        true, false, true, true));
    receiptCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(receiptCmd);

    var bankCheckCmd = new PredictBankCheckCommand(new CommandOptions(
        "us-bank-check", "US Bank Check",
        false, false, true, false));
    bankCheckCmd.ConfigureAction(mindeeClient);
    root.Subcommands.Add(bankCheckCmd);

    var silentOption = new Option<bool>("--silent")
    {
        Description = "Disables diagnostics output"
    };
    root.Options.Add(silentOption);

    root.SetAction(parseResult =>
    {
        Console.WriteLine("Please specify a subcommand. Use --help for more information.");
        return 1;
    });

    return root;
}
