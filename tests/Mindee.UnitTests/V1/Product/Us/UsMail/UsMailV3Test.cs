using Mindee.Parsing.Common;
using Mindee.Product.Us.UsMail;

namespace Mindee.UnitTests.V1.Product.Us.UsMail
{
    [Trait("Category", "UsMailV3")]
    public class UsMailV3Test
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
            Assert.Null(docPrediction.IsReturnToSender.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "us_mail/response_v3/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<UsMailV3>> GetPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/us_mail/response_v3/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<UsMailV3>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
