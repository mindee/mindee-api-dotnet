using DefaultNamespace;
using Mindee.Extraction;
using Mindee.Input;
using Mindee.Parsing.Common;
using Mindee.Product.Invoice;
using Mindee.Product.InvoiceSplitter;

namespace Mindee.IntegrationTests.Extraction
{
    public class InvoiceSplitterAutoExtractionTest
    {
        private static MindeeClient? _client;
        private static LocalInputSource? _invoiceSplitterInputSource;

        public InvoiceSplitterAutoExtractionTest()
        {
            ClientSetUp();
        }

        private static void ClientSetUp()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            _client = new MindeeClient(apiKey);
            _invoiceSplitterInputSource = new LocalInputSource(
                "Resources/products/invoice_splitter/default_sample.pdf"
            );
        }

        private async Task<Document<InvoiceSplitterV1>> GetInvoiceSplitterPredictionAsync()
        {
            // Generate a random artificial delay to avoid hitting 429 errors
            Random r = new Random();
            Thread.Sleep(r.Next(1000, 3000));
            AsyncPredictResponse<InvoiceSplitterV1> response =
                await _client!.EnqueueAndParseAsync<InvoiceSplitterV1>(_invoiceSplitterInputSource);
            return response.Document;
        }

        private async Task<PredictResponse<InvoiceV4>> GetInvoicePredictionAsync(LocalInputSource invoicePDF)
        {
            return await _client!.ParseAsync<InvoiceV4>(invoicePDF);
        }

        private string PrepareInvoiceReturn(string rstFilePath, Document<InvoiceV4> invoicePrediction)
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
        public async Task GivenAPDF_ShouldExtractInvoicesStrict()
        {
            Document<InvoiceSplitterV1> document = await GetInvoiceSplitterPredictionAsync();
            InvoiceSplitterV1 inference = document.Inference;

            PdfExtractor extractor = new PdfExtractor(_invoiceSplitterInputSource);
            Assert.Equal(2, extractor.GetPageCount());
            List<ExtractedPdf> ExtractedPdfsStrict = extractor.ExtractInvoices(
                inference.Prediction.PageGroups, false);
            Assert.Equal(2, ExtractedPdfsStrict.Count);
            Assert.Equal("default_sample_001-001.pdf", ExtractedPdfsStrict[0].Filename);
            Assert.Equal("default_sample_002-002.pdf", ExtractedPdfsStrict[1].Filename);

            PredictResponse<InvoiceV4> invoice0 =
                await GetInvoicePredictionAsync(ExtractedPdfsStrict[0].AsInputSource());

            string testStringRSTInvoice0 = PrepareInvoiceReturn(
                "Resources/products/invoices/response_v4/summary_full_invoice_p1.rst",
                invoice0.Document);

            Assert.Equal(testStringRSTInvoice0, invoice0.Document.ToString());
        }
    }
}
