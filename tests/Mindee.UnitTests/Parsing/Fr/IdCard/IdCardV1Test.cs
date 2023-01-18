using Mindee.Parsing;
using Mindee.Parsing.Fr.IdCard;

namespace Mindee.UnitTests.Parsing.Fr.IdCard
{
    [Trait("Category", "FR Id Card V1")]
    public class IdCardV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForFrIdCard();
            var prediction = await mindeeAPi.PredictAsync<IdCardV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/fr/id_card/response_v1/doc_to_string.txt");

            Assert.Equal(
                CleaningResult(expected),
                prediction.Inference.Prediction.ToString());
        }

        [Fact]
        public async Task Predict_CheckSummary_WithMultiplePages()
        {
            var mindeeAPi = GetMindeeApiForFrIdCard();
            var prediction = await mindeeAPi.PredictAsync<IdCardV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/fr/id_card/response_v1/page0_to_string.txt");

            Assert.Equal(
                CleaningResult(expected),
                prediction.Inference.Prediction.ToString());
        }

        private string CleaningResult(string expectedSummary)
        {
            string cleanedSummary = ParsingTestBase.CleaningFilenameFromResult(expectedSummary);

            // must be deleted when generic wil be place on the inference node
            var indexDocumentSide = cleanedSummary.IndexOf("Document side");
            var indexDocumentSideEOL = cleanedSummary.IndexOf("\n", indexDocumentSide);
            cleanedSummary = cleanedSummary.Remove(indexDocumentSide, indexDocumentSideEOL - indexDocumentSide + 1);

            return cleanedSummary;
        }

        private MindeeApi GetMindeeApiForFrIdCard(string fileName = "Resources/fr/id_card/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
