using Mindee.Parsing.Common;
using Mindee.Product.BusinessCard;

namespace Mindee.UnitTests.V1.Product.BusinessCard
{
    [Trait("Category", "BusinessCardV1")]
    public class BusinessCardV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.Firstname.Value);
            Assert.Null(docPrediction.Lastname.Value);
            Assert.Null(docPrediction.JobTitle.Value);
            Assert.Null(docPrediction.Company.Value);
            Assert.Null(docPrediction.Email.Value);
            Assert.Null(docPrediction.PhoneNumber.Value);
            Assert.Null(docPrediction.MobileNumber.Value);
            Assert.Null(docPrediction.FaxNumber.Value);
            Assert.Null(docPrediction.Address.Value);
            Assert.Null(docPrediction.Website.Value);
            Assert.Empty(docPrediction.SocialMedia);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "business_card/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<BusinessCardV1>> GetPrediction(string name)
        {
            string fileName = Constants.V1RootDir + $"products/business_card/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<BusinessCardV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
