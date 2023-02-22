using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Custom deserializer for <see cref="DateTime"/>
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        /// </summary>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, DateTime, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
