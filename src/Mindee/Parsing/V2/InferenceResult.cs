using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.V2.Field;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// A generic feature which can represent any OTS Mindee return prediction.
    /// </summary>
    public class InferenceResult
    {
        /// <summary>
        /// ResultModel fields..
        /// </summary>
        [JsonPropertyName("fields")]
        public InferenceFields Fields { get; set; }

        /// <summary>
        /// Full text extraction of the ocr result.
        /// </summary>
        [JsonPropertyName("raw_text")]
        public RawText RawText { get; set; }

        /// <summary>
        /// A prettier representation of the feature values.
        /// </summary>
        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine("Fields")
                .AppendLine("======")
                .AppendLine(Fields?.ToString() ?? string.Empty);

            string summary = strBuilder.ToString().TrimEnd('\n', '\r');
            return SummaryHelper.Clean(summary);
        }
    }
}
