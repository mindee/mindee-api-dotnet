using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent a predict response from Mindee V2 API.
    /// </summary>
    public class AsyncPredictResponseV2 : CommonResponseV2
    {
        /// <summary>
        /// Contents of the inference.
        /// </summary>
        [JsonPropertyName("inference")]
        public InferenceV2 Inference { get; set; }
    }
}
