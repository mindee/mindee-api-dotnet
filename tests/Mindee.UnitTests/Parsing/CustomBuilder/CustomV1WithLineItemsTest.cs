using Mindee.Parsing;
using Mindee.Parsing.CustomBuilder;
using Mindee.Parsing.CustomBuilder.Table;

namespace Mindee.UnitTests.Parsing.CustomBuilder
{
    [Trait("Category", "Custom API - Line items")]
    public class CustomV1WithLineItemsTest
    {
        [Fact]
        public async Task Predict_ThenBuildLineItems_Expected_3_Lines()
        {
            var mindeeAPi = GetMindeeApiForCustom("Resources/custom/response_v1/line_items/with_defined_anchor_3_expected_lines.json");
            var fieldNamesToLineItems = new List<string>() { "beneficiary_birth_date", "beneficiary_number", "beneficiary_name", "beneficiary_rank" };

            var document = await mindeeAPi.PredictAsync<CustomV1Inference>(
                new CustomEndpoint("customProduct", "fakeOrga"),
                ParsingTestBase.GetFakePredictParameter());

            var lineItems = LineItemsGenerator.Generate(
                fieldNamesToLineItems,
                document.Inference.DocumentPrediction.Fields,
                new Anchor("beneficiary_name", 0.011d));

            Assert.NotNull(lineItems);
            Assert.Equal(3, lineItems.Rows.Count());
            Assert.Equal(4, lineItems.Rows.First().Fields.Count);
            Assert.True(lineItems.Rows.First().Fields.ContainsKey("beneficiary_birth_date"));
            Assert.True(lineItems.Rows.First().Fields.ContainsKey("beneficiary_number"));
            Assert.True(lineItems.Rows.First().Fields.ContainsKey("beneficiary_name"));
            Assert.True(lineItems.Rows.First().Fields.ContainsKey("beneficiary_rank"));
            Assert.Equal(4, lineItems.Rows.Last().Fields.Count);
            Assert.Equal("2010-07-18", lineItems.Rows.Skip(1).First().Fields["beneficiary_birth_date"].Value);
            Assert.Equal("3", lineItems.Rows.Last().Fields["beneficiary_rank"].Value);
        }

        private MindeeApi GetMindeeApiForCustom(string fileName = "Resources/custom/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
