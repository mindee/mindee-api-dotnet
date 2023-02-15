using Mindee.Parsing;
using Mindee.Parsing.Common;
using Mindee.Parsing.Fr.CarteVitale;

namespace Mindee.UnitTests.Parsing.Fr.CarteVitale
{
    [Trait("Category", "CarteVitaleV1")]
    public class CarteVitaleV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/fr/carte_vitale/response_v1/summary_full.rst");
            Assert.Equal(expected, prediction.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/fr/carte_vitale/response_v1/summary_page0.rst");
            Assert.Equal(expected, prediction.Inference.Pages[0].ToString());
        }

        private async Task<Document<CarteVitaleV1Inference>> GetPrediction()
        {
            string fileName = "Resources/fr/carte_vitale/response_v1/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictAsync<CarteVitaleV1Inference>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
