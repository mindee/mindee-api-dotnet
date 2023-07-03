using Mindee.Parsing.Common;
using Mindee.Parsing.Fr.IdCard;

namespace Mindee.UnitTests.Parsing.Fr.IdCard
{
    [Trait("Category", "IdCardV1")]
    public class IdCardV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/fr/id_card/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/fr/id_card/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private async Task<PredictResponse<IdCardV1Inference>> GetPrediction()
        {
            const string fileName = "Resources/fr/id_card/response_v1/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictAsync<IdCardV1Inference>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
