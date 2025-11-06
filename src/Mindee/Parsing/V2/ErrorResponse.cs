using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Error response detailing a problem. The format adheres to RFC 9457.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// The HTTP status code returned by the server.
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// A human-readable explanation specific to the occurrence of the problem.
        /// </summary>
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        /// <summary>
        /// A short, human-readable summary of the problem.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// A machine-readable code specific to the occurrence of the problem.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// A list of explicit details on the problem.
        /// </summary>
        [JsonPropertyName("errors")]
        public List<ErrorItem> Errors { get; set; }

        /// <summary>
        /// To make the error prettier to display.
        /// </summary>
        public override string ToString()
        {
            return "HTTP Status: " + Status + " - " + Detail;
        }
    }
}
