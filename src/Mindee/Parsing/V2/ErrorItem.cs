using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Explicit details on a problem.
    /// </summary>
    public class ErrorItem
    {
        /// <summary>
        /// A JSON Pointer to the location of the body property.
        /// </summary>
        [JsonPropertyName("pointer")]
        public string Pointer { get; set; }

        /// <summary>
        /// Explicit information on the issue.
        /// </summary>
        [JsonPropertyName("detail")]
        public string Detail { get; set; }
    }
}
