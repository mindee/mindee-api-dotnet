using Mindee.Parsing.Common;
using Mindee.Product.Cropper;

namespace Mindee.UnitTests.Product.Cropper
{
    [Trait("Category", "CropperV1")]
    public class CropperV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            Assert.Empty(response.Document.Inference.Pages.First().Prediction.Cropping);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/cropper/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/cropper/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<CropperV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/cropper/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<CropperV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
