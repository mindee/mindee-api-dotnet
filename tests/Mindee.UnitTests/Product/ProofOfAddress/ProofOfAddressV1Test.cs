using Mindee.Parsing.Common;
using Mindee.Product.ProofOfAddress;

namespace Mindee.UnitTests.Product.ProofOfAddress
{
    [Trait("Category", "ProofOfAddressV1")]
    public class ProofOfAddressV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.Locale.Value);
            Assert.Null(docPrediction.IssuerName.Value);
            Assert.Empty(docPrediction.IssuerCompanyRegistration);
            Assert.Null(docPrediction.IssuerAddress.Value);
            Assert.Null(docPrediction.RecipientName.Value);
            Assert.Empty(docPrediction.RecipientCompanyRegistration);
            Assert.Null(docPrediction.RecipientAddress.Value);
            Assert.Empty(docPrediction.Dates);
            Assert.Null(docPrediction.Date.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/proof_of_address/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<ProofOfAddressV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/proof_of_address/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<ProofOfAddressV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
