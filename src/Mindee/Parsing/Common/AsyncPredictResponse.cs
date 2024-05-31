using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent an enqueued predict response from Mindee API.
    /// </summary>
    public class AsyncPredictResponse<TModel> : PredictResponse<TModel>
        where TModel : class, new()
    {
        /// <summary>
        /// <see cref="Common.Job"/>
        /// </summary>
        [JsonPropertyName("job")]
        public Job Job { get; set; }
    }
}
