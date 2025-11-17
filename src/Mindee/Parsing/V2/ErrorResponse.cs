using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Error response detailing a problem. The format adheres to RFC 9457.
    /// </summary>
    public class ErrorResponse : IErrorResponse
    {

        /// <inheritdoc/>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("errors")]
        public List<ErrorItem> Errors { get; set; }

        /// <summary>
        /// Constructor with all attributes.
        /// </summary>
        public ErrorResponse(int status, string title, string detail, string code, List<ErrorItem> errors)
        {
            Status = status;
            Title = title;
            Detail = detail;
            Code = code;
            Errors = errors;
        }

        /// <summary>
        /// To make the error prettier to display.
        /// </summary>
        public override string ToString()
        {
            return "HTTP Status: " + Status + " - " + Detail;
        }
    }
}
