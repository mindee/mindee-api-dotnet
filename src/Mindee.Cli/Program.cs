using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using Microsoft.Extensions.Hosting;
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
using PredictBillOfLadingCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.BillOfLading.BillOfLadingV1,
    Mindee.Product.BillOfLading.BillOfLadingV1Document,
    Mindee.Product.BillOfLading.BillOfLadingV1Document
>;
using PredictBusinessCardCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.BusinessCard.BusinessCardV1,
    Mindee.Product.BusinessCard.BusinessCardV1Document,
    Mindee.Product.BusinessCard.BusinessCardV1Document
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
using PredictDeliveryNoteCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.DeliveryNote.DeliveryNoteV1,
    Mindee.Product.DeliveryNote.DeliveryNoteV1Document,
    Mindee.Product.DeliveryNote.DeliveryNoteV1Document
>;
using PredictDriverLicenseCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.DriverLicense.DriverLicenseV1,
    Mindee.Product.DriverLicense.DriverLicenseV1Document,
    Mindee.Product.DriverLicense.DriverLicenseV1Document
>;
using PredictEnergyBillCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Fr.EnergyBill.EnergyBillV1,
    Mindee.Product.Fr.EnergyBill.EnergyBillV1Document,
    Mindee.Product.Fr.EnergyBill.EnergyBillV1Document
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
using PredictHealthcareCardCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Us.HealthcareCard.HealthcareCardV1,
    Mindee.Product.Us.HealthcareCard.HealthcareCardV1Document,
    Mindee.Product.Us.HealthcareCard.HealthcareCardV1Document
>;
using PredictIdCardCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Fr.IdCard.IdCardV2,
    Mindee.Product.Fr.IdCard.IdCardV2Page,
    Mindee.Product.Fr.IdCard.IdCardV2Document
>;
using PredictIndianPassportCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Ind.IndianPassport.IndianPassportV1,
    Mindee.Product.Ind.IndianPassport.IndianPassportV1Document,
    Mindee.Product.Ind.IndianPassport.IndianPassportV1Document
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
using PredictNutritionFactsLabelCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.NutritionFactsLabel.NutritionFactsLabelV1,
    Mindee.Product.NutritionFactsLabel.NutritionFactsLabelV1Document,
    Mindee.Product.NutritionFactsLabel.NutritionFactsLabelV1Document
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
using PredictResumeCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Resume.ResumeV1,
    Mindee.Product.Resume.ResumeV1Document,
    Mindee.Product.Resume.ResumeV1Document
>;
using PredictUsMailCommand = Mindee.Cli.Commands.PredictCommand<
    Mindee.Product.Us.UsMail.UsMailV3,
    Mindee.Product.Us.UsMail.UsMailV3Document,
    Mindee.Product.Us.UsMail.UsMailV3Document
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
        "barcode-reader", "Barcode Reader",
        false, false, true, false)));
    root.AddCommand(new PredictBillOfLadingCommand(new CommandOptions(
        "bill-of-lading", "Bill of Lading",
        false, false, false, true)));
    root.AddCommand(new PredictBusinessCardCommand(new CommandOptions(
        "business-card", "Business Card",
        false, false, false, true)));
    root.AddCommand(new PredictCropperCommand(new CommandOptions(
        "cropper", "Cropper",
        false, false, true, false)));
    root.AddCommand(new PredictDeliveryNoteCommand(new CommandOptions(
        "delivery-note", "Delivery note",
        false, false, false, true)));
    root.AddCommand(new PredictDriverLicenseCommand(new CommandOptions(
        "driver-license", "Driver License",
        false, false, false, true)));
    root.AddCommand(new PredictFinancialDocumentCommand(new CommandOptions(
        "financial-document", "Financial Document",
        true, false, true, true)));
    root.AddCommand(new PredictBankAccountDetailsCommand(new CommandOptions(
        "fr-bank-account-details", "FR Bank Account Details",
        false, false, true, false)));
    root.AddCommand(new PredictCarteGriseCommand(new CommandOptions(
        "fr-carte-grise", "FR Carte Grise",
        false, false, true, false)));
    root.AddCommand(new PredictEnergyBillCommand(new CommandOptions(
        "fr-energy-bill", "FR Energy Bill",
        false, false, false, true)));
    root.AddCommand(new PredictHealthCardCommand(new CommandOptions(
        "fr-health-card", "FR Health Card",
        false, false, false, true)));
    root.AddCommand(new PredictIdCardCommand(new CommandOptions(
        "fr-carte-nationale-d-identite", "FR Carte Nationale d'Identité",
        false, false, true, false)));
    root.AddCommand(new PredictPayslipCommand(new CommandOptions(
        "fr-payslip", "FR Payslip",
        false, false, false, true)));
    root.AddCommand(new PredictIndianPassportCommand(new CommandOptions(
        "ind-passport-india", "IND Passport - India",
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
    root.AddCommand(new PredictNutritionFactsLabelCommand(new CommandOptions(
        "nutrition-facts-label", "Nutrition Facts Label",
        false, false, false, true)));
    root.AddCommand(new PredictPassportCommand(new CommandOptions(
        "passport", "Passport",
        false, false, true, false)));
    root.AddCommand(new PredictReceiptCommand(new CommandOptions(
        "receipt", "Receipt",
        true, false, true, true)));
    root.AddCommand(new PredictResumeCommand(new CommandOptions(
        "resume", "Resume",
        false, false, false, true)));
    root.AddCommand(new PredictBankCheckCommand(new CommandOptions(
        "us-bank-check", "US Bank Check",
        false, false, true, false)));
    root.AddCommand(new PredictHealthcareCardCommand(new CommandOptions(
        "us-healthcare-card", "US Healthcare Card",
        false, false, false, true)));
    root.AddCommand(new PredictUsMailCommand(new CommandOptions(
        "us-us-mail", "US US Mail",
        false, true, false, true)));

    root.AddGlobalOption(new Option<bool>("--silent", "Disables diagnostics output"));
    root.SetHandler(() =>
    {
        root.InvokeAsync("--help");
    });

    return new CommandLineBuilder(root);
}
