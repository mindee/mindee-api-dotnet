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
            Assert.Null(response.Document.Inference.Prediction.Locale.Value);
            Assert.Null(response.Document.Inference.Prediction.IssuerName.Value);
            Assert.Empty(response.Document.Inference.Prediction.IssuerCompanyRegistration);
            Assert.Null(response.Document.Inference.Prediction.IssuerAddress.Value);
            Assert.Null(response.Document.Inference.Prediction.RecipientName.Value);
            Assert.Empty(response.Document.Inference.Prediction.RecipientCompanyRegistration);
            Assert.Null(response.Document.Inference.Prediction.RecipientAddress.Value);
            Assert.Empty(response.Document.Inference.Prediction.Dates);
            Assert.Null(response.Document.Inference.Prediction.Date.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/proof_of_address/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/proof_of_address/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
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
