using Mindee.Parsing;
using Mindee.Parsing.Fr.IdCard;

namespace Mindee.UnitTests.Parsing.Fr.IdCard
{
    [Trait("Category", "FR Carte Vitale V1")]
    public class CarteVitaleV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForCarteVitale();
            var prediction = await mindeeAPi.PredictAsync<CarteVitaleV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/fr/carte_vitale/response_v1/doc_to_string.txt");

            Assert.Equal(
                ParsingTestBase.CleaningFilenameFromResult(expected),
                prediction.Inference.Prediction.ToString());
        }

        private MindeeApi GetMindeeApiForCarteVitale(string fileName = "Resources/fr/carte_vitale/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
