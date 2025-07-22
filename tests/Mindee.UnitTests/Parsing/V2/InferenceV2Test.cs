using Mindee.Geometry;
using Mindee.Input;
using Mindee.Parsing.V2;
using Mindee.Parsing.V2.Field;

namespace Mindee.UnitTests.Parsing.V2
{
    [Trait("Category", "InferenceV2")]
    [Trait("Category", "V2")]
    public class InferenceV2Test
    {
        [Fact]
        public void AsyncPredict_WhenEmpty_MustHaveValidProperties()
        {
            var response = GetInference("Resources/v2/products/financial_document/blank.json");
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
        public void AsyncPredict_WhenComplete_MustHaveValidProperties()
        {
            var response = GetInference("Resources/v2/products/financial_document/complete.json");
            var fields = response.Inference.Result.Fields;
            Assert.Equal(21, fields.Count);
            Assert.Single(fields["taxes"].ListField.Items);
            Assert.NotNull(fields["taxes"].ToString());
            Assert.Equal(3, fields["taxes"].ListField.Items.First().ObjectField.Fields.Count);
            Assert.Equal(31.5, fields["taxes"].ListField.Items.First().ObjectField.Fields["base"].SimpleField.Value);
            Assert.Equal(195.0, fields["total_net"].SimpleField.Value);
            Assert.Null(fields["tips_gratuity"].SimpleField.Value);


            Assert.NotNull(fields["supplier_address"].ObjectField);
            Assert.NotNull(fields["supplier_address"].ObjectField.Fields["country"]);
            Assert.Equal("USA", fields["supplier_address"].ObjectField.Fields["country"].SimpleField.Value);
            Assert.Equal("USA", fields["supplier_address"].ObjectField.Fields["country"].ToString());

            Assert.NotNull(fields["supplier_address"].ToString());
        }


        [Fact(DisplayName = "deep_nested_fields.json – all nested structures must be typed correctly")]
        public void DeepNestedFields_mustExposeCorrectTypes()
        {
            var resp = GetInference("Resources/v2/inference/deep_nested_fields.json");
            Inference? inf = resp.Inference;
            Assert.NotNull(inf);

            InferenceFields fields = inf.Result.Fields;
            Assert.NotNull(fields["field_simple"].SimpleField);
            Assert.NotNull(fields["field_object"].ObjectField);

            ObjectField fieldObject = fields["field_object"].ObjectField!;
            InferenceFields lvl1 = fieldObject.Fields;
            Assert.NotNull(lvl1["sub_object_list"].ListField);
            Assert.NotNull(lvl1["sub_object_object"].ObjectField);

            ObjectField subObjectObject = lvl1["sub_object_object"].ObjectField!;
            InferenceFields lvl2 = subObjectObject.Fields;
            Assert.NotNull(lvl2["sub_object_object_sub_object_list"].ListField);

            ListField nestedList = lvl2["sub_object_object_sub_object_list"].ListField!;
            var items = nestedList.Items;
            Assert.NotEmpty(items);
            Assert.NotNull(items.First().ObjectField);

            ObjectField firstItem = items.First().ObjectField!;
            SimpleField deepSimple = firstItem.Fields["sub_object_object_sub_object_list_simple"].SimpleField!;
            Assert.Equal("value_9", deepSimple.Value);
        }


        [Fact(DisplayName = "standard_field_types.json – simple / object / list variants must be recognised")]
        public void StandardFieldTypes_mustExposeCorrectTypes()
        {
            var resp = GetInference("Resources/v2/inference/standard_field_types.json");
            Inference? inf = resp.Inference;
            Assert.NotNull(inf);

            InferenceFields fields = inf.Result.Fields;
            Assert.NotNull(fields["field_simple"].SimpleField);
            Assert.NotNull(fields["field_object"].ObjectField);
            Assert.NotNull(fields["field_simple_list"].ListField);
            Assert.NotNull(fields["field_object_list"].ListField);
        }

        [Fact(DisplayName = "standard_field_types.json - simple / object / list variants must be recognised")]
        public void StandardFieldTypes_mustHaveLocations()
        {
            var resp = GetInference("Resources/v2/inference/standard_field_types.json");
            Inference? inf = resp.Inference;
            InferenceFields fields = inf.Result.Fields;
            Assert.NotNull(inf);
            DynamicField simpleField = fields["field_simple"];
            Assert.Null(simpleField.Confidence);
            Assert.NotNull(simpleField.Locations);
            Assert.Single(simpleField.Locations);
            Assert.Equal(0, simpleField.Locations.First().Page);
            Polygon polygon = simpleField.Locations.First().Polygon;
            Assert.Equal([0, 0], polygon[0]);
            Assert.Equal([0, 0], polygon[1]);
            Assert.Equal([1, 1], polygon[2]);
            Assert.Equal([1, 1], polygon[3]);
        }

        private static InferenceResponse GetInference(string path)
        {
            var localResponse = new LocalResponse(File.ReadAllText(path));
            return localResponse.DeserializeResponse<InferenceResponse>();
        }
    }
}
