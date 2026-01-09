using Mindee.Parsing.Common;
using Mindee.Product.Cropper;

namespace Mindee.UnitTests.V1.Product.Cropper
{
    [Trait("Category", "CropperV1")]
    public class CropperV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var pagePrediction = response.Document.Inference.Pages.First().Prediction;
            Assert.Empty(pagePrediction.Cropping);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "cropper/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "cropper/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<CropperV1>> GetPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/cropper/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<CropperV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
