using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Extraction.RagDocuments
{
    /// <summary>
    ///  Custom deserializer for <see cref="AnnotatedSimpleField" />
    /// </summary>
    [Serializable]
    public class AnnotatedSimpleFieldJsonConverter : JsonConverter<AnnotatedSimpleField>
    {
        /// <summary>
        ///     <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)" />
        /// </summary>
        public override AnnotatedSimpleField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // read the response JSON into an object
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            string guidelines;
            if (jsonObject != null && jsonObject.TryGetPropertyValue("guidelines", out var confidenceNode))
                guidelines = confidenceNode.Deserialize<string>(options);
            else
                guidelines = null;

            bool selected;
            if (jsonObject != null && jsonObject.TryGetPropertyValue("selected", out var selectedNode))
                selected = selectedNode.Deserialize<bool>(options);
            else
                selected = false;

            Debug.Assert(jsonObject != null, nameof(jsonObject) + " != null");
            jsonObject.TryGetPropertyValue("value", out var fieldValue);
            if (fieldValue == null)
            {
                return new AnnotatedSimpleField(null, selected, guidelines);
            }

            AnnotatedSimpleField field = fieldValue.GetValueKind() switch
            {
                JsonValueKind.String => new AnnotatedSimpleField(fieldValue.GetValue<string>(), selected, guidelines),
                JsonValueKind.Number => new AnnotatedSimpleField(fieldValue.GetValue<double>(), selected, guidelines),
                JsonValueKind.True => new AnnotatedSimpleField(true, selected, guidelines),
                JsonValueKind.False => new AnnotatedSimpleField(false, selected, guidelines),
                _ => new AnnotatedSimpleField(null, selected, guidelines)
            };

            return field;
        }

        /// <summary>
        ///     <see cref="Write(Utf8JsonWriter, AnnotatedSimpleField, JsonSerializerOptions)" />
        /// </summary>
        public override void Write(Utf8JsonWriter writer, AnnotatedSimpleField value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();

            writer.WriteBoolean("selected", value.Selected);

            if (value.Guidelines == null)
                writer.WriteNull("guidelines");
            else
                writer.WriteString("guidelines", value.Guidelines);

            if (value.Value == null)
                writer.WriteNull("value");
            else
            {
                writer.WritePropertyName("value");
                JsonSerializer.Serialize(writer, value.Value, options);
            }

            writer.WriteEndObject();
        }
    }
}
