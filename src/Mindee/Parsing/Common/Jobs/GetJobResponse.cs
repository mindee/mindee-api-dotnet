using System.Text.Json.Serialization;
using Mindee.Parsing.Common.Jobs;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent information of a job from Mindee API.
    /// </summary>
    public class GetJobResponse<TModel> : PredictResponse<TModel>
        where TModel : class, new()
    {
        /// <summary>
        /// <see cref="Jobs.Job"/>
        /// </summary>
        [JsonPropertyName("job")]
        public Job Job { get; set; }
    }
}
