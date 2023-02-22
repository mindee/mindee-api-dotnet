using Mindee.Parsing.Common;
using Mindee.Parsing.Passport;

namespace Mindee.UnitTests.Parsing.Passport
{
    [Trait("Category", "Passport V1")]
    public class PassportV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/passport/response_v1/summary_full.rst");
            Assert.Equal(
                expected,
                response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckSummary_WithMultiplePages()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/passport/response_v1/summary_page0.rst");
            Assert.Equal(
                expected,
                response.Document.Inference.Pages.First().ToString());
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForCountry()
        {
            var response = await GetPrediction();

            Assert.Equal("GBR", response.Document.Inference.Pages.First().Prediction.Country.Value);
            Assert.Equal(1.0, response.Document.Inference.Pages.First().Prediction.Country.Confidence);
            Assert.Equal(0, response.Document.Inference.Pages.First().Id);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.508, 0.547 },
                new List<double>() { 0.559, 0.547 },
                new List<double>() { 0.559, 0.568 },
                new List<double>() { 0.508, 0.568 },
            }
            , response.Document.Inference.Pages.First().Prediction.Country.Polygon);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForGivenNames()
        {
            var response = await GetPrediction();

            var firstGivenNames = response.Document.Inference.Pages.First().Prediction.GivenNames.FirstOrDefault();
            Assert.NotNull(firstGivenNames);
            Assert.Single(response.Document.Inference.Pages.First().Prediction.GivenNames);
            Assert.Equal("HENERT", firstGivenNames!.Value);
            Assert.Equal(0.99, firstGivenNames!.Confidence);
            Assert.Equal(0, response.Document.Inference.Pages.First().Id);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForOrientation()
        {
            var response = await GetPrediction();
            Assert.Equal(0, response.Document.Inference.Pages.First().Orientation.Value);
        }

        private static async Task<PredictResponse<PassportV1Inference>> GetPrediction()
        {
            const string fileName = "Resources/passport/response_v1/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<PassportV1Inference>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
