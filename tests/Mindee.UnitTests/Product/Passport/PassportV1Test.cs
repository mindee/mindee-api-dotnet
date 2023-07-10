using Mindee.Parsing.Common;
using Mindee.Product.Passport;

namespace Mindee.UnitTests.Product.Passport
{
    [Trait("Category", "PassportV1")]
    public class PassportV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/passport/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/passport/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
            Assert.Equal(0, response.Document.Inference.Pages[0].Orientation.Value);
        }

        private static async Task<PredictResponse<PassportV1>> GetPrediction()
        {
            const string fileName = "Resources/passport/response_v1/complete.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<PassportV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
