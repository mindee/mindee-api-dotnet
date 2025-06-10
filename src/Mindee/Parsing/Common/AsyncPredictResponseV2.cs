using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent a predict response from Mindee V2 API.
    /// </summary>
    public class AsyncPredictResponseV2 : CommonResponse
    {
        /// <summary>
        /// Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.
        /// </summary>
        [JsonPropertyName("document")]
        public DocumentV2 Document { get; set; }

        /// <summary>
        /// <see cref="Common.Job"/>
        /// </summary>
        [JsonPropertyName("job")]
        public JobV2 Job { get; set; }
    }
}
