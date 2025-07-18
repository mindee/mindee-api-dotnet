using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    /// Custom deserializer for <see cref="DynamicField"/>
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(DynamicFieldJsonConverter))]
    public class DynamicFieldJsonConverter : JsonConverter<DynamicField>
    {
        /// <summary>
        /// <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        /// </summary>
        public override DynamicField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // read the response JSON into an object
            JsonObject jsonObject = (JsonObject)JsonSerializer.Deserialize(ref reader, typeof(JsonObject), options);

            DynamicField field;

            // -------- LIST FEATURE --------
            if (jsonObject.TryGetPropertyValue("items", out var itemsNode) &&
                itemsNode is JsonArray itemsArray)
            {
                var listField = new ListField();
                foreach (var item in itemsArray)
                {
                    listField.Items.Add(item.Deserialize<DynamicField>());
                }
                field = new DynamicField(
                    FieldType.ListField, listField: listField);
            }

            // -------- OBJECT WITH NESTED FIELDS --------
            else if (jsonObject.TryGetPropertyValue("fields", out var nestedFieldsNode) &&
                     nestedFieldsNode is JsonObject)
            {
                field = new DynamicField(FieldType.ObjectField,
                    objectField: jsonObject.Deserialize<ObjectField>());
            }
            // -------- SIMPLE OBJECT --------
            else if (jsonObject.ContainsKey("value"))
            {
                field = new DynamicField(
                    FieldType.SimpleField,
                    simpleField: jsonObject.Deserialize<SimpleField>());
            }
            else
            {
                return null;
            }

            return field;
        }

        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, DynamicField, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, DynamicField value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
