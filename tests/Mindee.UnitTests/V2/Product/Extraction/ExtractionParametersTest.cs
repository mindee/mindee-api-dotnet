using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using Mindee.V2.Product;
using Mindee.V2.Product.Extraction.Params;

namespace Mindee.UnitTests.V2.Product.Extraction
{
    [Trait("Category", "V2")]
    [Trait("Category", "ExtractionInference Parameter")]
    public class ExtractionParametersTest
    {
        private const string ModelId = "test-model-id";

        [Fact(DisplayName = "should init with minimum values")]
        public void Parameters_MustInit()
        {
            var productParams = new ExtractionParameters(ModelId);
            Assert.Equal(ModelId, productParams.ModelId);

            var productAttributes = productParams.GetType().GetCustomAttribute<ProductAttributes>();
            Assert.Equal("extraction", productAttributes?.Slug);
        }

        [Trait("Feature", "Data Schema")]
        public class DataSchemaTests
        {
            private const string ReplacePath = Constants.V2ProductPath + "extraction/data_schema_replace_param.json";
            private readonly Dictionary<string, object> DataSchemaDict;
            private readonly DataSchema DataSchemaInstance;
            private readonly string DataSchemaString;

            public DataSchemaTests()
            {
                var fileContent = File.ReadAllText(ReplacePath).Trim();
                DataSchemaDict = JsonSerializer.Deserialize<Dictionary<string, object>>(fileContent) ??
                                 new Dictionary<string, object>();
                DataSchemaString = JsonSerializer.Serialize(DataSchemaDict,
                    new JsonSerializerOptions
                    {
                        WriteIndented = false,
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });
                DataSchemaInstance = new DataSchema(DataSchemaDict);
            }

            [Fact(DisplayName = "should leave unset when not provided")]
            public void DataSchema_ShouldLeaveUnsetWhenNotProvided()
            {
                var inferenceParameters = new ExtractionParameters(ModelId);
                Assert.Null(inferenceParameters.DataSchema);
            }

            [Fact(DisplayName = "should initialize from a string")]
            public void DataSchemaString_ShouldInitialize()
            {
                var inferenceParameters = new ExtractionParameters(ModelId, dataSchema: DataSchemaString);
                Assert.Equal(DataSchemaString, inferenceParameters.DataSchema.ToString());
            }

            [Fact(DisplayName = "should initialize from a dictionary")]
            public void DataSchemaDict_ShouldInitialize()
            {
                var inferenceParameters = new ExtractionParameters(ModelId, dataSchema: DataSchemaDict);
                Assert.Equal(DataSchemaString, inferenceParameters.DataSchema.ToString());
            }

            [Fact(DisplayName = "should initialize from an object instance")]
            public void DataSchemaInstance_ShouldInitialize()
            {
                var inferenceParameters = new ExtractionParameters(ModelId, dataSchema: DataSchemaInstance);
                Assert.Equal(DataSchemaString, inferenceParameters.DataSchema.ToString());
            }
        }
    }
}
