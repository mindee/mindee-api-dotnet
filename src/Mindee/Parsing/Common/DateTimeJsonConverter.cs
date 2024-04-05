using System;
using System.Data;
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
            string dateString = reader.GetString();

            // null values should never get passed to this method
            if (dateString == null)
            {
                throw new NoNullAllowedException();
            }
            // default to UTC if the time zone is not specified
            if (!dateString.Contains("+"))
            {
                dateString += "+00:00";
            }
            return DateTime.Parse(dateString).ToUniversalTime();
        }

        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, DateTime, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("s"));
        }
    }
}
