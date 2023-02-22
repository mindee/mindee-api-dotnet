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
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/proof_of_address/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/proof_of_address/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<ProofOfAddressV1Inference>> GetPrediction()
        {
            const string fileName = "Resources/proof_of_address/response_v1/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<ProofOfAddressV1Inference>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
