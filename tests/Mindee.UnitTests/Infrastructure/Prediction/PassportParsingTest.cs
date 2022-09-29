using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Infrastructure.Api;
using Mindee.Infrastructure.Prediction;
using Mindee.Prediction;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests.Infrastructure.Prediction
{
    public class PassportParsingTest
    {
        [Fact]
        public async Task Execute_WithPassportData_MustSuccess()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.NotNull(prediction);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForCountry()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("string", prediction.Country.Value);
            Assert.Equal(0.99, prediction.Country.Confidence);
            Assert.Equal(0, prediction.Country.PageId); 
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.2988, 0.9426 },
                new List<double>() { 0.5498, 0.0769 },
                new List<double>() { 0.3998, 0.7691 },
                new List<double>() { 0.8192, 0.0147 },
            }
            , prediction.Country.Polygon);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForIdNumber()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("string", prediction.IdNumber.Value);

        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForGivenNames()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            var firstGivenNames = prediction.GivenNames.FirstOrDefault();

            Assert.NotNull(firstGivenNames);
            Assert.Single(prediction.GivenNames);
            Assert.Equal("string", firstGivenNames!.Value);
            Assert.Equal(0.99, firstGivenNames!.Confidence);
            Assert.Equal(0, firstGivenNames!.PageId);

        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForSurname()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("string", prediction.Surname.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForBirthdate()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("1963-08-03", prediction.BirthDate.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForBirthplace()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("string", prediction.BirthPlace.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForGender()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("string", prediction.Gender.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForIssuanceDate()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("2013-04-12", prediction.IssuanceDate.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForExpiryDate()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("2029-08-03", prediction.ExpiryDate.Value);
        }

        [Fact(Skip = "Orientation can not be get for now.")]
        public async Task Execute_WithPassportData_MustSuccessForOrientation()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal(90, prediction.Orientation.Degrees);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForMrz1()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("string", prediction.Mrz1.Value);
        }

        [Fact]
        public async Task Execute_WithPassportData_MustSuccessForMrz2()
        {
            IPassportParsing passportParsing = new PassportParsing(GetMindeeApi());
            var prediction = await passportParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("string", prediction.Mrz2.Value);
        }

        private static MindeeApi GetMindeeApi()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                    .Respond("application/json", File.ReadAllText("passport_response_full_v1.json"));

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
