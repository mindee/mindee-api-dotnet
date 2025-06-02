using Mindee.Parsing.Common;
using Mindee.Product.Us.HealthcareCard;

namespace Mindee.UnitTests.Product.Us.HealthcareCard
{
    [Trait("Category", "HealthcareCardV1")]
    public class HealthcareCardV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.CompanyName.Value);
            Assert.Null(docPrediction.PlanName.Value);
            Assert.Null(docPrediction.MemberName.Value);
            Assert.Null(docPrediction.MemberId.Value);
            Assert.Null(docPrediction.Issuer80840.Value);
            Assert.Empty(docPrediction.Dependents);
            Assert.Null(docPrediction.GroupNumber.Value);
            Assert.Null(docPrediction.PayerId.Value);
            Assert.Null(docPrediction.RxBin.Value);
            Assert.Null(docPrediction.RxId.Value);
            Assert.Null(docPrediction.RxGrp.Value);
            Assert.Null(docPrediction.RxPcn.Value);
            Assert.Empty(docPrediction.Copays);
            Assert.Null(docPrediction.EnrollmentDate.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/us_healthcare_cards/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<HealthcareCardV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/us_healthcare_cards/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<HealthcareCardV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
