using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    /// Custom de-serialize for <see cref="Taxes"/>
    /// </summary>
    public class ObjectListJsonConverter<TList, TItem> : JsonConverter<TList>
        where TList : List<TItem>, new()
        where TItem : class, new()
    {
        /// <summary>
        /// <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        /// </summary>
        public override TList Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            TList theList = new TList();
            JsonArray jsonObject = (JsonArray)JsonSerializer.Deserialize(ref reader, typeof(JsonArray), options);

            if (jsonObject == null)
                return theList;

            foreach (var jsonNode in jsonObject)
            {
                theList.Add(jsonNode.Deserialize<TItem>());
            }
            return theList;
        }

        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, TList, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, TList value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
