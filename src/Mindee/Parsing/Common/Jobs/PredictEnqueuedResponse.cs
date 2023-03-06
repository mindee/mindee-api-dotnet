using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common.Jobs
{
    /// <summary>
    /// Represent an enqueued predict response from Mindee API.
    /// </summary>
    public class PredictEnqueuedResponse
    {
        /// <summary>
        /// <see cref="Common.ApiRequest"/>
        /// </summary>
        [JsonPropertyName("api_request")]
        public ApiRequest ApiRequest { get; set; }

        /// <summary>
        /// <see cref="Job"/>
        /// </summary>
        [JsonPropertyName("job")]
        public Job Job { get; set; }
    }
}