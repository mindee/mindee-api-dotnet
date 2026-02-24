using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Ocr
{
    /// <summary>
    /// Result of the OCR utility inference.
    /// </summary>
    public class OcrResult
    {
        /// <summary>
        /// List of OCR results for each page in the document.
        /// </summary>
        [JsonPropertyName("pages")]
        public List<OcrPage> Pages { get; set; }
    }
}
