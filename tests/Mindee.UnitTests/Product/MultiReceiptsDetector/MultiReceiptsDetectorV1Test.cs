using Mindee.Parsing.Common;
using Mindee.Product.MultiReceiptsDetector;

namespace Mindee.UnitTests.Product.MultiReceiptsDetector
{
    [Trait("Category", "MultiReceiptsDetectorV1")]
    public class MultiReceiptsDetectorV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Empty(docPrediction.Receipts);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/multi_receipts_detector/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/multi_receipts_detector/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<MultiReceiptsDetectorV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/multi_receipts_detector/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<MultiReceiptsDetectorV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
