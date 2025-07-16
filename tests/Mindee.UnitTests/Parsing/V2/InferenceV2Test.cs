using Mindee.Parsing.V2;
using Mindee.Parsing.V2.Field;

namespace Mindee.UnitTests.Parsing.V2
{
    [Trait("Category", "InferenceV2")]
    [Trait("Category", "V2")]
    public class InferenceV2Test
    {
        [Fact]
        public async Task AsyncPredict_WhenEmpty_MustHaveValidProperties()
        {
            var response = await GetAsyncPrediction("blank");
            var fields = response.Inference.Result.Fields;
            Assert.Equal(21, fields.Count);
            Assert.Empty(fields["taxes"].ListField.Items);
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
            Assert.NotNull(fields["taxes"].ToString());
            Assert.Equal(3, fields["taxes"].ListField.Items.First().ObjectField.Fields.Count);
            Assert.Equal(31.5, fields["taxes"].ListField.Items.First().ObjectField.Fields["base"].SimpleField.Value);

            Assert.NotNull(fields["supplier_address"].ObjectField);
            Assert.NotNull(fields["supplier_address"].ObjectField.Fields["country"]);
            Assert.Equal("USA", fields["supplier_address"].ObjectField.Fields["country"].SimpleField.Value);
            Assert.Equal("USA", fields["supplier_address"].ObjectField.Fields["country"].ToString());

            Assert.NotNull(fields["supplier_address"].ToString());
        }

        private static async Task<AsyncInferenceResponse> GetAsyncPrediction(string name)
        {
            string fileName = $"Resources/v2/products/financial_document/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApiV2(fileName);
            return await mindeeAPi.GetInferenceFromQueueAsync("jobid");
        }
    }
}
