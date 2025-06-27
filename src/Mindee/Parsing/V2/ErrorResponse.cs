using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Represent an error information from the API response.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Detail relevant to the error.
        /// </summary>
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        /// <summary>
        /// Http error code.
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// To make the error prettier to display.
        /// </summary>
        public override string ToString()
        {
            return "HTTP Status: " + Status + " - " + Detail;
        }
    }
}
