using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Mindee.Exceptions;

namespace Mindee.Input
{
    /// <summary>
    /// Display name for the field, also impacts inference results.
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
        /// Allowed values when type is `classification`. Leave empty for other types.
        /// </summary>
        [JsonPropertyName("classification_values")]
        public List<string> ClassificationValues { get; set; }

        /// <summary>
        /// Whether to remove duplicate values in the array.
        /// Only applicable if `is_array` is True.
        /// </summary>
        [JsonPropertyName("unique_values")]
        public Boolean UniqueValues { get; set; }

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
        /// Optional extraction guidelines.
        /// </summary>
        [JsonPropertyName("nested_fields")]
        public Dictionary<string, object> NestedFields { get; set; }

        /// <summary>
        /// Stores the raw JSON element to preserve key order during serialization.
        /// </summary>
        [JsonIgnore]
        internal JsonElement? RawJson { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSchemaField"/> class from a dictionary.
        /// </summary>
        /// <param name="fieldData">Dictionary containing the field configuration.</param>
        public DataSchemaField(Dictionary<string, object> fieldData)
        {
            if (fieldData.TryGetValue("title", out var title))
                Title = title is JsonElement el ? el.GetString() : title?.ToString();
            if (fieldData.TryGetValue("name", out var name))
                Name = name is JsonElement el ? el.GetString() : name?.ToString();
            if (fieldData.TryGetValue("is_array", out var isArray))
                IsArray = isArray is JsonElement el ? el.GetBoolean() : Convert.ToBoolean(isArray);
            if (fieldData.TryGetValue("type", out var type))
                Type = type is JsonElement el ? el.GetString() : type?.ToString();
            if (fieldData.TryGetValue("classification_values", out var classificationValues))
            {
                ClassificationValues = classificationValues switch
                {
                    JsonElement el => el.EnumerateArray().Select(v => v.GetString()).ToList(),
                    IList<object> list => list.Select(v => v?.ToString()).ToList(),
                    _ => null
                };
            }

            if (fieldData.TryGetValue("unique_values", out var uniqueValues))
                UniqueValues = uniqueValues is JsonElement el ? el.GetBoolean() : Convert.ToBoolean(uniqueValues);
            if (fieldData.TryGetValue("description", out var description))
                Description = description is JsonElement el ? el.GetString() : description?.ToString();
            if (fieldData.TryGetValue("guidelines", out var guidelines))
                Guidelines = guidelines is JsonElement el ? el.GetString() : guidelines?.ToString();
            if (fieldData.TryGetValue("nested_fields", out var nestedFields))
                NestedFields = nestedFields as Dictionary<string, object>;
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
        public List<DataSchemaField> Fields { get; set; }

        private void Setup(Dictionary<string, object> dataSchemaReplace)
        {
            if (dataSchemaReplace.TryGetValue("fields", out object fieldsValue) &&
                fieldsValue is JsonElement fieldsElement)
            {

                var doc = JsonDocument.Parse(fieldsElement.GetRawText());
                this.Fields = [];

                foreach (var field in from fieldElement in doc.RootElement.EnumerateArray()
                                      let fieldDict = JsonSerializer.Deserialize<Dictionary<string, object>>(
                                          fieldElement.GetRawText())
                                      select new DataSchemaField(fieldDict) { RawJson = fieldElement.Clone() }
                        )
                {
                    this.Fields.Add(field);
                }

                if (this.Fields.Count == 0)
                {
                    throw new ArgumentException("Data Schema replacement fields cannot be empty.");
                }
            }
            else
            {
                throw new MindeeInputException("Invalid Data Schema.");
            }
        }

        /// <summary>
        /// String constructor.
        /// </summary>
        /// <param name="dataSchemaReplace"></param>
        public DataSchemaReplace(string dataSchemaReplace)
        {
            var jsonDoc = JsonDocument.Parse(dataSchemaReplace);
            var dataSchemaReplaceDict = JsonSerializer.Deserialize<Dictionary<string, object>>(
                jsonDoc.RootElement.GetRawText()
            );
            Setup(dataSchemaReplaceDict);
        }

        /// <summary>
        /// Dictionary constructor.
        /// </summary>
        /// <param name="dataSchemaReplace"></param>
        public DataSchemaReplace(Dictionary<string, object> dataSchemaReplace)
        {
            Setup(dataSchemaReplace);
        }

        /// <summary>
        /// Converts the DataSchemaReplace to a JSON string preserving key order.
        /// </summary>
        /// <returns>JSON string representation of the DataSchemaReplace.</returns>
        public override string ToString()
        {
            using var stream = new System.IO.MemoryStream();
            using (var writer = new Utf8JsonWriter(stream,
                       new JsonWriterOptions
                       {
                           Indented = true,
                           Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                       }))
            {
                writer.WriteStartObject();
                writer.WritePropertyName("fields");
                writer.WriteStartArray();
                foreach (var field in Fields.Where(field => field.RawJson.HasValue))
                {
                    field.RawJson?.WriteTo(writer);
                }

                writer.WriteEndArray();
                writer.WriteEndObject();
            }

            return System.Text.Encoding.UTF8.GetString(stream.ToArray());
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
        /// Initializes a new instance of the <see cref="DataSchema"/> class from a dictionary.
        /// </summary>
        /// <param name="dataSchema">Dictionary containing the data schema configuration.</param>
        public DataSchema(Dictionary<string, object> dataSchema)
        {
            if (dataSchema.TryGetValue("replace", out var replaceValue))
            {
                Replace = replaceValue switch
                {
                    JsonElement element => element.ValueKind switch
                    {
                        JsonValueKind.Object => new DataSchemaReplace(
                            JsonSerializer.Deserialize<Dictionary<string, object>>(element.GetRawText())
                            ?? throw new MindeeInputException("Invalid replace object")
                        ),
                        JsonValueKind.String => new DataSchemaReplace(element.GetString()
                                                                      ?? throw new MindeeInputException(
                                                                          "Invalid replace string")),
                        _ => throw new MindeeInputException("Invalid replace format")
                    },
                    DataSchemaReplace dataSchemaReplace => dataSchemaReplace,
                    _ => throw new MindeeInputException("Invalid replace type")
                };
            }
            else
            {
                throw new MindeeInputException("Invalid Data Schema format.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSchema"/> class from a JSON string.
        /// </summary>
        /// <param name="jsonString">JSON string containing the data schema configuration.</param>
        public DataSchema(string jsonString)
        {
            var parsed = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonString);
            if (parsed == null || !parsed.TryGetValue("replace", out var replaceElement))
            {
                throw new MindeeInputException("Invalid Data Schema provided.");
            }

            var replaceDict = JsonSerializer.Deserialize<Dictionary<string, object>>(
                replaceElement.GetRawText()
            );
            if (replaceDict != null)
            {
                Replace = new DataSchemaReplace(replaceDict);
            }
        }

        /// <summary>
        /// Converts the DataSchema to a JSON string preserving key order.
        /// </summary>
        /// <returns>JSON string representation of the DataSchema.</returns>
        public override string ToString()
        {
            using var stream = new System.IO.MemoryStream();
            using (var writer = new Utf8JsonWriter(stream,
                       new JsonWriterOptions
                       {
                           Indented = true,
                           Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                       }))
            {
                writer.WriteStartObject();
                writer.WritePropertyName("replace");
                writer.WriteStartObject();
                writer.WritePropertyName("fields");
                writer.WriteStartArray();
                foreach (var field in Replace.Fields.Where(field => field.RawJson.HasValue))
                {
                    field.RawJson?.WriteTo(writer);
                }

                writer.WriteEndArray();
                writer.WriteEndObject();
                writer.WriteEndObject();
            }

            return System.Text.Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
