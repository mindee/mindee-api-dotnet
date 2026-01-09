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

        [Fact]
        public async Task GivenAPdf_ShouldExtractInvoicesStrict_MustSucceed()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var client = TestingUtilities.GetOrGenerateMindeeClient(apiKey);
            var invoiceSplitterBytes =
                File.ReadAllBytes(Constants.V1ProductDir + "invoice_splitter/default_sample.pdf");
            var invoiceSplitterInputSource = new LocalInputSource(invoiceSplitterBytes, "default_sample.pdf");
            var response = await client.EnqueueAndParseAsync<InvoiceSplitterV1>(invoiceSplitterInputSource);
            var inference = response.Document.Inference;

            var extractor = new PdfExtractor(invoiceSplitterInputSource);
            Assert.Equal(2, extractor.GetPageCount());
            List<ExtractedPdf> extractedPdfsStrict = extractor.ExtractInvoices(
                inference.Prediction.InvoicePageGroups, false);
            Assert.Equal(2, extractedPdfsStrict.Count);
            Assert.Equal("default_sample_001-001.pdf", extractedPdfsStrict[0].Filename);
            Assert.Equal("default_sample_002-002.pdf", extractedPdfsStrict[1].Filename);

            var invoice0 =
                await client.ParseAsync<InvoiceV4>(extractedPdfsStrict[0].AsInputSource());

            var testStringRstInvoice0 = PrepareInvoiceReturn(
                Constants.V1ProductDir + "invoices/response_v4/summary_full_invoice_p1.rst", invoice0.Document);

            var ratio = TestingUtilities.LevenshteinRatio(testStringRstInvoice0, invoice0.Document.ToString());
            Assert.True(ratio >= 0.90);
        }
    }
}
