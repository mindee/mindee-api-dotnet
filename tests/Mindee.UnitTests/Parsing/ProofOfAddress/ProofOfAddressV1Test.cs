using Mindee.Parsing;
using Mindee.Parsing.ProofOfAddress;

namespace Mindee.UnitTests.Parsing.ProofOfAddress
{
    [Trait("Category", "Proof of Address V1")]
    public class ProofOfAddressV1Test
    {
        [Fact]
        public async Task Predict_All_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var prediction = await mindeeAPi.PredictAsync<ProofOfAddressV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/proof_of_address/response_v1/summary_full.rst");

            Assert.Equal(
                expected,
                prediction.ToString());
        }

        [Fact]
        public async Task Predict_FirstPage_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var prediction = await mindeeAPi.PredictAsync<ProofOfAddressV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/proof_of_address/response_v1/summary_page0.rst");

            Assert.Equal(
                expected,
                prediction.Inference.Pages.First().ToString());
        }

        private MindeeApi GetMindeeApiForInvoice(string fileName = "Resources/proof_of_address/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
