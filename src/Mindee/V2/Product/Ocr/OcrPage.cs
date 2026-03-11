using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Ocr
{
    /// <summary>
    /// OCR result for a single page.
    /// </summary>
    public class OcrPage
    {
        /// <summary>
        /// OCR result for a single page.
        /// </summary>
        [JsonPropertyName("words")]
        public List<OcrWord> Words { get; set; }

        /// <summary>
        /// Full text content extracted from the document page.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        ///     A prettier representation.
        /// </summary>
        public override string ToString()
        {
            string ocrWords = "\n";

            if (Words is { Count: > 0 })
            {
                ocrWords += string.Join("\n\n", Words);
            }

            return $"OCR Words\n---------{ocrWords}\n\n:Content: {Content}";
        }
    }
}
