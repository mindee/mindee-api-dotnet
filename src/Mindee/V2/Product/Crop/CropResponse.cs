using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Crop
{
    /// <summary>
    ///     Represent a crop response from Mindee V2 API.
    /// </summary>
    [EndpointSlug("crop")]
    public class CropResponse : CommonInferenceResponse
    {
        /// <summary>
        ///     Contents of the inference.
        /// </summary>
        [JsonPropertyName("inference")]
        public CropInference Inference { get; set; }
    }
}
