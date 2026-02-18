using System.Text.Json.Serialization;
using Mindee.V1.Parsing.Common;

namespace Mindee.V2.Parsing
{
    /// <summary>
    ///     Represent a polling response from Mindee V2 API.
    /// </summary>
    public class JobResponse : CommonResponse
    {
        /// <summary>
        ///     <see cref="V1.Parsing.Common.Job" />
        /// </summary>
        [JsonPropertyName("job")]
        public Job Job { get; set; }
    }
}
