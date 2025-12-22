using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Mindee.Exceptions;

namespace Mindee.Input
{
    /// <summary>
    /// Data Schema Field
    /// </summary>
    public class DataSchemaField
    {
        /// <summary>
        /// Name of the field in the data schema.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Display name for the field. Also impacts inference results.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Whether this field can contain multiple values.
        /// </summary>
        [JsonPropertyName("is_array")]
        public bool IsArray { get; set; }

        /// <summary>
        /// Data type of the field.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Detailed description of what this field represents.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Detailed description of what this field represents.
        /// </summary>
        [JsonPropertyName("guidelines")]
        public string Guidelines { get; set; }

        /// <summary>
        /// Whether to remove duplicate values in the array.
        /// Only applicable if `is_array` is True.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("unique_values")]
        public bool? UniqueValues { get; set; }

        /// <summary>
        /// Optional extraction guidelines.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("nested_fields")]
        public Dictionary<string, object> NestedFields { get; set; }

        /// <summary>
        /// Allowed values when type is `classification`. Leave empty for other types.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("classification_values")]
        public List<string> ClassificationValues { get; set; }

        /// <summary>
        /// Converts the DataSchemaReplace to a JSON string preserving key order.
        /// </summary>
        /// <returns>JSON string representation of the DataSchemaReplace.</returns>
        public override string ToString()
        {
            JsonSerializerOptions options =
                new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            return JsonSerializer.Serialize(this, options);
        }
    }

    /// <summary>
    /// The structure to completely replace the data schema of the model.
    /// </summary>
    public class DataSchemaReplace
    {
        /// <summary>
        /// List of fields in the Data Schema.
        /// </summary>
        [JsonPropertyName("fields")]
        public List<DataSchemaField> Fields { get; set; }

        /// <summary>
        /// Converts the DataSchemaReplace to a JSON string preserving key order.
        /// </summary>
        /// <returns>JSON string representation of the DataSchemaReplace.</returns>
        public override string ToString()
        {
            JsonSerializerOptions options =
                new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            return JsonSerializer.Serialize(this, options);
        }
    }

    /// <summary>
    /// Modify the Data Schema.
    /// </summary>
    public class DataSchema
    {
        /// <summary>
        /// If set, completely replaces the data schema of the model.
        /// </summary>
        [JsonPropertyName("replace")]
        public DataSchemaReplace Replace { get; set; }

        /// <summary>
        /// Initializes the Replace property.
        /// </summary>
        /// <param name="dataSchema"></param>
        /// <exception cref="MindeeInputException"></exception>
        private void Setup(Dictionary<string, object> dataSchema)
        {
            if (dataSchema == null)
                return;
            if (!dataSchema.TryGetValue("replace", out var value))
            {
                throw new MindeeInputException("Invalid Data Schema format.");
            }
            Replace = JsonSerializer.Deserialize<DataSchemaReplace>(
                JsonSerializer.Serialize(value)
            );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSchema"/> class from a dictionary.
        /// </summary>
        /// <param name="dataSchema">Dictionary containing the data schema configuration.</param>
        public DataSchema(Dictionary<string, object> dataSchema)
        {
            Setup(dataSchema);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSchema"/> class from a JSON string.
        /// </summary>
        /// <param name="jsonString">JSON string containing the data schema configuration.</param>
        public DataSchema(string jsonString)
        {
            var dataSchema = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
            Setup(dataSchema);
        }

        /// <summary>
        /// Converts the DataSchema to a JSON string preserving key order.
        /// </summary>
        /// <returns>JSON string representation of the DataSchema.</returns>
        public override string ToString()
        {
            JsonSerializerOptions options =
                new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
