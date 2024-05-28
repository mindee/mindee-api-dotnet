using Mindee.Parsing.Common;
using Mindee.Product.Us.UsMail;

namespace Mindee.UnitTests.Product.Us.UsMail
{
    [Trait("Category", "UsMailV2")]
    public class UsMailV2Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.SenderName.Value);
            Assert.Null(docPrediction.SenderAddress.City);
            Assert.Null(docPrediction.SenderAddress.Complete);
            Assert.Null(docPrediction.SenderAddress.PostalCode);
            Assert.Null(docPrediction.SenderAddress.State);
            Assert.Null(docPrediction.SenderAddress.Street);
            Assert.Empty(docPrediction.RecipientNames);
            Assert.Empty(docPrediction.RecipientAddresses);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/us_mail/response_v2/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<UsMailV2>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/us_mail/response_v2/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<UsMailV2>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
