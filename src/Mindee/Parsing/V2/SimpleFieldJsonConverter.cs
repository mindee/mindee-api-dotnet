using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
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

            jsonObject.TryGetPropertyValue("value", out var fieldValue);
            if (fieldValue == null)
                return new SimpleField(value: null);

            SimpleField field;

            switch (fieldValue.GetValueKind())
            {
                case JsonValueKind.String:
                    field = new SimpleField(value: fieldValue.GetValue<string>());
                    break;
                case JsonValueKind.Number:
                    field = new SimpleField(value: fieldValue.GetValue<double>());
                    break;
                case JsonValueKind.True:
                    field = new SimpleField(value: true);
                    break;
                case JsonValueKind.False:
                    field = new SimpleField(value: false);
                    break;
                default:
                    field = new SimpleField(value: null);
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
