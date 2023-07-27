using Mindee.Parsing.Common;
using Mindee.Product.Cropper;

namespace Mindee.UnitTests.Product.Cropper
{
    [Trait("Category", "Cropper V1")]
    public class CropperV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/cropper/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckSummary_WithMultiplePages()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/cropper/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        [Fact]
        public async Task Predict_WithCropping_MustSuccess()
        {
            var response = await GetPrediction("complete");

            Assert.NotNull(response.Document.Inference.Pages.First().Prediction.Cropping);
            Assert.Equal(2, response.Document.Inference.Pages.First().Prediction.Cropping.Count);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.588, 0.25 },
                new List<double>() { 0.953, 0.25 },
                new List<double>() { 0.953, 0.682 },
                new List<double>() { 0.588, 0.682 },
            }
            , response.Document.Inference.Pages.First().Prediction.Cropping.First().BoundingBox);

            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.589, 0.252 },
                new List<double>() { 0.953, 0.25 },
                new List<double>() { 0.949, 0.639 },
                new List<double>() { 0.607, 0.681 },
            }
            , response.Document.Inference.Pages.First().Prediction.Cropping.First().Quadrangle);

            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.588, 0.25 },
                new List<double>() { 0.951, 0.25 },
                new List<double>() { 0.951, 0.68 },
                new List<double>() { 0.588, 0.68 },
            }
            , response.Document.Inference.Pages.First().Prediction.Cropping.First().Rectangle);

            Assert.Equal(
                new List<double>() { 0.598, 0.377 }
            , response.Document.Inference.Pages.First().Prediction.Cropping.First().Polygon.First());
        }

        private async Task<PredictResponse<CropperV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/cropper/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<CropperV1>(
                UnitTestBase.GetFakePredictParameter());
        }

    }
}
