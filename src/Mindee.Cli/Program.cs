using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using Microsoft.Extensions.Hosting;
using Mindee.Cli;
using Mindee.Cli.Commands;
using Mindee.Extensions.DependencyInjection;
using PredictCropperCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Cropper.CropperV1,
    Mindee.Product.Cropper.CropperV1Page,
    Mindee.Product.Cropper.CropperV1Document
>;
using PredictDriverLicenseCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.DriverLicense.DriverLicenseV1,
    Mindee.Product.DriverLicense.DriverLicenseV1Document,
    Mindee.Product.DriverLicense.DriverLicenseV1Document
>;
using PredictFinancialDocumentCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.FinancialDocument.FinancialDocumentV1,
    Mindee.Product.FinancialDocument.FinancialDocumentV1Document,
    Mindee.Product.FinancialDocument.FinancialDocumentV1Document
>;
using PredictFrIdCardCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Fr.IdCard.IdCardV2,
    Mindee.Product.Fr.IdCard.IdCardV2Page,
    Mindee.Product.Fr.IdCard.IdCardV2Document
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
using PredictPassportCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Passport.PassportV1,
    Mindee.Product.Passport.PassportV1Document,
    Mindee.Product.Passport.PassportV1Document
>;
using PredictReceiptCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Receipt.ReceiptV5,
    Mindee.Product.Receipt.ReceiptV5Document,
    Mindee.Product.Receipt.ReceiptV5Document
>;
using PredictUsBankCheckCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Us.BankCheck.BankCheckV1,
    Mindee.Product.Us.BankCheck.BankCheckV1Page,
    Mindee.Product.Us.BankCheck.BankCheckV1Document
>;
using PredictUsMailCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Us.UsMail.UsMailV3,
    Mindee.Product.Us.UsMail.UsMailV3Document,
    Mindee.Product.Us.UsMail.UsMailV3Document
>;
using PredictUsPayrollCheckRegisterCommand = Mindee.Cli.PredictCommand<
    Mindee.Product.Us.PayrollCheckRegister.PayrollCheckRegisterV1,
    Mindee.Product.Us.PayrollCheckRegister.PayrollCheckRegisterV1Document,
    Mindee.Product.Us.PayrollCheckRegister.PayrollCheckRegisterV1Document
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
            .UseCommandHandler<PredictDriverLicenseCommand, PredictDriverLicenseCommand.Handler>()
            .UseCommandHandler<PredictReceiptCommand, PredictReceiptCommand.Handler>()
            .UseCommandHandler<PredictPassportCommand, PredictPassportCommand.Handler>()
            .UseCommandHandler<PredictCropperCommand, PredictCropperCommand.Handler>()
            .UseCommandHandler<PredictCustomCommand, PredictCustomCommand.Handler>()
            .UseCommandHandler<PredictInvoiceSplitterCommand, PredictInvoiceSplitterCommand.Handler>()
            .UseCommandHandler<PredictFrIdCardCommand, PredictFrIdCardCommand.Handler>()
            .UseCommandHandler<PredictUsBankCheckCommand, PredictUsBankCheckCommand.Handler>()
            .UseCommandHandler<PredictUsPayrollCheckRegisterCommand, PredictUsPayrollCheckRegisterCommand.Handler>()
            .UseCommandHandler<PredictUsMailCommand, PredictUsMailCommand.Handler>()
            .UseCommandHandler<PredictInternationalIdCommand, PredictInternationalIdCommand.Handler>()
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
    root.AddCommand(new PredictInvoiceCommand(new CommandOptions(
        name: "invoice", description: "Invoice",
        allWords: true, fullText: false, sync: true, async: false
        )));
    root.AddCommand(new PredictFinancialDocumentCommand(new CommandOptions(
        name: "financial-document", description: "Financial Document",
        allWords: true, fullText: false, sync: true, async: false
        )));
    root.AddCommand(new PredictReceiptCommand(new CommandOptions(
        name: "receipt", description: "Receipt",
        allWords: true, fullText: false, sync: true, async: false
        )));
    root.AddCommand(new PredictPassportCommand(new CommandOptions(
        name: "passport", description: "Passport",
        allWords: false, fullText: false, sync: true, async: false
        )));
    root.AddCommand(new PredictInternationalIdCommand(new CommandOptions(
        name: "international-id", description: "International ID",
        allWords: false, fullText: true, sync: false, async: true
        )));
    root.AddCommand(new PredictCropperCommand(new CommandOptions(
        name: "cropper", description: "Cropper",
        allWords: false, fullText: false, sync: true, async: false
        )));
    root.AddCommand(new PredictInvoiceSplitterCommand(new CommandOptions(
        name: "invoice-splitter", description: "Invoice Splitter",
        allWords: false, fullText: false, sync: false, async: true
        )));
    root.AddCommand(new PredictFrIdCardCommand(new CommandOptions(
        name: "fr-id-card", description: "FR ID Card",
        allWords: false, fullText: false, sync: true, async: false
        )));
    root.AddCommand(new PredictUsBankCheckCommand(new CommandOptions(
        name: "us-bank-check", description: "US Bank Check",
        allWords: false, fullText: false, sync: true, async: false
        )));
    root.AddCommand(new PredictDriverLicenseCommand(new CommandOptions(
        name: "driver-license", description: "Driver License",
        allWords: false, fullText: false, sync: false, async: true
        )));
    root.AddCommand(new PredictUsPayrollCheckRegisterCommand(new CommandOptions(
        name: "us-payroll-check-register", description: "US Payroll Check Register",
        allWords: false, fullText: false, sync: false, async: true
        )));
    root.AddCommand(new PredictUsMailCommand(new CommandOptions(
        name: "us-mail", description: "US Mail",
        allWords: false, fullText: true, sync: false, async: true
    )));

    root.AddGlobalOption(new Option<bool>(name: "--silent", "Disables diagnostics output"));
    root.SetHandler(handle: () =>
    {
        root.InvokeAsync(commandLine: "--help");
    });

    return new CommandLineBuilder(root);
}
