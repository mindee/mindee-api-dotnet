using Mindee.Http;
using Mindee.Parsing.Common;
using Mindee.Product.Custom;

namespace Mindee.UnitTests.Product.Custom
{
    [Trait("Category", "Custom API")]
    public class CustomV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/products/custom/response_v1/summary_full.rst");
            Assert.Equal(
                expected,
                response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/products/custom/response_v1/summary_page0.rst");
            Assert.Equal(expected, prediction.Document.Inference.Pages[0].ToString());
        }

        [Fact]
        public async Task Predict_WithFieldWithOnlyOneValue_MustSuccess()
        {
            var response = await GetPrediction();

            var listField = response.Document.Inference.Pages.First().Prediction["date_normal"];
            Assert.Equal(0.99, listField!.Confidence);
            Assert.Equal(0.99, listField.Values.First().Confidence);
            Assert.Equal("2020-12-17", listField.Values.First().Content);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.834, 0.177 },
                new List<double>() { 0.927, 0.177 },
                new List<double>() { 0.927, 0.186 },
                new List<double>() { 0.835, 0.187 },
            },
            listField.Values.First().Polygon);
        }

        [Fact]
        public async Task Predict_WithFieldWithMultipleValues_MustSuccess()
        {
            var response = await GetPrediction();

            var listField = response.Document.Inference.Pages.First().Prediction["string_all"];
            Assert.Equal(3, listField!.Values.Count);
            Assert.Equal(1.0, listField.Confidence);
            Assert.Equal(1.0, listField.Values.Last().Confidence);
            Assert.Equal("great", listField.Values.Last().Content);
            Assert.Equal("Jenny_is_great", listField.ContentsString("_"));
            Assert.Equal("Jenny is great", listField.ToString());
            Assert.Equal(new List<string>() { "Jenny", "is", "great" }, listField.ContentsList);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.713, 0.013 },
                new List<double>() { 0.956, 0.013 },
                new List<double>() { 0.956, 0.054 },
                new List<double>() { 0.713, 0.055 },
            },
            listField.Values.First().Polygon);
        }

        [Fact]
        public async Task Predict_WithFieldWithNoValues_MustSuccess()
        {
            var response = await GetPrediction();
            var listField = response.Document.Inference.Pages.First().Prediction["url"];
            Assert.Equal(0.0, listField!.Confidence);
            Assert.False(listField.Values.Any());
        }

        [Fact]
        public async Task Predict_MustSuccessfullyGetOrientation()
        {
            var response = await GetPrediction();
            Assert.Equal(0, response.Document.Inference.Pages.First().Orientation.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessfullyHandleMultiplePages()
        {
            var response = await GetPrediction();
            Assert.Equal(2, response.Document.Inference.Pages.Count);
        }

        private static async Task<PredictResponse<CustomV1>> GetPrediction()
        {
            const string fileName = "Resources/products/custom/response_v1/complete.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<CustomV1>(
                new CustomEndpoint("customProduct", "fakeOrga"),
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
