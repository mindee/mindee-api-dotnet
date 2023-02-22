using Mindee.Parsing.Common;
using Mindee.Parsing.InvoiceSplitter;

namespace Mindee.UnitTests.Parsing.InvoiceSplitter
{
    [Trait("Category", "Invoice Splitter V1")]
    public class InvoiceSplitterV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/invoice_splitter/response_v1/2_invoices_summary.rst");
            Assert.Equal(
                expected,
                response.Document.ToString());
        }

        private static async Task<AsyncPredictResponse<InvoiceSplitterV1Inference>> GetPrediction()
        {
            const string fileName = "Resources/invoice_splitter/response_v1/2_invoices_response.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.DocumentQueueGetAsync<InvoiceSplitterV1Inference>("hello");
        }
    }
}
