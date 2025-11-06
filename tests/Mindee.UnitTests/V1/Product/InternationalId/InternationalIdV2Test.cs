using Mindee.Parsing.Common;
using Mindee.Product.InternationalId;

namespace Mindee.UnitTests.V1.Product.InternationalId
{
    [Trait("Category", "InternationalIdV2")]
    public class InternationalIdV2Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.IsType<Mindee.Parsing.Standard.ClassificationField>(docPrediction.DocumentType);
            Assert.Null(docPrediction.DocumentNumber.Value);
            Assert.Empty(docPrediction.Surnames);
            Assert.Empty(docPrediction.GivenNames);
            Assert.Null(docPrediction.Sex.Value);
            Assert.Null(docPrediction.BirthDate.Value);
            Assert.Null(docPrediction.BirthPlace.Value);
            Assert.Null(docPrediction.Nationality.Value);
            Assert.Null(docPrediction.PersonalNumber.Value);
            Assert.Null(docPrediction.CountryOfIssue.Value);
            Assert.Null(docPrediction.StateOfIssue.Value);
            Assert.Null(docPrediction.IssueDate.Value);
            Assert.Null(docPrediction.ExpiryDate.Value);
            Assert.Null(docPrediction.Address.Value);
            Assert.Null(docPrediction.MrzLine1.Value);
            Assert.Null(docPrediction.MrzLine2.Value);
            Assert.Null(docPrediction.MrzLine3.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/v1/products/international_id/response_v2/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<InternationalIdV2>> GetPrediction(string name)
        {
            string fileName = $"Resources/v1/products/international_id/response_v2/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<InternationalIdV2>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
