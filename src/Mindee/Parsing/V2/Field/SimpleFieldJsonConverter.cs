using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    /// Custom deserializer for <see cref="DynamicField"/>
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(SimpleFieldJsonConverter))]
    public class SimpleFieldJsonConverter : JsonConverter<SimpleField>
    {
        /// <summary>
        /// <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        /// </summary>
        public override SimpleField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // read the response JSON into an object
            JsonObject jsonObject = (JsonObject)JsonSerializer.Deserialize(ref reader, typeof(JsonObject), options);

            FieldConfidence? confidence;
            if (jsonObject.TryGetPropertyValue("confidence", out JsonNode confidenceNode))
            {
                confidence = confidenceNode.Deserialize<FieldConfidence?>(options);
            }
            else
            {
                confidence = null;
            }

            List<FieldLocation> locations;
            if (jsonObject.TryGetPropertyValue("locations", out JsonNode locationsNode) &&
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
                return new SimpleField(value: null, confidence: confidence, locations: locations);

            SimpleField field;

            switch (fieldValue.GetValueKind())
            {
                case JsonValueKind.String:
                    field = new SimpleField(
                        value: fieldValue.GetValue<string>(), confidence: confidence, locations: locations);
                    break;
                case JsonValueKind.Number:
                    field = new SimpleField(
                        value: fieldValue.GetValue<double>(), confidence: confidence, locations: locations);
                    break;

                case JsonValueKind.True:
                    field = new SimpleField(
                        value: true, confidence: confidence, locations: locations);
                    break;
                case JsonValueKind.False:
                    field = new SimpleField(
                        value: false, confidence: confidence, locations: locations);
                    break;
                default:
                    field = new SimpleField(
                        value: null, confidence: confidence, locations: locations);
                    break;
            }

            return field;
        }

        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, SimpleField, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, SimpleField value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
