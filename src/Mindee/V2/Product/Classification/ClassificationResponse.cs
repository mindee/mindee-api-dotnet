using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Classification
{
    /// <summary>
    ///   Response for a classification utility inference.
    /// </summary>
    [EndpointSlug("classification")]
    public class ClassificationResponse : CommonInferenceResponse
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("inference")]
        public ClassificationInference Inference { get; set; }

    }
}
