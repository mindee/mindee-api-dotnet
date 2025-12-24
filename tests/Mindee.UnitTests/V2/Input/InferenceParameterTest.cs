using System.Text.Encodings.Web;
using System.Text.Json;
using Mindee.Input;

namespace Mindee.UnitTests.V2.Input
{
    [Trait("Category", "Inference Parameter")]
    [Trait("Category", "V2")]
    public class InferenceParameterTest
    {
        private const string ReplacePath = Constants.V2RootDir + "inference/data_schema_replace_param.json";
        private string DataSchemaString;
        private Dictionary<string, object> DataSchemaDict;
        private DataSchema DataSchemaInstance;

        public InferenceParameterTest()
        {
            var fileContent = File.ReadAllText(ReplacePath).Trim();
            DataSchemaDict = JsonSerializer.Deserialize<Dictionary<string, object>>(fileContent) ?? new Dictionary<string, object>();
            DataSchemaString = JsonSerializer.Serialize(DataSchemaDict, new JsonSerializerOptions
            {
                WriteIndented = false,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            DataSchemaInstance = new DataSchema(DataSchemaDict);
        }
        [Fact]
        public void DataSchemaNull_ShouldInitializeWithoutIssues()
        {
            InferenceParameters inferenceParameters = new InferenceParameters(modelId: "test");
            Assert.Null(inferenceParameters.DataSchema);
        }
        [Fact]
        public void DataSchemaStr_ShouldInitialize()
        {
            InferenceParameters inferenceParameters = new InferenceParameters(modelId: "test", dataSchema: DataSchemaString);
            Assert.Equal(DataSchemaString, inferenceParameters.DataSchema.ToString());
        }
        [Fact]
        public void DataSchemaDict_ShouldInitialize()
        {
            InferenceParameters inferenceParameters = new InferenceParameters(modelId: "test", dataSchema: DataSchemaDict);
            Assert.Equal(DataSchemaString, inferenceParameters.DataSchema.ToString());
        }
        [Fact]
        public void DataSchemaInstance_ShouldInitialize()
        {
            InferenceParameters inferenceParameters = new InferenceParameters(modelId: "test", dataSchema: DataSchemaInstance);
            Assert.Equal(DataSchemaString, inferenceParameters.DataSchema.ToString());
        }
    }
}
