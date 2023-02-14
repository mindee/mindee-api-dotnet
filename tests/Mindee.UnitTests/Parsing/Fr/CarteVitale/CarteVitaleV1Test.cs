using Mindee.Parsing;
using Mindee.Parsing.Fr.CarteVitale;

namespace Mindee.UnitTests.Parsing.Fr.CarteVitale
{
    [Trait("Category", "FR Carte Vitale V1")]
    public class CarteVitaleV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForCarteVitale();
            var prediction = await mindeeAPi.PredictAsync<CarteVitaleV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/fr/carte_vitale/response_v1/summary_full.rst");

            Assert.Equal(
                expected,
                prediction.ToString());
        }

        private MindeeApi GetMindeeApiForCarteVitale(string fileName = "Resources/fr/carte_vitale/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
