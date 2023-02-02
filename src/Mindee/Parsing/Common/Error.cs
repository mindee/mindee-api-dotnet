using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent an error information from the API response.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Details about it.
        /// </summary>
        [JsonPropertyName("details")]
        public ErrorDetails Details { get; set; }

        /// <summary>
        /// More precise information about the current error.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// A code to identify it.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// To make the error prettier to display.
        /// </summary>
        public override string ToString()
        {
            return $"{Code} : {Message} - {Details}";
        }
    }
}
