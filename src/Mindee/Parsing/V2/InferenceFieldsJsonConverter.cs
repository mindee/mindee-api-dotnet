using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Mindee.Parsing.V2;

namespace Mindee.Product.V2
{
    /// <summary>
    /// Custom deserializer for <see cref="InferenceFields"/>
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(InferenceFieldsJsonConverter))]
    public class InferenceFieldsJsonConverter : JsonConverter<InferenceFields>
    {
        /// <summary>
        /// <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        /// </summary>
        public override InferenceFields Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            InferenceFields fields = new InferenceFields { };

            // read the response JSON into an object
            JsonObject jsonObject = (JsonObject)JsonSerializer.Deserialize(ref reader, typeof(JsonObject), options);

            foreach (var jsonNode in jsonObject)
            {
                DynamicField field;

                // -------- LIST FEATURE --------
                if (jsonNode.Value is JsonObject obj &&
                    obj.TryGetPropertyValue("items", out var itemsNode) &&
                    itemsNode is JsonArray itemsArray)
                {
                    field = new DynamicField(
                        FieldType.ListField,
                        listField: jsonNode.Value.Deserialize<ListField>());
                }
                // -------- OBJECT WITH NESTED FIELDS --------
                else if (jsonNode.Value is JsonObject itemsObj &&
                         itemsObj.TryGetPropertyValue("fields", out var nestedFieldsNode) &&
                         nestedFieldsNode is JsonObject nestedFieldsObj)
                {
                    field = new DynamicField(FieldType.ObjectField,
                        objectField: jsonNode.Value.Deserialize<ObjectField>());
                }
                // -------- SIMPLE OBJECT --------
                else
                {
                    field = new DynamicField(
                        FieldType.SimpleField,
                        simpleField: jsonNode.Value.Deserialize<SimpleField>());
                }
                fields.Add(jsonNode.Key, field);
            }
            return fields;
        }


        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, InferenceFields, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, InferenceFields value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
