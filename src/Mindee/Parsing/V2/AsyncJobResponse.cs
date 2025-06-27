using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Represent a polling response from Mindee V2 API.
    /// </summary>
    public class AsyncJobResponse : CommonResponse
    {
        /// <summary>
        /// <see cref="Common.Job"/>
        /// </summary>
        [JsonPropertyName("job")]
        public Job Job { get; set; }
    }
}
