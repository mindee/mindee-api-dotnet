using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using Microsoft.Extensions.Hosting;
using Mindee.Cli;
using Mindee.Cli.Commands;
using Mindee.Extensions.DependencyInjection;
using PredictBankAccountDetailsCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Fr.BankAccountDetails.BankAccountDetailsV2,
    Mindee.Product.Fr.BankAccountDetails.BankAccountDetailsV2Document,
    Mindee.Product.Fr.BankAccountDetails.BankAccountDetailsV2Document
>;
using PredictBankCheckCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Us.BankCheck.BankCheckV1,
    Mindee.Product.Us.BankCheck.BankCheckV1Page,
    Mindee.Product.Us.BankCheck.BankCheckV1Document
>;
using PredictBarcodeReaderCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.BarcodeReader.BarcodeReaderV1,
    Mindee.Product.BarcodeReader.BarcodeReaderV1Document,
    Mindee.Product.BarcodeReader.BarcodeReaderV1Document
>;
using PredictBillOfLadingCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.BillOfLading.BillOfLadingV1,
    Mindee.Product.BillOfLading.BillOfLadingV1Document,
    Mindee.Product.BillOfLading.BillOfLadingV1Document
>;
using PredictBusinessCardCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.BusinessCard.BusinessCardV1,
    Mindee.Product.BusinessCard.BusinessCardV1Document,
    Mindee.Product.BusinessCard.BusinessCardV1Document
>;
using PredictCarteGriseCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Fr.CarteGrise.CarteGriseV1,
    Mindee.Product.Fr.CarteGrise.CarteGriseV1Document,
    Mindee.Product.Fr.CarteGrise.CarteGriseV1Document
>;
using PredictCropperCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Cropper.CropperV1,
    Mindee.Product.Cropper.CropperV1Page,
    Mindee.Product.Cropper.CropperV1Document
>;
using PredictDeliveryNoteCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.DeliveryNote.DeliveryNoteV1,
    Mindee.Product.DeliveryNote.DeliveryNoteV1Document,
    Mindee.Product.DeliveryNote.DeliveryNoteV1Document
>;
using PredictDriverLicenseCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.DriverLicense.DriverLicenseV1,
    Mindee.Product.DriverLicense.DriverLicenseV1Document,
    Mindee.Product.DriverLicense.DriverLicenseV1Document
>;
using PredictEnergyBillCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Fr.EnergyBill.EnergyBillV1,
    Mindee.Product.Fr.EnergyBill.EnergyBillV1Document,
    Mindee.Product.Fr.EnergyBill.EnergyBillV1Document
>;
using PredictFinancialDocumentCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.FinancialDocument.FinancialDocumentV1,
    Mindee.Product.FinancialDocument.FinancialDocumentV1Document,
    Mindee.Product.FinancialDocument.FinancialDocumentV1Document
>;
using PredictHealthCardCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Fr.HealthCard.HealthCardV1,
    Mindee.Product.Fr.HealthCard.HealthCardV1Document,
    Mindee.Product.Fr.HealthCard.HealthCardV1Document
>;
using PredictHealthcareCardCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Us.HealthcareCard.HealthcareCardV1,
    Mindee.Product.Us.HealthcareCard.HealthcareCardV1Document,
    Mindee.Product.Us.HealthcareCard.HealthcareCardV1Document
>;
using PredictIdCardCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Fr.IdCard.IdCardV2,
    Mindee.Product.Fr.IdCard.IdCardV2Page,
    Mindee.Product.Fr.IdCard.IdCardV2Document
>;
using PredictIndianPassportCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Ind.IndianPassport.IndianPassportV1,
    Mindee.Product.Ind.IndianPassport.IndianPassportV1Document,
    Mindee.Product.Ind.IndianPassport.IndianPassportV1Document
>;
using PredictInternationalIdCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.InternationalId.InternationalIdV2,
    Mindee.Product.InternationalId.InternationalIdV2Document,
    Mindee.Product.InternationalId.InternationalIdV2Document
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
using PredictMultiReceiptsDetectorCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.MultiReceiptsDetector.MultiReceiptsDetectorV1,
    Mindee.Product.MultiReceiptsDetector.MultiReceiptsDetectorV1Document,
    Mindee.Product.MultiReceiptsDetector.MultiReceiptsDetectorV1Document
