using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Extraction
{
    /// <summary>
    ///     Response for an extraction inference.
    /// </summary>
    [ProductAttributes("extraction")]
    public class ExtractionResponse : BaseResponse
    {
        /// <summary>
        ///     Contents of the inference.
        /// </summary>
        [JsonPropertyName("inference")]
        public ExtractionInference Inference { get; set; }
    }
}
