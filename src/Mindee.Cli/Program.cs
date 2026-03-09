using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mindee.Cli.Commands.V2;
using Mindee.Extensions.DependencyInjection;
using CommandOptions = Mindee.Cli.Commands.V1.CommandOptions;
using PredictBankAccountDetailsCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.Fr.BankAccountDetails.BankAccountDetailsV2,
    Mindee.V1.Product.Fr.BankAccountDetails.BankAccountDetailsV2Document,
    Mindee.V1.Product.Fr.BankAccountDetails.BankAccountDetailsV2Document
>;
using PredictBankCheckCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.Us.BankCheck.BankCheckV1,
    Mindee.V1.Product.Us.BankCheck.BankCheckV1Page,
    Mindee.V1.Product.Us.BankCheck.BankCheckV1Document
>;
using PredictBarcodeReaderCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.BarcodeReader.BarcodeReaderV1,
    Mindee.V1.Product.BarcodeReader.BarcodeReaderV1Document,
    Mindee.V1.Product.BarcodeReader.BarcodeReaderV1Document
>;
using PredictCarteGriseCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.Fr.CarteGrise.CarteGriseV1,
    Mindee.V1.Product.Fr.CarteGrise.CarteGriseV1Document,
    Mindee.V1.Product.Fr.CarteGrise.CarteGriseV1Document
>;
using PredictCropperCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.Cropper.CropperV1,
    Mindee.V1.Product.Cropper.CropperV1Page,
    Mindee.V1.Product.Cropper.CropperV1Document
>;
using PredictFinancialDocumentCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.FinancialDocument.FinancialDocumentV1,
    Mindee.V1.Product.FinancialDocument.FinancialDocumentV1Document,
    Mindee.V1.Product.FinancialDocument.FinancialDocumentV1Document
>;
using PredictHealthCardCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.Fr.HealthCard.HealthCardV1,
    Mindee.V1.Product.Fr.HealthCard.HealthCardV1Document,
    Mindee.V1.Product.Fr.HealthCard.HealthCardV1Document
>;
using PredictIdCardCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.Fr.IdCard.IdCardV2,
    Mindee.V1.Product.Fr.IdCard.IdCardV2Page,
    Mindee.V1.Product.Fr.IdCard.IdCardV2Document
>;
using PredictInternationalIdCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.InternationalId.InternationalIdV2,
    Mindee.V1.Product.InternationalId.InternationalIdV2Document,
    Mindee.V1.Product.InternationalId.InternationalIdV2Document
>;
using PredictInvoiceCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.Invoice.InvoiceV4,
    Mindee.V1.Product.Invoice.InvoiceV4Document,
    Mindee.V1.Product.Invoice.InvoiceV4Document
>;
using PredictInvoiceSplitterCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.InvoiceSplitter.InvoiceSplitterV1,
    Mindee.V1.Product.InvoiceSplitter.InvoiceSplitterV1Document,
    Mindee.V1.Product.InvoiceSplitter.InvoiceSplitterV1Document
>;
using PredictMultiReceiptsDetectorCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.MultiReceiptsDetector.MultiReceiptsDetectorV1,
    Mindee.V1.Product.MultiReceiptsDetector.MultiReceiptsDetectorV1Document,
    Mindee.V1.Product.MultiReceiptsDetector.MultiReceiptsDetectorV1Document
>;
using PredictPassportCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.Passport.PassportV1,
    Mindee.V1.Product.Passport.PassportV1Document,
    Mindee.V1.Product.Passport.PassportV1Document
>;
using PredictPayslipCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.Fr.Payslip.PayslipV3,
    Mindee.V1.Product.Fr.Payslip.PayslipV3Document,
    Mindee.V1.Product.Fr.Payslip.PayslipV3Document
>;
using PredictReceiptCommand = Mindee.Cli.Commands.V1.PredictCommand<
    Mindee.V1.Product.Receipt.ReceiptV5,
    Mindee.V1.Product.Receipt.ReceiptV5Document,
    Mindee.V1.Product.Receipt.ReceiptV5Document
>;
using V1Client = Mindee.V1.Client;
using V2Client = Mindee.V2.Client;

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

