using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    ///     Custom de-serializer for custom lists of objects.
    /// </summary>
    public class PagesJsonConverter<TPage> : JsonConverter<Pages<TPage>>
        where TPage : IPrediction, new()
    {
        /// <summary>
        ///     <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)" />
        /// </summary>
        public override Pages<TPage> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var pages = new Pages<TPage>();
            var jsonObject = (JsonArray)JsonSerializer.Deserialize(ref reader, typeof(JsonArray), options);

            if (jsonObject == null)
            {
                return pages;
            }

            foreach (var jsonNode in jsonObject)
            {
                var pageString = jsonNode.ToString()
                        .Replace("\"prediction\": {}", "\"prediction\": null")
                        .Replace("\"extras\": {}", "\"extras\": null")
                    ;
                var page = (Page<TPage>)JsonSerializer.Deserialize(pageString, typeof(Page<TPage>), options);
                pages.Add(page);
            }

            return pages;
        }

        /// <summary>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, Pages<TPage> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
