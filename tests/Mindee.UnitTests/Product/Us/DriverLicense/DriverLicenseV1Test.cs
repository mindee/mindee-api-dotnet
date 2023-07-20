using Mindee.Parsing.Common;
using Mindee.Product.Us.DriverLicense;

namespace Mindee.UnitTests.Product.Us.DriverLicense
{
    [Trait("Category", "DriverLicenseV1")]
    public class DriverLicenseV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/us/driver_license/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/us/driver_license/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
            Assert.Equal(0, response.Document.Inference.Pages[0].Orientation.Value);
        }

        private static async Task<PredictResponse<DriverLicenseV1>> GetPrediction()
        {
            const string fileName = "Resources/us/driver_license/response_v1/complete.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<DriverLicenseV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
