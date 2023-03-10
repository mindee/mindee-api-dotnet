using Mindee.Parsing;
using Mindee.Parsing.Common;
using Mindee.Parsing.ProofOfAddress;

namespace Mindee.UnitTests.Parsing.ProofOfAddress
{
    [Trait("Category", "ProofOfAddressV1")]
    public class ProofOfAddressV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/proof_of_address/response_v1/summary_full.rst");
            Assert.Equal(expected, prediction.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/proof_of_address/response_v1/summary_page0.rst");
            Assert.Equal(expected, prediction.Inference.Pages[0].ToString());
        }

        private async Task<Document<ProofOfAddressV1Inference>> GetPrediction()
        {
            string fileName = "Resources/proof_of_address/response_v1/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictAsync<ProofOfAddressV1Inference>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
