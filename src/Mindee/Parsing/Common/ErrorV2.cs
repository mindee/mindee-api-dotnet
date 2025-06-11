using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Exceptions;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent an error information from the API response.
    /// </summary>
    public class ErrorV2
    {
        /// <summary>
        /// Detail relevant to the error.
        /// </summary>
        [JsonPropertyName("details")]
        public string Detail { get; set; }

        /// <summary>
        /// Http error code.
        /// </summary>
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        /// <summary>
        /// Error title.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Specific error code.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// List of error sub-objects for more details.
        /// </summary>
        [JsonPropertyName("errors")]
        public List<MindeeHttpExceptionError> Errors { get; set; }

        /// <summary>
        /// To make the error prettier to display.
        /// </summary>
        public override string ToString()
        {
            var builder = new StringBuilder();
            if (Code != null)
            {
                builder.Append($"{StatusCode}: ");
            }
            else
            {
                builder.Append($"{Code}: ");
            }

            if (!string.IsNullOrEmpty(Title))
            {
                builder.Append(Title);
                if (!string.IsNullOrEmpty(Detail))
                    builder.Append($" - {Detail}");
            }
            else if (!string.IsNullOrEmpty(Detail))
            {
                builder.Append($"{Detail}");
            }

            return builder.ToString();
        }
    }
}
