using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mindee;
using Mindee.Extraction;
using Mindee.Input;
using Mindee.Product.InvoiceSplitter;
using Mindee.Product.Invoice;

var apiKey = "my-api-key";
var mindeeClient = new MindeeClient(apiKey);
var myFilePath = "path/to/your/file.ext";

await InvoiceSplitterAutoExtraction(myFilePath);

async Task InvoiceSplitterAutoExtraction(string filePath)
{
    var inputSource = new LocalInputSource(filePath);

    if (inputSource.IsPdf() && inputSource.GetPageCount() > 1)
    {
        await ParseMultiPage(inputSource);
    }
    else
    {
        await ParseSinglePage(inputSource);
    }
}

async Task ParseSinglePage(InputSource inputSource)
{
    var invoiceResult = await mindeeClient.ParseAsync<InvoiceV4>(inputSource);
    Console.WriteLine(invoiceResult.Document);
}

async Task ParseMultiPage(InputSource inputSource)
{
    PdfExtractor extractor = new PdfExtractor(inputSource);
    var invoiceSplitterResponse = await mindeeClient.EnqueueAndParseAsync<InvoiceSplitterV1>(inputSource);
    List<ExtractedPdf> extractedPdfs = extractor.ExtractInvoices(
        invoiceSplitterResponse.Document.Inference.Prediction.PageGroups,
        false
    );

    foreach (var extractedPdf in extractedPdfs)
    {
        // Optional: Save the files locally
        // extractedPdf.WriteToFile("output/path");

        var invoiceResult = await mindeeClient.ParseAsync<InvoiceV4>(extractedPdf.AsInputSource());
        Console.WriteLine(invoiceResult.Document);
    }
}
