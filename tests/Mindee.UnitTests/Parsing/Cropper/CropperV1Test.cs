using Mindee.Parsing;
using Mindee.Parsing.Cropper;

namespace Mindee.UnitTests.Parsing.Receipt
{
    [Trait("Category", "Cropper V1")]
    public class CropperV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<CropperV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/cropper/response_v1/doc_to_string.txt");

            Assert.Equal(
                ParsingTestBase.CleaningFilenameFromResult(expected),
                prediction.Inference.DocumentPrediction.ToString());
        }

        [Fact]
        public async Task Predict_CheckSummary_WithMultiplePages()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<CropperV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/cropper/response_v1/page0_to_string.txt");

            Assert.Equal(
                ParsingTestBase.CleaningFilenameFromResult(expected),
                prediction.Inference.Pages.First().Prediction.ToString());
        }

        [Fact]
        public async Task Predict_WithCropping_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<CropperV1Inference>(ParsingTestBase.GetFakePredictParameter());

            Assert.NotNull(prediction.Inference.Pages.First().Prediction.Cropping);
            Assert.Equal(2, prediction.Inference.Pages.First().Prediction.Cropping.Count);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.588, 0.25 },
                new List<double>() { 0.953, 0.25 },
                new List<double>() { 0.953, 0.682 },
                new List<double>() { 0.588, 0.682 },
            }
            , prediction.Inference.Pages.First().Prediction.Cropping.First().BoundingBox);

            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.589, 0.252 },
                new List<double>() { 0.953, 0.25 },
                new List<double>() { 0.949, 0.639 },
                new List<double>() { 0.607, 0.681 },
            }
            , prediction.Inference.Pages.First().Prediction.Cropping.First().Quadrangle);

            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.588, 0.25 },
                new List<double>() { 0.951, 0.25 },
                new List<double>() { 0.951, 0.68 },
                new List<double>() { 0.588, 0.68 },
            }
            , prediction.Inference.Pages.First().Prediction.Cropping.First().Rectangle);

            Assert.Equal(
                new List<double>() { 0.598, 0.377 }
            , prediction.Inference.Pages.First().Prediction.Cropping.First().Polygon.First());
        }

        private MindeeApi GetMindeeApiForReceipt(string fileName = "Resources/cropper/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
