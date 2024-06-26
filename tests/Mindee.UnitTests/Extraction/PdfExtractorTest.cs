using Mindee.Extraction;
using Mindee.Input;
using Mindee.Parsing.Common;
using Mindee.Product.InvoiceSplitter;
using Xunit.Abstractions;

namespace Mindee.UnitTests.Extraction
{
    public class PdfExtractorTest
    {
        [Fact]
        public async Task GivenAPDF_ShouldExtractInvoicesNoStrict()
        {
            var pdf = new LocalInputSource("Resources/products/invoice_splitter/invoice_5p.pdf");
            var response = await GetPrediction();
            Assert.NotNull(response);
            var inference = response.Document.Inference;
            var extractor = new PdfExtractor(pdf);
            Assert.Equal(5, extractor.GetPageCount());

            var extractedPDFSNoStrict = extractor.ExtractInvoices(inference.Prediction.PageGroups, false);
            Assert.Equal(3, extractedPDFSNoStrict.Count);
            Assert.Equal("invoice_5p_001-001.pdf", extractedPDFSNoStrict[0].Filename);
            Assert.Equal(1, extractedPDFSNoStrict[0].GetPageCount());
            Assert.Equal("invoice_5p_002-004.pdf", extractedPDFSNoStrict[1].Filename);
            Assert.Equal(3, extractedPDFSNoStrict[1].GetPageCount());
            Assert.Equal("invoice_5p_005-005.pdf", extractedPDFSNoStrict[2].Filename);
            Assert.Equal(1, extractedPDFSNoStrict[2].GetPageCount());
        }

        [Fact]
        public async Task GivenAPDF_ShouldExtractInvoicesStrict()
        {
            var pdf = new LocalInputSource("Resources/products/invoice_splitter/invoice_5p.pdf");
            var response = await GetPrediction();
            Assert.NotNull(response);
            var inference = response.Document.Inference;

            var extractor = new PdfExtractor(pdf);
            Assert.Equal(5, extractor.GetPageCount());

            var extractedPDFStrict = extractor.ExtractInvoices(inference.Prediction.PageGroups, true);
            Assert.Equal(2, extractedPDFStrict.Count);
            Assert.Equal("invoice_5p_001-001.pdf", extractedPDFStrict[0].Filename);
            Assert.Equal(1, extractedPDFStrict[0].GetPageCount());
            Assert.Equal("invoice_5p_002-005.pdf", extractedPDFStrict[1].Filename);
            Assert.Equal(4, extractedPDFStrict[1].GetPageCount());
        }
        private static async Task<AsyncPredictResponse<InvoiceSplitterV1>> GetPrediction()
        {
            const string fileName = "Resources/products/invoice_splitter/response_v1/complete.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.DocumentQueueGetAsync<InvoiceSplitterV1>("hello");
        }
    }
}
