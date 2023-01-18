using Mindee.Parsing;
using Mindee.Parsing.CustomBuilder;

namespace Mindee.UnitTests.Parsing
{
    public class CustomParsingTest
    {
        [Fact]
        [Trait("Category", "Custom API")]
        public async Task Execute_WithReceiptData_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<CustomV1Inference>(
                new CustomEndpoint("customProduct", "fakeOrga"),
                ParsingTestBase.GetFakePredictParameter());

            Assert.NotNull(prediction);
        }

        [Fact]
        [Trait("Category", "Custom API")]
        public async Task Predict_WithFieldWithOnlyOneValue_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<CustomV1Inference>(
                new CustomEndpoint("customProduct", "fakeOrga"),
                ParsingTestBase.GetFakePredictParameter());

            var listField = prediction.Inference.Pages.First().Prediction["date_normal"];
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
        [Trait("Category", "Custom API")]
        public async Task Predict_WithFieldWithMultipleValues_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<CustomV1Inference>(
                new CustomEndpoint("customProduct", "fakeOrga"),
                ParsingTestBase.GetFakePredictParameter());

            var listField = prediction.Inference.Pages.First().Prediction["string_all"];
            Assert.Equal(3, listField!.Values.Count);
            Assert.Equal(1.0, listField.Confidence);
            Assert.Equal(1.0, listField.Values.Last().Confidence);
            Assert.Equal("great", listField.Values.Last().Content);
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
        [Trait("Category", "Custom API")]
        public async Task Predict_WithFieldWithNoValues_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<CustomV1Inference>(
                new CustomEndpoint("customProduct", "fakeOrga"),
                ParsingTestBase.GetFakePredictParameter());

            var listField = prediction.Inference.Pages.First().Prediction["url"];
            Assert.Equal(0.0, listField!.Confidence);
            Assert.False(listField.Values.Any());
        }

        [Fact]
        [Trait("Category", "Custom API")]
        public async Task Predict_MustSuccessfullyGetOrientation()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<CustomV1Inference>(
                new CustomEndpoint("customProduct", "fakeOrga"),
                ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(0, prediction.Inference.Pages.First().Orientation.Value);
        }

        [Fact]
        [Trait("Category", "Custom API")]
        public async Task Predict_MustSuccessfullyHandleMultiplePages()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<CustomV1Inference>(
                new CustomEndpoint("customProduct", "fakeOrga"),
                ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(2, prediction.Inference.Pages.Count);
        }

        private MindeeApi GetMindeeApiForReceipt(string fileName = "Resources/custom/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
