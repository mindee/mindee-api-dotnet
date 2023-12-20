using Mindee.Parsing.Common;
using Mindee.Product.InternationalId;

namespace Mindee.UnitTests.Product.InternationalId
{
    [Trait("Category", "InternationalIdV1")]
    public class InternationalIdV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.DocumentNumber.Value);
            Assert.Null(docPrediction.CountryOfIssue.Value);
            Assert.Empty(docPrediction.Surnames);
            Assert.Empty(docPrediction.GivenNames);
            Assert.Null(docPrediction.Sex.Value);
            Assert.Null(docPrediction.BirthDate.Value);
            Assert.Null(docPrediction.BirthPlace.Value);
            Assert.Null(docPrediction.Nationality.Value);
            Assert.Null(docPrediction.IssueDate.Value);
            Assert.Null(docPrediction.ExpiryDate.Value);
            Assert.Null(docPrediction.Address.Value);
            Assert.Null(docPrediction.Mrz1.Value);
            Assert.Null(docPrediction.Mrz2.Value);
            Assert.Null(docPrediction.Mrz3.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/international_id/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<InternationalIdV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/international_id/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<InternationalIdV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