>;
using PredictNutritionFactsLabelCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.NutritionFactsLabel.NutritionFactsLabelV1,
    Mindee.Product.NutritionFactsLabel.NutritionFactsLabelV1Document,
    Mindee.Product.NutritionFactsLabel.NutritionFactsLabelV1Document
>;
using PredictPassportCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Passport.PassportV1,
    Mindee.Product.Passport.PassportV1Document,
    Mindee.Product.Passport.PassportV1Document
>;
using PredictPayslipCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Fr.Payslip.PayslipV3,
    Mindee.Product.Fr.Payslip.PayslipV3Document,
    Mindee.Product.Fr.Payslip.PayslipV3Document
>;
using PredictReceiptCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Receipt.ReceiptV5,
    Mindee.Product.Receipt.ReceiptV5Document,
    Mindee.Product.Receipt.ReceiptV5Document
>;
using PredictResumeCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Resume.ResumeV1,
    Mindee.Product.Resume.ResumeV1Document,
    Mindee.Product.Resume.ResumeV1Document
>;
using PredictUsMailCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Us.UsMail.UsMailV3,
    Mindee.Product.Us.UsMail.UsMailV3Document,
    Mindee.Product.Us.UsMail.UsMailV3Document
>;

var runner = BuildCommandLine()
    .UseHost(_ => Host.CreateDefaultBuilder(args), (builder) =>
    {
        builder
            .ConfigureServices((hostContext, services) =>
            {
                services.AddMindeeClient();
            })
            .UseCommandHandler<PredictBarcodeReaderCommand, PredictBarcodeReaderCommand.Handler>()
            .UseCommandHandler<PredictBillOfLadingCommand, PredictBillOfLadingCommand.Handler>()
            .UseCommandHandler<PredictBusinessCardCommand, PredictBusinessCardCommand.Handler>()
            .UseCommandHandler<PredictCropperCommand, PredictCropperCommand.Handler>()
            .UseCommandHandler<PredictDeliveryNoteCommand, PredictDeliveryNoteCommand.Handler>()
            .UseCommandHandler<PredictDriverLicenseCommand, PredictDriverLicenseCommand.Handler>()
            .UseCommandHandler<PredictFinancialDocumentCommand, PredictFinancialDocumentCommand.Handler>()
            .UseCommandHandler<PredictBankAccountDetailsCommand, PredictBankAccountDetailsCommand.Handler>()
            .UseCommandHandler<PredictCarteGriseCommand, PredictCarteGriseCommand.Handler>()
            .UseCommandHandler<PredictEnergyBillCommand, PredictEnergyBillCommand.Handler>()
            .UseCommandHandler<PredictHealthCardCommand, PredictHealthCardCommand.Handler>()
            .UseCommandHandler<PredictIdCardCommand, PredictIdCardCommand.Handler>()
            .UseCommandHandler<PredictPayslipCommand, PredictPayslipCommand.Handler>()
            .UseCommandHandler<PredictIndianPassportCommand, PredictIndianPassportCommand.Handler>()
            .UseCommandHandler<PredictInternationalIdCommand, PredictInternationalIdCommand.Handler>()
            .UseCommandHandler<PredictInvoiceCommand, PredictInvoiceCommand.Handler>()
            .UseCommandHandler<PredictInvoiceSplitterCommand, PredictInvoiceSplitterCommand.Handler>()
            .UseCommandHandler<PredictMultiReceiptsDetectorCommand, PredictMultiReceiptsDetectorCommand.Handler>()
            .UseCommandHandler<PredictNutritionFactsLabelCommand, PredictNutritionFactsLabelCommand.Handler>()
            .UseCommandHandler<PredictPassportCommand, PredictPassportCommand.Handler>()
            .UseCommandHandler<PredictReceiptCommand, PredictReceiptCommand.Handler>()
            .UseCommandHandler<PredictResumeCommand, PredictResumeCommand.Handler>()
            .UseCommandHandler<PredictBankCheckCommand, PredictBankCheckCommand.Handler>()
            .UseCommandHandler<PredictHealthcareCardCommand, PredictHealthcareCardCommand.Handler>()
            .UseCommandHandler<PredictUsMailCommand, PredictUsMailCommand.Handler>()
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
    root.AddCommand(new PredictCustomCommand());
    root.AddCommand(new PredictBarcodeReaderCommand(new CommandOptions(
        name: "barcode-reader", description: "Barcode Reader",
        allWords: false, fullText: false, sync: true, async: false)));
    root.AddCommand(new PredictBillOfLadingCommand(new CommandOptions(
        name: "bill-of-lading", description: "Bill of Lading",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictBusinessCardCommand(new CommandOptions(
        name: "business-card", description: "Business Card",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictCropperCommand(new CommandOptions(
        name: "cropper", description: "Cropper",
        allWords: false, fullText: false, sync: true, async: false)));
    root.AddCommand(new PredictDeliveryNoteCommand(new CommandOptions(
        name: "delivery-note", description: "Delivery note",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictDriverLicenseCommand(new CommandOptions(
        name: "driver-license", description: "Driver License",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictFinancialDocumentCommand(new CommandOptions(
        name: "financial-document", description: "Financial Document",
        allWords: true, fullText: false, sync: true, async: true)));
    root.AddCommand(new PredictBankAccountDetailsCommand(new CommandOptions(
        name: "fr-bank-account-details", description: "FR Bank Account Details",
        allWords: false, fullText: false, sync: true, async: false)));
    root.AddCommand(new PredictCarteGriseCommand(new CommandOptions(
        name: "fr-carte-grise", description: "FR Carte Grise",
        allWords: false, fullText: false, sync: true, async: false)));
    root.AddCommand(new PredictEnergyBillCommand(new CommandOptions(
        name: "fr-energy-bill", description: "FR Energy Bill",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictHealthCardCommand(new CommandOptions(
        name: "fr-health-card", description: "FR Health Card",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictIdCardCommand(new CommandOptions(
        name: "fr-carte-nationale-d-identite", description: "FR Carte Nationale d'Identité",
        allWords: false, fullText: false, sync: true, async: false)));
    root.AddCommand(new PredictPayslipCommand(new CommandOptions(
        name: "fr-payslip", description: "FR Payslip",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictIndianPassportCommand(new CommandOptions(
        name: "ind-passport-india", description: "IND Passport - India",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictInternationalIdCommand(new CommandOptions(
        name: "international-id", description: "International ID",
        allWords: false, fullText: true, sync: false, async: true)));
    root.AddCommand(new PredictInvoiceCommand(new CommandOptions(
        name: "invoice", description: "Invoice",
        allWords: true, fullText: false, sync: true, async: true)));
    root.AddCommand(new PredictInvoiceSplitterCommand(new CommandOptions(
        name: "invoice-splitter", description: "Invoice Splitter",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictMultiReceiptsDetectorCommand(new CommandOptions(
        name: "multi-receipts-detector", description: "Multi Receipts Detector",
        allWords: false, fullText: false, sync: true, async: false)));
    root.AddCommand(new PredictNutritionFactsLabelCommand(new CommandOptions(
        name: "nutrition-facts-label", description: "Nutrition Facts Label",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictPassportCommand(new CommandOptions(
        name: "passport", description: "Passport",
        allWords: false, fullText: false, sync: true, async: false)));
    root.AddCommand(new PredictReceiptCommand(new CommandOptions(
        name: "receipt", description: "Receipt",
        allWords: true, fullText: false, sync: true, async: true)));
    root.AddCommand(new PredictResumeCommand(new CommandOptions(
        name: "resume", description: "Resume",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictBankCheckCommand(new CommandOptions(
        name: "us-bank-check", description: "US Bank Check",
        allWords: false, fullText: false, sync: true, async: false)));
    root.AddCommand(new PredictHealthcareCardCommand(new CommandOptions(
        name: "us-healthcare-card", description: "US Healthcare Card",
        allWords: false, fullText: false, sync: false, async: true)));
    root.AddCommand(new PredictUsMailCommand(new CommandOptions(
        name: "us-us-mail", description: "US US Mail",
        allWords: false, fullText: true, sync: false, async: true)));

    root.AddGlobalOption(new Option<bool>(name: "--silent", "Disables diagnostics output"));
    root.SetHandler(handle: () =>
    {
        root.InvokeAsync(commandLine: "--help");
    });

    return new CommandLineBuilder(root);
}
