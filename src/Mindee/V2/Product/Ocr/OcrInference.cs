using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.V2.Parsing.Inference;

namespace Mindee.V2.Product.Ocr
{
    /// <summary>
    ///   Response for an OCR utility inference.
    /// </summary>
    public class OcrInference : BaseInference
    {
        /// <summary>
        ///     Result of the inference.
        /// </summary>
        [JsonPropertyName("result")]
        public OcrResult Result { get; set; }

        /// <summary>
        ///     A prettier representation.
        /// </summary>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder(base.ToString());
            stringBuilder.Append(Result);
            stringBuilder.Append('\n');

            return SummaryHelper.Clean(stringBuilder.ToString());
        }
    }
}
