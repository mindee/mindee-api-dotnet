using Mindee.Parsing.Common;

namespace Mindee.UnitTests.V2.Product.Generated
{
    [Trait("Category", "GeneratedV2")]
    [Trait("Category", "V2")]
    public class GeneratedV2Test
    {
        [Fact]
        public async Task AsyncPredict_WhenEmpty_MustHaveValidProperties()
        {
            var response = await GetAsyncPrediction("blank");
            var features = response.Inference.Result.Fields;
            Assert.Equal(21, features.Count);
            Assert.True(features["supplier_company_registration"].IsList);
            Assert.Equal(9, features["supplier_address"].Fields.Count);

            foreach (var field in features)
            {
                if (field.Value == null)
                {
                    continue;
                }

                if (field.Value.IsList)
                {
                    Assert.Empty(field.Value);
                }
                else
                {
                    if (field.Value?.Count > 0)
                    {
                        Assert.Null(field.Value.First()["value"].GetString());
                    }
                }
            }
        }

        private static async Task<AsyncPredictResponseV2> GetAsyncPrediction(string name)
        {
            string fileName = $"Resources/v2/products/financial_document/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApiV2(fileName);
            return await mindeeAPi.DocumentQueueGetAsync("jobid");
        }
    }
}
