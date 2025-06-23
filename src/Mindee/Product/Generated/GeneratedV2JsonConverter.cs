using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Mindee.Parsing.Generated;

namespace Mindee.Product.Generated
{
    /// <summary>
    /// Custm deserializer for <see cref="GeneratedV2"/>
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(GeneratedV2JsonConverter))]
    public class GeneratedV2JsonConverter : JsonConverter<GeneratedV2>
    {
        /// <summary>
        /// <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        /// </summary>
        public override GeneratedV2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var fields = new Dictionary<string, GeneratedFeature>();

            JsonObject jsonObject = (JsonObject)JsonSerializer.Deserialize(ref reader, typeof(JsonObject), options);

            foreach (var jsonNode in jsonObject)
            {
                GeneratedFeature feature;

                if (jsonNode.Value is JsonArray)
                {
                    feature = new GeneratedFeature(true);
                    foreach (var featureValue in (JsonArray)jsonNode.Value)
                    {
                        feature.Add(featureValue.Deserialize<GeneratedObject>());
                    }
                    fields.Add(jsonNode.Key, feature);
                }
                else
                {
                    fields.Add(
                        jsonNode.Key,
                        new GeneratedFeature(false)
                        {
                            jsonNode.Value.Deserialize<GeneratedObject>()
                        }
                    );
                }
            }

            return new GeneratedV2()
            {
                Fields = fields
            };
        }

        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, GeneratedV2, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, GeneratedV2 value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
