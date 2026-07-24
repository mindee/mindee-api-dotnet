using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Mindee.V2.Parsing.Inference.Field;

namespace Mindee.V2.Product.Extraction.RagDocuments
{
    /// <summary>
    /// ustom deserializer for <see cref="AnnotatedDynamicField" />
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(DynamicAnnotationFieldJsonConverter))]
    public class DynamicAnnotationFieldJsonConverter : JsonConverter<AnnotatedDynamicField>
    {
        /// <summary>
        ///     <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)" />
        /// </summary>
        public override AnnotatedDynamicField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // read the response JSON into an object
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            // -------- LIST OF FIELDS --------
            if (jsonObject != null &&
                jsonObject.TryGetPropertyValue("items", out var itemsNode) &&
                itemsNode is JsonArray itemsArray)
            {
                string guidelines;
                if (jsonObject.TryGetPropertyValue("guidelines", out var confidenceNode))
                    guidelines = confidenceNode.Deserialize<string>(options);
                else
                    guidelines = null;

                bool selected;
                if (jsonObject.TryGetPropertyValue("selected", out var selectedNode))
                    selected = selectedNode.Deserialize<bool>(options);
                else
                    selected = false;

                var listField = new AnnotatedListField(selected, guidelines);
                foreach (var item in itemsArray)
                {
                    listField.Items.Add(item.Deserialize<AnnotatedDynamicField>(options));
                }

                return new AnnotatedDynamicField(
                    FieldType.ListField, listField: listField);
            }
            if (jsonObject != null &&
                     jsonObject.TryGetPropertyValue("fields", out var nestedFieldsNode) &&
                     nestedFieldsNode is JsonObject)
            {
                return new AnnotatedDynamicField(
                    FieldType.ObjectField,
                    objectField: jsonObject.Deserialize<AnnotatedObjectField>(options));
            }
            // -------- SIMPLE FIELD --------
            if (jsonObject != null && jsonObject.ContainsKey("value"))
            {
                return new AnnotatedDynamicField(
                    FieldType.SimpleField,
                    simpleField: jsonObject.Deserialize<AnnotatedSimpleField>(options));
            }
            return null;
        }

        /// <summary>
        ///     <see cref="Write(Utf8JsonWriter, AnnotatedDynamicField, JsonSerializerOptions)" />
        /// </summary>
        public override void Write(Utf8JsonWriter writer, AnnotatedDynamicField value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
