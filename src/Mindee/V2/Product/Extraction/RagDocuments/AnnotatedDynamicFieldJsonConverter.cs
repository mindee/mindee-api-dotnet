using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Mindee.V2.Parsing.Inference.Field;

namespace Mindee.V2.Product.Extraction.RagDocuments
{
    /// <summary>
    /// Custom deserializer for <see cref="AnnotatedDynamicField" />
    /// </summary>
    public class DynamicAnnotationFieldJsonConverter : JsonConverter<AnnotatedDynamicField>
    {
        /// <summary>
        ///     <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)" />
        /// </summary>
        public override AnnotatedDynamicField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // read the response JSON into an object
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            if (jsonObject == null)
                return null;

            // -------- LIST OF FIELDS --------
            if (jsonObject.TryGetPropertyValue("items", out var itemsNode) &&
                itemsNode is JsonArray itemsArray)
            {
                string guidelines = jsonObject["guidelines"]?.Deserialize<string>(options);
                bool selected = jsonObject["selected"]?.Deserialize<bool>(options) ?? false;

                var listField = new AnnotatedListField(selected, guidelines);
                foreach (var item in itemsArray)
                {
                    listField.Items.Add(item.Deserialize<AnnotatedDynamicField>(options));
                }

                return new AnnotatedDynamicField(
                    FieldType.ListField, listField: listField);
            }
            if (jsonObject.TryGetPropertyValue("fields", out var nestedFieldsNode) &&
                nestedFieldsNode is JsonObject)
            {
                return new AnnotatedDynamicField(
                    FieldType.ObjectField,
                    objectField: jsonObject.Deserialize<AnnotatedObjectField>(options));
            }
            // -------- SIMPLE FIELD --------
            if (jsonObject.ContainsKey("value"))
            {
                return new AnnotatedDynamicField(
                    FieldType.SimpleField,
                    simpleField: jsonObject.Deserialize<AnnotatedSimpleField>(options));
            }

            throw new JsonException($"Unknown field: {jsonObject.GetPath()}");
        }

        /// <summary>
        ///     <see cref="Write(Utf8JsonWriter, AnnotatedDynamicField, JsonSerializerOptions)" />
        /// </summary>
        public override void Write(Utf8JsonWriter writer, AnnotatedDynamicField value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }
            switch (value.Type)
            {
                case FieldType.SimpleField:
                    JsonSerializer.Serialize(writer, value.SimpleField, options);
                    break;
                case FieldType.ObjectField:
                    JsonSerializer.Serialize(writer, value.ObjectField, options);
                    break;
                case FieldType.ListField:
                    JsonSerializer.Serialize(writer, value.ListField, options);
                    break;
                default:
                    throw new JsonException($"Unknown field type: {value.Type}");
            }
        }
    }
}
