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
            var fields = new Dictionary<string, GeneratedFeatureV2>();

            JsonObject jsonObject = (JsonObject)JsonSerializer.Deserialize(ref reader, typeof(JsonObject), options);

            if (jsonObject == null)
            {
                return new GeneratedV2 { Fields = fields };
            }

            foreach (var jsonNode in jsonObject)
            {
                GeneratedFeatureV2 feature;

                if (jsonNode.Value is JsonObject obj &&
                    obj.TryGetPropertyValue("items", out var itemsNode) &&
                    itemsNode is JsonArray itemsArray
                   )
                {
                    feature = new GeneratedFeatureV2(true);
                    foreach (var item in itemsArray)
                    {
                        feature.Add(item.Deserialize<GeneratedObjectV2>());
                    }

                    fields.Add(jsonNode.Key, feature);
                }
                else if (jsonNode.Value is JsonObject itemsObj &&
                         itemsObj.TryGetPropertyValue("fields", out var nestedFieldsNode) &&
                         nestedFieldsNode is JsonObject nestedFieldsObj)
                {
                    fields.Add(jsonNode.Key, new GeneratedFeatureV2(false, nestedFieldsObj));
                }
                else
                {
                    fields.Add(
                        jsonNode.Key,
                        new GeneratedFeatureV2(false) { jsonNode.Value.Deserialize<GeneratedObjectV2>() }
                    );
                }
            }

            return new GeneratedV2 { Fields = fields };
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
