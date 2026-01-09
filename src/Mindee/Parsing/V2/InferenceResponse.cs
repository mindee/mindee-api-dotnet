using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    ///     Represent a predict response from Mindee V2 API.
    /// </summary>
    public class InferenceResponse : CommonResponse
    {
        /// <summary>
        ///     Contents of the inference.
        /// </summary>
        [JsonPropertyName("inference")]
        public Inference Inference { get; set; }
    }
}
