using Mindee.Parsing;
using Mindee.Parsing.LicensePlates;

namespace Mindee.UnitTests.Parsing.Eu.LicensePlates
{
    [Trait("Category", "EU License plates V1")]
    public class LicensePlatesV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForLicensePlates();
            var prediction = await mindeeAPi.PredictAsync<LicensePlatesV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/eu/license_plate/response_v1/doc_to_string.txt");

            Assert.Equal(
                ParsingTestBase.CleaningFilenameFromResult(expected),
                prediction.Inference.Prediction.ToString());
        }

        private MindeeApi GetMindeeApiForLicensePlates(string fileName = "Resources/eu/license_plate/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
