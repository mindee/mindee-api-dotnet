using Mindee.Parsing.Common;
using Mindee.Product.Receipt;

namespace Mindee.UnitTests.Product.Receipt
{
    [Trait("Category", "ReceiptV5")]
    public class ReceiptV5Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/receipt/response_v5/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/receipt/response_v5/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
            Assert.Equal(0, response.Document.Inference.Pages[0].Orientation.Value);
        }

        private static async Task<PredictResponse<ReceiptV5>> GetPrediction()
        {
            const string fileName = "Resources/receipt/response_v5/complete.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<ReceiptV5>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
