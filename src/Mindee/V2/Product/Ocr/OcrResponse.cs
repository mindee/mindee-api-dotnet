using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Ocr
{
    /// <summary>
    /// Response for an OCR utility inference.
    /// </summary>
    [ProductSlug("ocr")]
    public class OcrResponse : BaseResponse
    {
        /// <summary>
        /// Result of an OCR inference.
        /// </summary>
        [JsonPropertyName("inference")]
        public OcrInference Inference { get; set; }
    }
}
