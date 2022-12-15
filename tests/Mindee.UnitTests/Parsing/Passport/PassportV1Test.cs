using Mindee.Parsing;
using Mindee.Parsing.Passport;

namespace Mindee.UnitTests.Parsing.Passport
{
    [Trait("Category", "Passport V1")]
    public class PassportV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForPassport();
            var prediction = await mindeeAPi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/passport/response_v1/doc_to_string.txt");

            var indexFilename = expected.IndexOf("Filename");
            var indexEOL = expected.IndexOf("\n", indexFilename);

            Assert.Equal(
                expected.Remove(indexFilename, indexEOL - indexFilename + 1),
                prediction.Inference.Prediction.ToString());
        }

        [Fact]
        public async Task Predict_CheckSummary_WithMultiplePages()
        {
            var mindeeAPi = GetMindeeApiForPassport();
            var prediction = await mindeeAPi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/passport/response_v1/page0_to_string.txt");

            var indexFilename = expected.IndexOf("Filename");
            var indexEOL = expected.IndexOf("\n", indexFilename);

            Assert.Equal(
                expected.Remove(indexFilename, indexEOL - indexFilename + 1),
                prediction.Inference.Pages.First().Prediction.ToString());
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForCountry()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("GBR", prediction.Inference.Pages.First().Prediction.Country.Value);
            Assert.Equal(1.0, prediction.Inference.Pages.First().Prediction.Country.Confidence);
            Assert.Equal(0, prediction.Inference.Pages.First().Id);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.508, 0.547 },
                new List<double>() { 0.559, 0.547 },
                new List<double>() { 0.559, 0.568 },
                new List<double>() { 0.508, 0.568 },
            }
            , prediction.Inference.Pages.First().Prediction.Country.Polygon);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForIdNumber()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("707797979", prediction.Inference.Pages.First().Prediction.IdNumber.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForGivenNames()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            var firstGivenNames = prediction.Inference.Pages.First().Prediction.GivenNames.FirstOrDefault();

            Assert.NotNull(firstGivenNames);
            Assert.Single(prediction.Inference.Pages.First().Prediction.GivenNames);
            Assert.Equal("HENERT", firstGivenNames!.Value);
            Assert.Equal(0.99, firstGivenNames!.Confidence);
            Assert.Equal(0, prediction.Inference.Pages.First().Id);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForSurname()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("PUDARSAN", prediction.Inference.Pages.First().Prediction.Surname.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForBirthdate()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("1995-05-20", prediction.Inference.Pages.First().Prediction.BirthDate.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForBirthplace()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("CAMTETH", prediction.Inference.Pages.First().Prediction.BirthPlace.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForGender()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("M", prediction.Inference.Pages.First().Prediction.Gender.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForIssuanceDate()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("2012-04-22", prediction.Inference.Pages.First().Prediction.IssuanceDate.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForExpiryDate()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("2057-04-22", prediction.Inference.Pages.First().Prediction.ExpiryDate.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForOrientation()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(0, prediction.Inference.Pages.First().Orientation.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForMrz1()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("P<GBRPUDARSAN<<HENERT<<<<<<<<<<<<<<<<<<<<<<<", prediction.Inference.Pages.First().Prediction.Mrz1.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForMrz2()
        {
            var mindeeApi = GetMindeeApiForPassport();
            var prediction = await mindeeApi.PredictAsync<PassportV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("7077979792GBR9505209M1704224<<<<<<<<<<<<<<00", prediction.Inference.Pages.First().Prediction.Mrz2.Value);
        }

        private MindeeApi GetMindeeApiForPassport(string fileName = "Resources/passport/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
