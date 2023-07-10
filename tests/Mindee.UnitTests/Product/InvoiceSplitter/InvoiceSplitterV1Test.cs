using Mindee.Parsing.Common;
using Mindee.Product.InvoiceSplitter;

namespace Mindee.UnitTests.Product.InvoiceSplitter
{
    [Trait("Category", "Invoice Splitter V1")]
    public class InvoiceSplitterV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/invoice_splitter/response_v1/summary_full.rst");
            Assert.Equal(
                expected,
                response.Document.ToString());
        }

        private static async Task<AsyncPredictResponse<InvoiceSplitterV1>> GetPrediction()
        {
            const string fileName = "Resources/invoice_splitter/response_v1/complete.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.DocumentQueueGetAsync<InvoiceSplitterV1>("hello");
        }
    }
}
