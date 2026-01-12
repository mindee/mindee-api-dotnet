using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Mindee.Parsing
{
    /// <summary>
    ///     Custom de-serializer for custom lists of objects.
    /// </summary>
    public class ObjectListJsonConverter<TList, TItem> : JsonConverter<TList>
        where TList : IList<TItem>, new()
        where TItem : class, new()
    {
        /// <summary>
        ///     <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)" />
        /// </summary>
        public override TList Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var objectList = new TList();
            var jsonObject = (JsonArray)JsonSerializer.Deserialize(ref reader, typeof(JsonArray), options);

            if (jsonObject == null)
            {
                return objectList;
            }

            foreach (var jsonNode in jsonObject)
            {
                objectList.Add(jsonNode.Deserialize<TItem>());
            }

            return objectList;
        }

        /// <summary>
        ///     <see cref="Write(Utf8JsonWriter, TList, JsonSerializerOptions)" />
        /// </summary>
        public override void Write(Utf8JsonWriter writer, TList value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
