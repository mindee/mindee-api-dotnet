using Microsoft.Extensions.DependencyInjection;
using Mindee.DependencyInjection;
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
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMindeeApi(options =>
            {
                options.ApiKey = apiKey;
            }, true);
            var client = TestingUtilities.GetOrGenerateMindeeClient(apiKey);
            var invoiceSplitterBytes = File.ReadAllBytes("Resources/products/invoice_splitter/default_sample.pdf");
            var invoiceSplitterInputSource = new LocalInputSource(invoiceSplitterBytes, "default_sample.pdf");
            var response = await client.EnqueueAndParseAsync<InvoiceSplitterV1>(invoiceSplitterInputSource);
            InvoiceSplitterV1 inference = response.Document.Inference;

            PdfExtractor extractor = new PdfExtractor(invoiceSplitterInputSource);
            Assert.Equal(2, extractor.GetPageCount());
            List<ExtractedPdf> extractedPdfsStrict = extractor.ExtractInvoices(
                inference.Prediction.PageGroups, false);
            Assert.Equal(2, extractedPdfsStrict.Count);
            Assert.Equal("default_sample_001-001.pdf", extractedPdfsStrict[0].Filename);
            Assert.Equal("default_sample_002-002.pdf", extractedPdfsStrict[1].Filename);

            PredictResponse<InvoiceV4> invoice0 =
                await client.ParseAsync<InvoiceV4>(extractedPdfsStrict[0].AsInputSource());

            string testStringRstInvoice0 = PrepareInvoiceReturn(
                "Resources/products/invoices/response_v4/summary_full_invoice_p1.rst",
                invoice0.Document);

            Assert.Equal(testStringRstInvoice0, invoice0.Document.ToString());
        }
    }
}
