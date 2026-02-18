using System.Diagnostics;
using Mindee.Extraction;
using Mindee.Input;
using Mindee.Parsing.Common;
using Mindee.Product.Invoice;
using Mindee.Product.InvoiceSplitter;

namespace Mindee.IntegrationTests.V1.Extraction
{
    [Trait("Category", "Integration")]
    public class InvoiceSplitterAutoExtractionTest
    {
        private static string PrepareInvoiceReturn(string rstFilePath, Document<InvoiceV4> invoicePrediction)
        {
            var rstRefLines = File.ReadAllText(rstFilePath);
            var parsingVersion = invoicePrediction.Inference.Product.Version;
            var parsingId = invoicePrediction.Id;
            rstRefLines = rstRefLines
                .Replace(TestingUtilities.GetVersion(rstRefLines), parsingVersion)
                .Replace(TestingUtilities.GetId(rstRefLines), parsingId)
                .Replace(TestingUtilities.GetFileName(rstRefLines), invoicePrediction.Filename);
            return rstRefLines;
        }

        [Fact(Timeout = 180000)]
        public async Task GivenAPdf_ShouldExtractInvoicesStrict_MustSucceed()
        {
            var sw = Stopwatch.StartNew();
            var stderr = new StreamWriter(Console.OpenStandardError()) { AutoFlush = true };
            void Log(string message) =>
                stderr.WriteLine($"[InvoiceSplitterAutoExtractionTest +{sw.Elapsed}] {message}");

            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var client = TestingUtilities.GetOrGenerateMindeeClient(apiKey);
            var invoiceSplitterBytes =
                File.ReadAllBytes(Constants.V1ProductDir + "invoice_splitter/default_sample.pdf");
            var invoiceSplitterInputSource = new LocalInputSource(invoiceSplitterBytes, "default_sample.pdf");
            Log("EnqueueAndParseAsync<InvoiceSplitterV1> start");
            var response = await client.EnqueueAndParseAsync<InvoiceSplitterV1>(invoiceSplitterInputSource);
            Log("EnqueueAndParseAsync<InvoiceSplitterV1> done");
            var inference = response.Document.Inference;

            Log("PdfExtractor ctor start");
            var extractor = new PdfExtractor(invoiceSplitterInputSource);
            Log("GetPageCount start");
            Assert.Equal(2, extractor.GetPageCount());
            Log("ExtractInvoices start");
            List<ExtractedPdf> extractedPdfsStrict = extractor.ExtractInvoices(
                inference.Prediction.InvoicePageGroups, false);
            Log("ExtractInvoices done");
            Assert.Equal(2, extractedPdfsStrict.Count);
            Assert.Equal("default_sample_001-001.pdf", extractedPdfsStrict[0].Filename);
            Assert.Equal("default_sample_002-002.pdf", extractedPdfsStrict[1].Filename);

            Log("ParseAsync<InvoiceV4> start");
            var invoice0 =
                await client.ParseAsync<InvoiceV4>(extractedPdfsStrict[0].AsInputSource());
            Log("ParseAsync<InvoiceV4> done");

            var testStringRstInvoice0 = PrepareInvoiceReturn(
                Constants.V1ProductDir + "invoices/response_v4/summary_full_invoice_p1.rst", invoice0.Document);

            var ratio = TestingUtilities.LevenshteinRatio(testStringRstInvoice0, invoice0.Document.ToString());
            Assert.True(ratio >= 0.90);
        }
    }
}
