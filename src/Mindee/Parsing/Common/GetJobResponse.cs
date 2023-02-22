using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent information of a job from Mindee API.
    /// </summary>
    public class GetJobResponse<TModel> : PredictResponse<TModel>
        where TModel : class, new()
    {
        /// <summary>
        /// <see cref="Job"/>
        /// </summary>
        [JsonPropertyName("job")]
        public Job Job { get; set; }
    }
}
