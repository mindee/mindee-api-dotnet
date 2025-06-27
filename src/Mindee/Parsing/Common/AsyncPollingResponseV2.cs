using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent a polling response from Mindee V2 API.
    /// </summary>
    public class AsyncPollingResponseV2 : CommonResponseV2
    {
        /// <summary>
        /// <see cref="Common.Job"/>
        /// </summary>
        [JsonPropertyName("job")]
        public JobV2 Job { get; set; }
    }
}
