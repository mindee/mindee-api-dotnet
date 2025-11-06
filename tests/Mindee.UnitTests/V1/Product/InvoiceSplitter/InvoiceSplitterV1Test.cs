using Mindee.Parsing.Common;
using Mindee.Product.InvoiceSplitter;

namespace Mindee.UnitTests.V1.Product.InvoiceSplitter
{
    [Trait("Category", "InvoiceSplitterV1")]
    public class InvoiceSplitterV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Empty(docPrediction.InvoicePageGroups);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "invoice_splitter/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<InvoiceSplitterV1>> GetPrediction(string name)
        {
            string fileName = Constants.V1RootDir + $"products/invoice_splitter/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<InvoiceSplitterV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
