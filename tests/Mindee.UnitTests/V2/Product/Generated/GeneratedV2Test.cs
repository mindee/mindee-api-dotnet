using Mindee.Parsing.V2;

namespace Mindee.UnitTests.V2.Product.Generated
{
    [Trait("Category", "GeneratedV2")]
    [Trait("Category", "V22")]
    public class GeneratedV2Test
    {
        [Fact]
        public async Task AsyncPredict_WhenEmpty_MustHaveValidProperties()
        {
            var response = await GetAsyncPrediction("blank");
            var fields = response.Inference.Result.Fields;
            Assert.Equal(21, fields.Count);
            Assert.Empty(fields["supplier_company_registration"].ListField.Items);
            Assert.NotNull(fields["supplier_address"].ObjectField);

            foreach (var field in fields)
            {
                if (field.Value == null)
                {
                    continue;
                }

                if (field.Value.Type == FieldType.ListField)
                {
                    Assert.NotNull(field.Value.ListField);
                    Assert.Null(field.Value.ObjectField);
                    Assert.Null(field.Value.SimpleField);
                }
                else if (field.Value.Type == FieldType.ObjectField)
                {
                    Assert.NotNull(field.Value.ObjectField);
                    Assert.Null(field.Value.ListField);
                    Assert.Null(field.Value.SimpleField);
                }
                else
                {
                    Assert.NotNull(field.Value.SimpleField);
                   Assert.Null(field.Value.ListField);
                   Assert.Null(field.Value.ObjectField);
                }
            }
        }

        [Fact]
        public async Task AsyncPredict_WhenComplete_MustHaveValidProperties()
        {
            var response = await GetAsyncPrediction("complete");
            var fields = response.Inference.Result.Fields;
            Assert.Equal(21, fields.Count);
            Assert.Single(fields["taxes"].ListField.Items);
            Assert.Equal(3, fields["taxes"].ListField.Items.First().ObjectField.Fields.Count);
            Assert.Equal(31.5, fields["taxes"].ListField.Items.First().ObjectField.Fields["base"].Value);
        }

        private static async Task<AsyncInferenceResponse> GetAsyncPrediction(string name)
        {
            string fileName = $"Resources/v2/products/financial_document/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApiV2(fileName);
            return await mindeeAPi.DocumentQueueGetAsync("jobid");
        }
    }
}
