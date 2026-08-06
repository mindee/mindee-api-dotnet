using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Mindee.V2.Parsing.Inference.Field
{
    /// <summary>
    ///     Custom deserializer for <see cref="DynamicField" />
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(DynamicFieldJsonConverter))]
    public class DynamicFieldJsonConverter : JsonConverter<DynamicField>
    {
        /// <summary>
        ///     <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)" />
        /// </summary>
        public override DynamicField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // read the response JSON into an object
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            // -------- LIST OF FIELDS --------
            if (jsonObject != null &&
                jsonObject.TryGetPropertyValue("items", out var itemsNode) &&
                itemsNode is JsonArray itemsArray)
            {
                FieldConfidence? confidence = null;
                if (jsonObject.TryGetPropertyValue("confidence", out var confidenceNode))
                {
                    confidence = confidenceNode.Deserialize<FieldConfidence?>(options);
                }

                var listField = new ListField(confidence);
                foreach (var item in itemsArray)
                {
                    listField.Items.Add(item.Deserialize<DynamicField>(options));
                }

                return new DynamicField(
                    FieldType.ListField, listField: listField);
            }

            // -------- OBJECT FIELD --------
            if (jsonObject != null &&
                     jsonObject.TryGetPropertyValue("fields", out var nestedFieldsNode) &&
                     nestedFieldsNode is JsonObject)
            {
                return new DynamicField(FieldType.ObjectField,
                    objectField: jsonObject.Deserialize<ObjectField>(options));
            }

            // -------- SIMPLE FIELD --------
            if (jsonObject != null && jsonObject.ContainsKey("value"))
            {
                return new DynamicField(
                    FieldType.SimpleField,
                    simpleField: jsonObject.Deserialize<SimpleField>(options));
            }

            return null;
        }

        /// <summary>
        ///     <see cref="Write(Utf8JsonWriter, DynamicField, JsonSerializerOptions)" />
        /// </summary>
        public override void Write(Utf8JsonWriter writer, DynamicField value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
