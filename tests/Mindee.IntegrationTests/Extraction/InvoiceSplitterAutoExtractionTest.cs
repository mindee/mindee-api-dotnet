using Mindee.Extraction;
using Mindee.Input;
using Mindee.Parsing.Common;
using Mindee.Product.Invoice;
using Mindee.Product.InvoiceSplitter;

namespace Mindee.IntegrationTests.Extraction
{
    [Trait("Category", "Integration tests")]
    public class InvoiceSplitterAutoExtractionTest
    {
        private readonly MindeeClient _client;

        public InvoiceSplitterAutoExtractionTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            _client = new MindeeClient(apiKey);
        }

        private static string PrepareInvoiceReturn(string rstFilePath, Document<InvoiceV4> invoicePrediction)
        {
            string rstRefLines = File.ReadAllText(rstFilePath);
            string parsingVersion = invoicePrediction.Inference.Product.Version;
            string parsingId = invoicePrediction.Id;
            rstRefLines = rstRefLines
                .Replace(TestingUtilities.GetVersion(rstRefLines), parsingVersion)
                .Replace(TestingUtilities.GetId(rstRefLines), parsingId)
                .Replace(TestingUtilities.GetFileName(rstRefLines), invoicePrediction.Filename);
            return rstRefLines;
        }

        [Fact]
        public async Task GivenAPdf_ShouldExtractInvoicesStrict_MustSucceed()
        {
            var invoiceSplitterInputSource = new LocalInputSource(
                "Resources/products/invoice_splitter/default_sample.pdf"
            );
            var response = await _client.EnqueueAndParseAsync<InvoiceSplitterV1>(invoiceSplitterInputSource);
            InvoiceSplitterV1 inference = response.Document.Inference;

            PdfExtractor extractor = new PdfExtractor(invoiceSplitterInputSource);
            Assert.Equal(2, extractor.GetPageCount());
            List<ExtractedPdf> extractedPdfsStrict = extractor.ExtractInvoices(
                inference.Prediction.PageGroups, false);
            Assert.Equal(2, extractedPdfsStrict.Count);
            Assert.Equal("default_sample_001-001.pdf", extractedPdfsStrict[0].Filename);
            Assert.Equal("default_sample_002-002.pdf", extractedPdfsStrict[1].Filename);

            PredictResponse<InvoiceV4> invoice0 =
                await _client.ParseAsync<InvoiceV4>(extractedPdfsStrict[0].AsInputSource());

            string testStringRstInvoice0 = PrepareInvoiceReturn(
                "Resources/products/invoices/response_v4/summary_full_invoice_p1.rst",
                invoice0.Document);

            Assert.Equal(testStringRstInvoice0, invoice0.Document.ToString());
        }
    }
}
