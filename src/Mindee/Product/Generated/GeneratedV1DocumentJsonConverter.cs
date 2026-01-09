using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Mindee.Parsing.Generated;

namespace Mindee.Product.Generated
{
    /// <summary>
    /// Custm deserializer for <see cref="GeneratedV1Document"/>
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(GeneratedV1DocumentJsonConverter))]
    public class GeneratedV1DocumentJsonConverter : JsonConverter<GeneratedV1Document>
    {
        /// <summary>
        /// <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        /// </summary>
        public override GeneratedV1Document Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

            return new GeneratedV1Document()
            {
                Fields = fields
            };
        }

        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, GeneratedV1Document, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, GeneratedV1Document value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
