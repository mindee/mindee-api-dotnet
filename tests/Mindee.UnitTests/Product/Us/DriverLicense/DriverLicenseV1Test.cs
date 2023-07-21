using Mindee.Parsing.Common;
using Mindee.Product.Us.DriverLicense;

namespace Mindee.UnitTests.Product.Us.DriverLicense
{
    [Trait("Category", "DriverLicenseV1")]
    public class DriverLicenseV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            Assert.Null(response.Document.Inference.Prediction.State.Value);
            Assert.Null(response.Document.Inference.Prediction.DriverLicenseId.Value);
            Assert.Null(response.Document.Inference.Prediction.ExpiryDate.Value);
            Assert.Null(response.Document.Inference.Prediction.IssuedDate.Value);
            Assert.Null(response.Document.Inference.Prediction.LastName.Value);
            Assert.Null(response.Document.Inference.Prediction.FirstName.Value);
            Assert.Null(response.Document.Inference.Prediction.Address.Value);
            Assert.Null(response.Document.Inference.Prediction.DateOfBirth.Value);
            Assert.Null(response.Document.Inference.Prediction.Restrictions.Value);
            Assert.Null(response.Document.Inference.Prediction.Endorsements.Value);
            Assert.Null(response.Document.Inference.Prediction.Class.Value);
            Assert.Null(response.Document.Inference.Prediction.Sex.Value);
            Assert.Null(response.Document.Inference.Prediction.Height.Value);
            Assert.Null(response.Document.Inference.Prediction.Weight.Value);
            Assert.Null(response.Document.Inference.Prediction.HairColor.Value);
            Assert.Null(response.Document.Inference.Prediction.EyeColor.Value);
            Assert.Null(response.Document.Inference.Prediction.DdNumber.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/us/driver_license/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/us/driver_license/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
            Assert.Equal(0, response.Document.Inference.Pages[0].Orientation.Value);
        }

        private static async Task<PredictResponse<DriverLicenseV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/us/driver_license/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<DriverLicenseV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
