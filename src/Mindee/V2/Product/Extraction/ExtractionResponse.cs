using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Extraction
{
    /// <summary>
    ///     Represent an extraction response from Mindee V2 API.
    /// </summary>
    [EndpointSlug("extraction")]
    public class ExtractionResponse : CommonInferenceResponse
    {
        /// <summary>
        ///     Contents of the inference.
        /// </summary>
        [JsonPropertyName("inference")]
        public ExtractionInference Inference { get; set; }
    }
}
