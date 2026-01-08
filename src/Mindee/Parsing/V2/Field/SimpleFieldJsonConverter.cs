using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    ///     Custom deserializer for <see cref="DynamicField" />
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(SimpleFieldJsonConverter))]
    public class SimpleFieldJsonConverter : JsonConverter<SimpleField>
    {
        /// <summary>
        ///     <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)" />
        /// </summary>
        public override SimpleField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // read the response JSON into an object
            var jsonObject = (JsonObject)JsonSerializer.Deserialize(ref reader, typeof(JsonObject), options);

            FieldConfidence? confidence;
            if (jsonObject.TryGetPropertyValue("confidence", out var confidenceNode))
            {
                confidence = confidenceNode.Deserialize<FieldConfidence?>(options);
            }
            else
            {
                confidence = null;
            }

            List<FieldLocation> locations;
            if (jsonObject.TryGetPropertyValue("locations", out var locationsNode) &&
                locationsNode is JsonArray)
            {
                locations = locationsNode.Deserialize<List<FieldLocation>>(options);
            }
            else
            {
                locations = null;
            }

            Debug.Assert(jsonObject != null, nameof(jsonObject) + " != null");
            jsonObject.TryGetPropertyValue("value", out var fieldValue);
            if (fieldValue == null)
            {
                return new SimpleField(null, confidence, locations);
            }

            SimpleField field;

            switch (fieldValue.GetValueKind())
            {
                case JsonValueKind.String:
                    field = new SimpleField(
                        fieldValue.GetValue<string>(), confidence, locations);
                    break;
                case JsonValueKind.Number:
                    field = new SimpleField(
                        fieldValue.GetValue<double>(), confidence, locations);
                    break;

                case JsonValueKind.True:
                    field = new SimpleField(
                        true, confidence, locations);
                    break;
                case JsonValueKind.False:
                    field = new SimpleField(
                        false, confidence, locations);
                    break;
                default:
                    field = new SimpleField(
                        null, confidence, locations);
                    break;
            }

            return field;
        }

        /// <summary>
        ///     <see cref="Write(Utf8JsonWriter, SimpleField, JsonSerializerOptions)" />
        /// </summary>
        public override void Write(Utf8JsonWriter writer, SimpleField value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
