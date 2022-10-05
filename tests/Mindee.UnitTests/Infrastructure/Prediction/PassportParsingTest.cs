using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Infrastructure.Api;
using Mindee.Infrastructure.Prediction;
using Mindee.Domain.Parsing;
using RichardSzalay.MockHttp;
using Mindee.Domain;

namespace Mindee.UnitTests.Infrastructure.Prediction
{
    public class PassportParsingTest
    {
        [Fact]
        public async Task Execute_WithPassportData_MustSuccess()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

            Assert.NotNull(prediction);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForCountry()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

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
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.IdNumber.Value);

        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForGivenNames()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

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
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.Surname.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForBirthdate()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

            Assert.Equal("1963-08-03", prediction.Inference.Pages.First().Prediction.BirthDate.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForBirthplace()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.BirthPlace.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForGender()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.Gender.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForIssuanceDate()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

            Assert.Equal("2013-04-12", prediction.Inference.Pages.First().Prediction.IssuanceDate.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForExpiryDate()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

            Assert.Equal("2029-08-03", prediction.Inference.Pages.First().Prediction.ExpiryDate.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForOrientation()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

            Assert.Equal(180, prediction.Inference.Pages.First().Orientation.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForMrz1()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.Mrz1.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForMrz2()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(GetFakeParseParameter());

            Assert.Equal("string", prediction.Inference.Pages.First().Prediction.Mrz2.Value);
        }

        private ParseParameter GetFakeParseParameter()
        {
            return
                new ParseParameter(
                    new DocumentClient(
                        new byte[] { byte.MinValue },
                        "Bou"));
        }

        private static MindeeApi GetMindeeApi()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                    .Respond("application/json", File.ReadAllText("Resources/passport_response_full_v1.json"));

            var config = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>() {
                                { "MindeeApiSettings:ApiKey", "blou" }
                            })
                        .Build();

            return new MindeeApi(
                new NullLogger<MindeeApi>(),
                Options.Create(new MindeeApiSettings()
                {
                    ApiKey = "Expelliarmus"
                }),
                config,
                mockHttp
                );
        }
    }
}
