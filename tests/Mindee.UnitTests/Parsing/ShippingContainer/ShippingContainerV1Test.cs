using Mindee.Parsing;
using Mindee.Parsing.ShippingContainer;

namespace Mindee.UnitTests.Parsing.ShippingContainer
{
    [Trait("Category", "Shipping Container")]
    public class ShippingContainerV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForCarteVitale();
            var prediction = await mindeeAPi.PredictAsync<ShippingContainerV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/shipping_container/response_v1/doc_to_string.txt");

            Assert.Equal(
                CleaningResult(expected),
                prediction.Inference.Prediction.ToString());
        }

        private string CleaningResult(string expectedSummary)
        {
            var indexFilename = expectedSummary.IndexOf("Filename");
            var indexFilenameEOL = expectedSummary.IndexOf("\n", indexFilename);
            string cleanedSummary = expectedSummary.Remove(indexFilename, indexFilenameEOL - indexFilename + 1);

            return cleanedSummary;
        }

        private MindeeApi GetMindeeApiForCarteVitale(string fileName = "Resources/shipping_container/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
