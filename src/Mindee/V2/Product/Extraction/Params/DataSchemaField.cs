using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Extraction.Params
{/// <summary>
 ///     Data Schema Field
 /// </summary>
    public class DataSchemaField
    {
        /// <summary>
        ///     Name of the field in the data schema.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Display name for the field. Also impacts inference results.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///     Whether this field can contain multiple values.
        /// </summary>
        [JsonPropertyName("is_array")]
        public bool IsArray { get; set; }

        /// <summary>
        ///     Data type of the field.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Detailed description of what this field represents.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Optional extraction guidelines.
        /// </summary>
        [JsonPropertyName("guidelines")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Guidelines { get; set; }

        /// <summary>
        ///     Whether to remove duplicate values in the array.
        ///     Only applicable if `is_array` is True.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("unique_values")]
        public bool? UniqueValues { get; set; }

        /// <summary>
        ///     Subfields when type is `nested_object`. Leave empty for other types.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("nested_fields")]
        public Dictionary<string, object> NestedFields { get; set; }

        /// <summary>
        ///     Allowed values when type is `classification`. Leave empty for other types.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("classification_values")]
        public List<string> ClassificationValues { get; set; }

        /// <summary>
        ///     Converts the DataSchemaReplace to a JSON string preserving key order.
        /// </summary>
        /// <returns>JSON string representation of the DataSchemaReplace.</returns>
        public override string ToString()
        {
            var options =
                new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
