using Mindee.Parsing;
using Mindee.Parsing.Common;
using Mindee.Parsing.Eu.LicensePlate;

namespace Mindee.UnitTests.Parsing.Eu.LicensePlate
{
    [Trait("Category", "LicensePlateV1")]
    public class LicensePlateV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/eu/license_plate/response_v1/summary_full.rst");
            Assert.Equal(expected, prediction.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/eu/license_plate/response_v1/summary_page0.rst");
            Assert.Equal(expected, prediction.Inference.Pages[0].ToString());
        }

        private async Task<Document<LicensePlateV1Inference>> GetPrediction()
        {
            string fileName = "Resources/eu/license_plate/response_v1/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictAsync<LicensePlateV1Inference>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
