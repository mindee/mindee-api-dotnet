using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent common response information from Mindee API.
    /// </summary>
    public abstract class CommonResponse
    {
        /// <summary>
        /// <see cref="Common.ApiRequest"/>
        /// </summary>
        [JsonPropertyName("api_request")]
        public virtual ApiRequest ApiRequest { get; set; }

        /// <summary>
        /// The raw server response.
        /// This is not formatted in any way by the library and may contain newline and tab characters.
        /// </summary>
        public string RawResponse { get; set; }
    }
}
