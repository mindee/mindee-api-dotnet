using Mindee.Parsing.Common;
using Mindee.Product.Eu.LicensePlate;

namespace Mindee.UnitTests.Parsing.Eu.LicensePlate
{
    [Trait("Category", "LicensePlateV1")]
    public class LicensePlateV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/eu/license_plate/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/eu/license_plate/response_v1/summary_page0.rst");
            Assert.Equal(expected, prediction.Document.Inference.Pages[0].ToString());
        }

        private async Task<PredictResponse<LicensePlateV1>> GetPrediction()
        {
            const string fileName = "Resources/eu/license_plate/response_v1/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<LicensePlateV1>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
