using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    ///
    /// </summary>
    public class DecimalJsonConverter : JsonConverter<decimal?>
    {
        /// <summary>
        ///
        /// </summary>
        public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string valueString = reader.GetString();
            if (valueString == "")
            {
                return null;
            }
            return decimal.Parse(valueString);
        }

        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, decimal?, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
