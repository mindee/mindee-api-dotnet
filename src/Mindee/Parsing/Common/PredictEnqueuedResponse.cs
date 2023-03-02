using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent an enqueued predict response from Mindee API.
    /// </summary>
    public class PredictEnqueuedResponse : Job
    {
        /// <summary>
        /// <see cref="Common.ApiRequest"/>
        /// </summary>
        [JsonPropertyName("api_request")]
        public ApiRequest ApiRequest { get; set; }
    }
}
