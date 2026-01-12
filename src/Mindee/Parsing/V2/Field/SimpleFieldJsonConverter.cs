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
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            FieldConfidence? confidence;
            if (jsonObject != null && jsonObject.TryGetPropertyValue("confidence", out var confidenceNode))
            {
                confidence = confidenceNode.Deserialize<FieldConfidence?>(options);
            }
            else
            {
                confidence = null;
            }

            List<FieldLocation> locations;
            if (jsonObject != null &&
                jsonObject.TryGetPropertyValue("locations", out var locationsNode) &&
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

            SimpleField field = fieldValue.GetValueKind() switch
            {
                JsonValueKind.String => new SimpleField(fieldValue.GetValue<string>(), confidence, locations),
                JsonValueKind.Number => new SimpleField(fieldValue.GetValue<double>(), confidence, locations),
                JsonValueKind.True => new SimpleField(true, confidence, locations),
                JsonValueKind.False => new SimpleField(false, confidence, locations),
                _ => new SimpleField(null, confidence, locations)
            };

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
