using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Mindee.Parsing.V2.Field;

namespace Mindee.Parsing.V2
{
    /// <summary>
    ///     Custom deserializer for <see cref="InferenceFields" />
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(InferenceFieldsJsonConverter))]
    public class InferenceFieldsJsonConverter : JsonConverter<InferenceFields>
    {
        /// <summary>
        ///     <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)" />
        /// </summary>
        public override InferenceFields Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            var fields = new InferenceFields();

            // read the response JSON into an object
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            if (jsonObject == null)
            {
                return fields;
            }

            foreach (var jsonNode in jsonObject)
            {
                fields.Add(jsonNode.Key, jsonNode.Value.Deserialize<DynamicField>());
            }

            return fields;
        }


        /// <summary>
        ///     <see cref="Write(Utf8JsonWriter, InferenceFields, JsonSerializerOptions)" />
        /// </summary>
        public override void Write(Utf8JsonWriter writer, InferenceFields value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
