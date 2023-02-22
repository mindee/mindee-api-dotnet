using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent an enqueued predict response from Mindee API.
    /// </summary>
    public class AsyncPredictResponse<TModel> : CommonResponse
        where TModel : class, new()
    {
        /// <summary>
        /// <see cref="Common.Job"/>
        /// </summary>
        [JsonPropertyName("job")]
        public Job Job { get; set; }

        /// <summary>
        /// Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.
        /// </summary>
        [JsonPropertyName("document")]
        public Document<TModel> Document { get; set; }
    }
}