static void BuildV1Commands(Command v1Command, V1Client mindeeClient)
{

    var barcodeReaderCmd = new PredictBarcodeReaderCommand(new CommandOptions(
        "barcode-reader", "Barcode Reader",
        false, false, true, false));
    barcodeReaderCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(barcodeReaderCmd);

    var cropperCmd = new PredictCropperCommand(new CommandOptions(
        "cropper", "Cropper",
        false, false, true, false));
    cropperCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(cropperCmd);

    var financialDocumentCmd = new PredictFinancialDocumentCommand(new CommandOptions(
        "financial-document", "Financial Document",
        true, false, true, true));
    financialDocumentCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(financialDocumentCmd);

    var bankAccountDetailsCmd = new PredictBankAccountDetailsCommand(new CommandOptions(
        "fr-bank-account-details", "FR Bank Account Details",
        false, false, true, false));
    bankAccountDetailsCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(bankAccountDetailsCmd);

    var carteGriseCmd = new PredictCarteGriseCommand(new CommandOptions(
        "fr-carte-grise", "FR Carte Grise",
        false, false, true, false));
    carteGriseCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(carteGriseCmd);

    var healthCardCmd = new PredictHealthCardCommand(new CommandOptions(
        "fr-health-card", "FR Health Card",
        false, false, false, true));
    healthCardCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(healthCardCmd);

    var idCardCmd = new PredictIdCardCommand(new CommandOptions(
        "fr-carte-nationale-d-identite", "FR Carte Nationale d'Identité",
        false, false, true, false));
    idCardCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(idCardCmd);

    var payslipCmd = new PredictPayslipCommand(new CommandOptions(
        "fr-payslip", "FR Payslip",
        false, false, false, true));
    payslipCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(payslipCmd);

    var internationalIdCmd = new PredictInternationalIdCommand(new CommandOptions(
        "international-id", "International ID",
        false, true, false, true));
    internationalIdCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(internationalIdCmd);

    var invoiceCmd = new PredictInvoiceCommand(new CommandOptions(
        "invoice", "Invoice",
        true, false, true, true));
    invoiceCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(invoiceCmd);

    var invoiceSplitterCmd = new PredictInvoiceSplitterCommand(new CommandOptions(
        "invoice-splitter", "Invoice Splitter",
        false, false, false, true));
    invoiceSplitterCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(invoiceSplitterCmd);

    var multiReceiptsDetectorCmd = new PredictMultiReceiptsDetectorCommand(new CommandOptions(
        "multi-receipts-detector", "Multi Receipts Detector",
        false, false, true, false));
    multiReceiptsDetectorCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(multiReceiptsDetectorCmd);

    var passportCmd = new PredictPassportCommand(new CommandOptions(
        "passport", "Passport",
        false, false, true, false));
    passportCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(passportCmd);

    var receiptCmd = new PredictReceiptCommand(new CommandOptions(
        "receipt", "Receipt",
        true, false, true, true));
    receiptCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(receiptCmd);

    var bankCheckCmd = new PredictBankCheckCommand(new CommandOptions(
        "us-bank-check", "US Bank Check",
        false, false, true, false));
    bankCheckCmd.ConfigureAction(mindeeClient);
    v1Command.Subcommands.Add(bankCheckCmd);
}

static void BuildV2Commands(Command v2Command, V2Client mindeeClient)
{
    var classificationCmd = new InferenceCommand(new InferenceCommandOptions("classification", "Classification utility.", false, false, false, false, false));
    v2Command.Subcommands.Add(classificationCmd);
    classificationCmd.ConfigureAction(mindeeClient);
    var cropCmd = new InferenceCommand(new InferenceCommandOptions("crop", "Crop utility.", false, false, false, false, false));
    v2Command.Subcommands.Add(cropCmd);
    cropCmd.ConfigureAction(mindeeClient);
    var extractionCmd = new InferenceCommand(new InferenceCommandOptions("extraction",
        "Generic all-purpose extraction.", true, true, true, true, true));
    v2Command.Subcommands.Add(extractionCmd);
    extractionCmd.ConfigureAction(mindeeClient);
    var ocrCmd = new InferenceCommand(new InferenceCommandOptions("ocr", "OCR utility.", false, false, false, false, false));
    v2Command.Subcommands.Add(ocrCmd);
    ocrCmd.ConfigureAction(mindeeClient);
    var splitCmd = new InferenceCommand(new InferenceCommandOptions("split", "Split utility.", false, false, false, false, false));
    v2Command.Subcommands.Add(splitCmd);
    splitCmd.ConfigureAction(mindeeClient);
}

static RootCommand BuildCommandLine(IServiceProvider services)
{
    var root = new RootCommand();
    var v1Command = new Command("v1", "Mindee V1 product commands");
    var mindeeClientV1 = services.GetRequiredService<V1Client>();
    BuildV1Commands(v1Command, mindeeClientV1);
    var v2Command = new Command("v2", "Mindee V2 product commands");
    var mindeeClientV2 = services.GetRequiredService<V2Client>();
    BuildV2Commands(v2Command, mindeeClientV2);

    var silentOption = new Option<bool>("--silent")
    {
        Description = "Disables diagnostics output"
    };
    root.Options.Add(silentOption);

    root.SetAction(_ =>
    {
        Console.WriteLine("Please specify a subcommand. Use --help for more information.");
        return 1;
    });

    return root;
}
