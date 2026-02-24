using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.V2.Parsing;
using Mindee.V2.Parsing.Field;

namespace Mindee.V2.Product.Extraction
{
    /// <summary>
    ///     A generic feature which can represent any OTS Mindee return prediction.
    /// </summary>
    public class ExtractionResult
    {
        /// <summary>
        ///     ResultModel fields..
        /// </summary>
        [JsonPropertyName("fields")]
        public InferenceFields Fields { get; set; }

        /// <summary>
        ///     Full text extraction of the ocr result.
        /// </summary>
        [JsonPropertyName("raw_text")]
        public RawText RawText { get; set; }

        /// <summary>
        ///     RAG metadata.
        /// </summary>
        [JsonPropertyName("rag")]
        public RagMetadata Rag { get; set; }

        /// <summary>
        ///     A prettier representation of the feature values.
        /// </summary>
        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine("Fields")
                .AppendLine("======")
                .AppendLine(Fields?.ToString() ?? string.Empty);

            var summary = strBuilder.ToString().TrimEnd('\n', '\r');
            return SummaryHelper.Clean(summary);
        }
    }
}
