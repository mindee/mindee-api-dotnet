using Mindee.Parsing;
using Mindee.Parsing.Passport;

namespace Mindee.UnitTests.Parsing.Prediction
{
    public class PassportParsingTest : ParsingTestBase
    {
        [Fact]
        public async Task Execute_WithPassportData_MustSuccess()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.NotNull(prediction);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForCountry()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.Country.Value);
            Assert.Equal(0.99, prediction.Inference.Pages.First().Prediction.Country.Confidence);
            Assert.Equal(0, prediction.Inference.Pages.First().Id);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.2988, 0.9426 },
                new List<double>() { 0.5498, 0.0769 },
                new List<double>() { 0.3998, 0.7691 },
                new List<double>() { 0.8192, 0.0147 },
            }
            , prediction.Inference.Pages.First().Prediction.Country.Polygon);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForIdNumber()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.IdNumber.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForGivenNames()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            var firstGivenNames = prediction.Inference.Pages.First().Prediction.GivenNames.FirstOrDefault();

            Assert.NotNull(firstGivenNames);
            Assert.Single(prediction.Inference.Pages.First().Prediction.GivenNames);
            Assert.Equal("string", firstGivenNames!.Value);
            Assert.Equal(0.99, firstGivenNames!.Confidence);
            Assert.Equal(0, prediction.Inference.Pages.First().Id);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForSurname()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.Surname.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForBirthdate()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.Equal("1963-08-03", prediction.Inference.Pages.First().Prediction.BirthDate.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForBirthplace()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.BirthPlace.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForGender()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.Gender.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForIssuanceDate()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.Equal("2013-04-12", prediction.Inference.Pages.First().Prediction.IssuanceDate.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForExpiryDate()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.Equal("2029-08-03", prediction.Inference.Pages.First().Prediction.ExpiryDate.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForOrientation()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.Equal(180, prediction.Inference.Pages.First().Orientation.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForMrz1()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.Mrz1.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForMrz2()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportPrediction>(GetFakePredictParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.Mrz2.Value);
        }

        private MindeeApi GetMindeeApiForPassport(string fileName = "Resources/passport_response_full_v1.json")
        {
            return GetMindeeApi(fileName);
        }
    }
}
