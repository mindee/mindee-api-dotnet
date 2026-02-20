using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Extraction
{
    /// <summary>
    ///     Represent an extraction response from Mindee V2 API.
    /// </summary>
    public class ExtractionResponse : CommonResponse<Extraction>
    {
        /// <summary>
        ///     Contents of the inference.
        /// </summary>
        [JsonPropertyName("inference")]
        public ExtractionInference Inference { get; set; }
    }
}
