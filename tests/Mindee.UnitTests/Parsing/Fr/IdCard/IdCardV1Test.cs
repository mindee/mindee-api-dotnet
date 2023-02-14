using Mindee.Parsing;
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
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/fr/id_card/response_v1/summary_full.rst");
            Assert.Equal(expected, prediction.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/fr/id_card/response_v1/summary_page0.rst");
            Assert.Equal(expected, prediction.Inference.Pages[0].ToString());
        }

        private async Task<Document<IdCardV1Inference>> GetPrediction()
        {
            string fileName = "Resources/fr/id_card/response_v1/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictAsync<IdCardV1Inference>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
