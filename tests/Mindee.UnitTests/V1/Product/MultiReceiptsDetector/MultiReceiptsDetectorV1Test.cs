using Mindee.Parsing.Common;
using Mindee.Product.MultiReceiptsDetector;

namespace Mindee.UnitTests.V1.Product.MultiReceiptsDetector
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
            var expected =
                File.ReadAllText(Constants.V1ProductDir + "multi_receipts_detector/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<MultiReceiptsDetectorV1>> GetPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/multi_receipts_detector/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<MultiReceiptsDetectorV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
