using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
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
        /// Reads and converts the JSON to a DateTime.
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

            // If the string ends with "Z", replace it with "+00:00" to represent UTC.
            if (dateString.EndsWith("Z"))
            {
                dateString = dateString.Substring(0, dateString.Length - 1) + "+00:00";
            }
            else
            {
                if (dateString.Length < 6 ||
                    (dateString[dateString.Length - 6] != '+' && dateString[dateString.Length - 6] != '-'))
                {
                    dateString += "+00:00";
                }
            }
            return DateTime.Parse(dateString, null, DateTimeStyles.RoundtripKind).ToUniversalTime();
        }

        /// <summary>
        /// Writes the DateTime as a string, keeps the precision.
        /// <see cref="Write(Utf8JsonWriter, DateTime, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString("O"));
        }
    }
}
