using Mindee.Http;
using Mindee.Parsing.Custom.LineItem;
using Mindee.Product.Custom;

namespace Mindee.UnitTests.Parsing.CustomBuilder
{
    [Trait("Category", "Custom API - Line items")]
    public class CustomV1WithLineItemsTest
    {
        [Fact]
        public async Task BuildLineItems_SingeTable01()
        {
            var mindeeAPi = GetMindeeApiForCustom(
                fileName: "Resources/custom/response_v1/line_items/single_table_01.json");
            var fieldNamesToLineItems = new List<string>()
            {
                "beneficiary_name",
                "beneficiary_birth_date",
                "beneficiary_number",
                "beneficiary_rank"
            };

            var response = await mindeeAPi.PredictPostAsync<CustomV1>(
                new CustomEndpoint("customProduct", "fakeOrg"),
                ParsingTestBase.GetFakePredictParameter());

            var lineItems = LineItemsGenerator.Generate(
                new Anchor("beneficiary_name", 0.011d),
                fieldNamesToLineItems,
                response.Document.Inference.Prediction.Fields);

            Assert.NotNull(lineItems);
            Assert.Equal(3, lineItems.Lines.Count());
            var firstLine = lineItems.Lines.First();
            Assert.Equal(4, firstLine.Fields.Count);
            Assert.True(firstLine.Fields.ContainsKey("beneficiary_birth_date"));
            Assert.True(firstLine.Fields.ContainsKey("beneficiary_number"));
            Assert.True(firstLine.Fields.ContainsKey("beneficiary_name"));
            Assert.True(firstLine.Fields.ContainsKey("beneficiary_rank"));
            Assert.Equal(4, lineItems.Lines.Last().Fields.Count);
            Assert.Equal("2010-07-18", lineItems.Lines.Skip(1).First().Fields["beneficiary_birth_date"].Content);
            Assert.Equal("3", lineItems.Lines.Last().Fields["beneficiary_rank"].Content);
        }

        [Fact]
        public async Task BuildLineItems_MultipleTables01()
        {
            var mindeeAPi = GetMindeeApiForCustom(
                fileName: "Resources/custom/response_v1/line_items/multiple_tables_01.json");

            var earningsAnchor = new Anchor(name: "earnings_description", tolerance: 0.002d);
            var earningsFields = new List<string>
            {
                "earnings_description",
                "earnings_rate",
                "earnings_hours",
                "earnings_amount",
                "earnings_ytd_amt",
                "earnings_ytd_hrs"
            };

            var taxesAnchor = new Anchor(name: "taxes_description", tolerance: 0.002d);
            var taxesFields = new List<string>(new string[] {
                "taxes_description",
                "taxes_amount",
                "taxes_ytd_amt",
            });

            var response = await mindeeAPi.PredictPostAsync<CustomV1>(
                new CustomEndpoint("customProduct", "fakeOrg"),
                ParsingTestBase.GetFakePredictParameter());

            var earningsTable = LineItemsGenerator.Generate(
                earningsAnchor,
                earningsFields,
                response.Document.Inference.Prediction.Fields);
            Assert.NotNull(earningsTable);
            Assert.Equal(4, earningsTable.Lines.Count());
            var earningsFirstLine = earningsTable.Lines.First();
            Assert.Equal(6, earningsFirstLine.Fields.Count);

            var taxesTable = LineItemsGenerator.Generate(
                taxesAnchor,
                taxesFields,
                response.Document.Inference.Prediction.Fields);
            Assert.NotNull(taxesTable);
            Assert.Equal(7, taxesTable.Lines.Count());
            var taxesLastLine = taxesTable.Lines.Last();
            Assert.Equal(3, taxesLastLine.Fields.Count);
            Assert.Equal("ZZ Disability", taxesLastLine.Fields["taxes_description"].Content);
        }

        [Fact]
        public async Task BuildLineItems_MultipleTables02()
        {
            var mindeeAPi = GetMindeeApiForCustom(
                fileName: "Resources/custom/response_v1/line_items/multiple_tables_02.json");

            var earningsAnchor = new Anchor(name: "earnings_description", tolerance: 0.002d);
            var earningsFields = new List<string>
            {
                "earnings_description",
                "earnings_rate",
                "earnings_hours",
                "earnings_amount",
                "earnings_ytd_amt",
                "earnings_ytd_hrs"
            };

            var taxesAnchor = new Anchor(name: "taxes_description", tolerance: 0.002d);
            var taxesFields = new List<string>(new string[] {
                "taxes_description",
                "taxes_amount",
                "taxes_ytd_amt",
            });

            var response = await mindeeAPi.PredictPostAsync<CustomV1>(
                new CustomEndpoint("customProduct", "fakeOrg"),
                ParsingTestBase.GetFakePredictParameter());

            var earningsTable = LineItemsGenerator.Generate(
                earningsAnchor,
                earningsFields,
                response.Document.Inference.Prediction.Fields);
            Assert.NotNull(earningsTable);
            Assert.Equal(2, earningsTable.Lines.Count());
            var earningsFirstLine = earningsTable.Lines.First();
            Assert.Equal(4, earningsFirstLine.Fields.Count);

            var taxesTable = LineItemsGenerator.Generate(
                taxesAnchor,
                taxesFields,
                response.Document.Inference.Prediction.Fields);
            Assert.NotNull(taxesTable);
            Assert.Equal(7, taxesTable.Lines.Count());
            var taxesLastLine = taxesTable.Lines.Last();
            Assert.Equal(3, taxesLastLine.Fields.Count);
            Assert.Equal("ZZ Disability", taxesLastLine.Fields["taxes_description"].Content);
        }

        private MindeeApi GetMindeeApiForCustom(string fileName = "Resources/custom/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
