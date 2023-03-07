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
        public ApiRequest ApiRequest { get; set; }
    }
}
