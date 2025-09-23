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
            InferenceActiveOptions activeOptions = response.Inference.ActiveOptions;
            Assert.NotNull(activeOptions);
            Assert.False(activeOptions.Rag);
            Assert.False(activeOptions.Polygon);
            Assert.False(activeOptions.Confidence);
            Assert.False(activeOptions.RawText);

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
            var file = response.Inference.File;
            Assert.Equal("complete.jpg", file.Name);
            Assert.Equal(1, file.PageCount);
            Assert.Equal("image/jpeg", file.MimeType);
            Assert.Null(file.Alias);
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

        [Fact(DisplayName = "standard_field_types.json – file metadata must be recognised")]
        public void StandardFieldTypes_mustExposeFileValues()
        {
            var resp = GetInference("Resources/v2/inference/standard_field_types.json");
            Inference? inference = resp.Inference;
            Assert.NotNull(inference);
            InferenceFile file = inference.File;
            Assert.NotNull(file);

            string fileName = file.Name;
            Assert.Equal("test-file-name.jpg", fileName);

            string mimeType = file.MimeType;
            Assert.Equal("image/jpeg", mimeType);

            int pageCount = file.PageCount;
            Assert.Equal(1, pageCount);

            string? alias = file.Alias;
            Assert.Null(alias);
        }

        [Fact(DisplayName = "standard_field_types.json – simple fields must be recognised")]
        public void StandardFieldTypes_mustExposeSimpleFieldValues()
        {
            var resp = GetInference("Resources/v2/inference/standard_field_types.json");
            Inference? inference = resp.Inference;
            Assert.NotNull(inference);
            InferenceFields fields = inference.Result.Fields;

            Assert.NotNull(fields["field_simple_string"].SimpleField);
            string fieldSimpleStringValue = fields["field_simple_string"].SimpleField.Value;
            Assert.Equal("field_simple_string-value", fieldSimpleStringValue);
            Assert.Equal(FieldConfidence.Certain, fields["field_simple_string"].SimpleField.Confidence);

            Assert.NotNull(fields["field_simple_float"].SimpleField);
            Double fieldSimpleFloatValue = fields["field_simple_float"].SimpleField.Value;
            Assert.Equal(1.1, fieldSimpleFloatValue);
            Assert.Equal(FieldConfidence.High, fields["field_simple_float"].SimpleField.Confidence);

            Assert.NotNull(fields["field_simple_int"].SimpleField);
            Double fieldSimpleIntValue = fields["field_simple_int"].SimpleField.Value;
            Assert.Equal(12.0, fieldSimpleIntValue);
            Assert.Equal(FieldConfidence.Medium, fields["field_simple_int"].SimpleField.Confidence);

            Assert.NotNull(fields["field_simple_zero"].SimpleField);
            Assert.Equal(0.0, fields["field_simple_zero"].SimpleField.Value);
            Assert.Equal(FieldConfidence.Low, fields["field_simple_zero"].SimpleField.Confidence);

            Assert.NotNull(fields["field_simple_bool"].SimpleField);
            Boolean fieldSimpleBoolValue = fields["field_simple_bool"].SimpleField.Value;
            Assert.True(fieldSimpleBoolValue);
            Assert.Null(fields["field_simple_bool"].SimpleField.Confidence);

            Assert.NotNull(fields["field_simple_null"].SimpleField);
            Assert.Null(fields["field_simple_null"].SimpleField.Value);
            Assert.Null(fields["field_simple_null"].SimpleField.Confidence);
        }

        [Fact(DisplayName = "standard_field_types.json – simple list fields must be recognised")]
        public void StandardFieldTypes_mustExposeSimpleListFieldValues()
        {
            var resp = GetInference("Resources/v2/inference/standard_field_types.json");
            Inference? inference = resp.Inference;
            Assert.NotNull(inference);
            InferenceFields fields = inference.Result.Fields;

            ListField fieldSimpleList = fields["field_simple_list"].ListField;
            Assert.NotNull(fieldSimpleList);
            Assert.Null(fieldSimpleList.Confidence);

            List<SimpleField> simpleItems = fieldSimpleList.SimpleItems;
            Assert.Equal(2, simpleItems.Count);
            foreach (SimpleField itemField in simpleItems)
            {
                string fieldValue = itemField.Value;
                Assert.NotNull(fieldValue);
            }
        }

        [Fact(DisplayName = "standard_field_types.json – object fields must be recognised")]
        public void StandardFieldTypes_mustExposeObjectFieldValues()
        {
            var resp = GetInference("Resources/v2/inference/standard_field_types.json");
            Inference? inference = resp.Inference;
            Assert.NotNull(inference);
            InferenceFields fields = inference.Result.Fields;

            ObjectField objectField = fields["field_object"].ObjectField;
            Assert.NotNull(objectField);

            Dictionary<string, SimpleField> subFields = objectField.SimpleFields;
            SimpleField subField1 = subFields["subfield_1"];
            Assert.Equal(FieldConfidence.High, subField1.Confidence);

            foreach (var entry in subFields)
            {
                string fieldName = entry.Key;
                SimpleField subField = entry.Value;
                Assert.StartsWith("subfield_", fieldName);
                Assert.NotNull(subField.Value);
            }
        }

        [Fact(DisplayName = "standard_field_types.json – simple list fields must be recognised")]
        public void StandardFieldTypes_mustExposeObjectListFieldValues()
        {
            var resp = GetInference("Resources/v2/inference/standard_field_types.json");
            Inference? inference = resp.Inference;
            Assert.NotNull(inference);
            InferenceFields fields = inference.Result.Fields;

            ListField fieldObjectList = fields["field_object_list"].ListField;
            Assert.NotNull(fieldObjectList);
            Assert.Null(fieldObjectList.Confidence);

            List<ObjectField> objectItems = fieldObjectList.ObjectItems;
            Assert.Equal(2, objectItems.Count);

            foreach (ObjectField itemField in objectItems)
            {
                InferenceFields subFields = itemField.Fields;

                SimpleField subField1 = subFields["subfield_1"].SimpleField;
                string subFieldValue = subField1.Value;
                Assert.StartsWith("field_object_list", subFieldValue);

                foreach (var entry in subFields)
                {
                    string fieldName = entry.Key;
                    SimpleField subField = entry.Value.SimpleField;
                    Assert.StartsWith("subfield_", fieldName);
                    Assert.NotNull(subField.Value);
                }
            }
        }

        [Fact]
        public void AsyncPredict_WhenComplete_MustHaveRawText()
        {
            InferenceResponse response = GetInference("Resources/v2/inference/raw_texts.json");

            InferenceActiveOptions activeOptions = response.Inference.ActiveOptions;
            Assert.NotNull(activeOptions);
            Assert.False(activeOptions.Rag);
            Assert.False(activeOptions.Polygon);
            Assert.False(activeOptions.Confidence);
            Assert.True(activeOptions.RawText);

            RawText rawText = response.Inference.Result.RawText;
            Assert.NotNull(rawText);
            Assert.NotNull(rawText.Pages);
            Assert.Equal(2, rawText.Pages.Count);
            foreach (RawTextPage page in rawText.Pages)
            {
                Assert.NotNull(page.Content);
            }
            Assert.Equal("This is the raw text of the first page...", rawText.Pages[0].Content);
            Assert.Equal(
                File.ReadAllText("Resources/v2/inference/raw_texts.txt"),
                rawText.ToString());
        }

        [Fact(DisplayName = "standard_field_types.json - locations must be recognised")]
        public void StandardFieldTypes_mustHaveLocations()
        {
            var resp = GetInference("Resources/v2/inference/standard_field_types.json");
            Inference? inf = resp.Inference;
            InferenceFields fields = inf.Result.Fields;
            Assert.NotNull(inf);
            SimpleField simpleField = fields["field_simple_string"].SimpleField;
            Assert.Equal(FieldConfidence.Certain, simpleField.Confidence);
            Assert.NotNull(simpleField.Locations);
            List<FieldLocation> locations = simpleField.Locations;
            Assert.Single(locations);
            Assert.Equal(0, locations.First().Page);
            Polygon polygon = locations.First().Polygon;
            Assert.Equal(new Point(0, 0), polygon[0]);
            Assert.Equal(new Point(0, 0), polygon[1]);
            Assert.Equal(new Point(1, 1), polygon[2]);
            Assert.Equal(new Point(1, 1), polygon[3]);
        }

        [Fact(DisplayName = "standard_field_types.rst – RST display must be parsed and exposed")]
        public void RstDisplay_mustBeAccessible()
        {
            // Arrange
            var resp = GetInference("Resources/v2/inference/standard_field_types.json");
            var rstReference = File.ReadAllText("Resources/v2/inference/standard_field_types.rst");

            Inference inf = resp.Inference;

            Assert.NotNull(inf);

            Assert.Equal(
                NormalizeLineEndings(rstReference),
                NormalizeLineEndings(inf.ToString())
            );

        }

        /// <summary>
        /// Ensures all line endings are identical before comparison so the test
        /// behaves the same on every platform (LF vs CRLF).
        /// </summary>
        private static string NormalizeLineEndings(string input) =>
            input.Replace("\r\n", "\n").Replace("\r", "\n");


        private static InferenceResponse GetInference(string path)
        {
            var localResponse = new LocalResponse(File.ReadAllText(path));
            return localResponse.DeserializeResponse<InferenceResponse>();
        }
    }
}
