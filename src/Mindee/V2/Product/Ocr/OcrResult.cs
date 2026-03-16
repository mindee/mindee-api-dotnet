using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// String representation of the OCR result.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = new System.Text.StringBuilder("OCR Result\n");
            stringBuilder.Append("##########\n");
            var i = 1;
            foreach (var page in Pages)
            {
                var pageNumberTitle = $"Page {i}";
                stringBuilder.Append($"{pageNumberTitle}");
                stringBuilder.Append('\n');
                stringBuilder.Append(string.Concat(Enumerable.Repeat("=", pageNumberTitle.Length)));
                stringBuilder.Append('\n');
                stringBuilder.Append('\n');
                stringBuilder.Append($"{page}\n");
                i++;
            }
            return stringBuilder.ToString();
        }
    }
}
