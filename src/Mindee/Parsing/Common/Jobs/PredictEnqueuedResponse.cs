using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common.Jobs
{
    /// <summary>
    /// Represent an enqueued predict response from Mindee API.
    /// </summary>
    public class PredictEnqueuedResponse : CommonResponse
    {
        /// <summary>
        /// <see cref="Job"/>
        /// </summary>
        [JsonPropertyName("job")]
        public Job Job { get; set; }
    }
}
