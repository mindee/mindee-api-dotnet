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
            // «result» object of the response
            JsonObject jsonObject = (JsonObject)JsonSerializer.Deserialize(ref reader, typeof(JsonObject), options);

            // Safety net
            if (jsonObject is null)
            {
                return new GeneratedV2 { Fields = new Dictionary<string, GeneratedFeatureV2>() };
            }

            /* -----------------------------------------------------------------
             * V2 responses wrap the real prediction fields inside the property
             * named "fields" and may also contain an "options" block.
             * ----------------------------------------------------------------- */
            if (jsonObject.TryGetPropertyValue("fields", out var fldNode) && fldNode is JsonObject fldObj)
            {
                jsonObject = fldObj;          // work on the inner object that really holds the 21 fields
            }

            var fields = new Dictionary<string, GeneratedFeatureV2>();

            foreach (var jsonNode in jsonObject)
            {
                GeneratedFeatureV2 feature;

                // ───────────── LIST FEATURE ─────────────
                if (jsonNode.Value is JsonObject obj &&
                    obj.TryGetPropertyValue("items", out var itemsNode) &&
                    itemsNode is JsonArray itemsArray)
                {
                    feature = new GeneratedFeatureV2(true);

                    foreach (var item in itemsArray)
                    {
                        feature.Add(item.Deserialize<GeneratedObjectV2>());
                    }

                    fields.Add(jsonNode.Key, feature);
                }
                // ───────────── OBJECT WITH NESTED FIELDS ─────────────
                else if (jsonNode.Value is JsonObject itemsObj &&
                         itemsObj.TryGetPropertyValue("fields", out var nestedFieldsNode) &&
                         nestedFieldsNode is JsonObject nestedFieldsObj)
                {
                    fields.Add(jsonNode.Key, new GeneratedFeatureV2(false, nestedFieldsObj));
                }
                // ───────────── SIMPLE OBJECT ─────────────
                else
                {
                    fields.Add(
                        jsonNode.Key,
                        new GeneratedFeatureV2(false) { jsonNode.Value.Deserialize<GeneratedObjectV2>() }
                    );
                }
            }

            return new GeneratedV2
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
