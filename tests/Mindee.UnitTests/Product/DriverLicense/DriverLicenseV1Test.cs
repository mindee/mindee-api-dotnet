using Mindee.Parsing.Common;
using Mindee.Product.DriverLicense;

namespace Mindee.UnitTests.Product.DriverLicense
{
    [Trait("Category", "DriverLicenseV1")]
    public class DriverLicenseV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.CountryCode.Value);
            Assert.Null(docPrediction.State.Value);
            Assert.Null(docPrediction.Id.Value);
            Assert.Null(docPrediction.Category.Value);
            Assert.Null(docPrediction.LastName.Value);
            Assert.Null(docPrediction.FirstName.Value);
            Assert.Null(docPrediction.DateOfBirth.Value);
            Assert.Null(docPrediction.PlaceOfBirth.Value);
            Assert.Null(docPrediction.ExpiryDate.Value);
            Assert.Null(docPrediction.IssuedDate.Value);
            Assert.Null(docPrediction.IssuingAuthority.Value);
            Assert.Null(docPrediction.Mrz.Value);
            Assert.Null(docPrediction.DdNumber.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/driver_license/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<DriverLicenseV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/driver_license/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<DriverLicenseV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
