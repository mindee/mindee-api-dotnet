using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Custom de-serialize for <see cref="ErrorDetails"/>
    /// </summary>
    public class ErrorDetailsJsonConverter : JsonConverter<ErrorDetails>
    {
        /// <summary>
        /// <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        /// </summary>
        public override ErrorDetails Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string errorDetails;

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                JsonObject jsonObject = (JsonObject)JsonSerializer.Deserialize(
                    ref reader,
                    typeof(JsonObject),
                    options);

                errorDetails = jsonObject.ToJsonString();
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                errorDetails = reader.GetString();
            }
            else
            {
                throw new InvalidOperationException("The JSON type is not handled.");
            }

            return new ErrorDetails(errorDetails);
        }

        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, ErrorDetails, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, ErrorDetails value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
