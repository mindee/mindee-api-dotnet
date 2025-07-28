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
        /// ResultOptions.
        /// </summary>
        [JsonPropertyName("options")]
        public InferenceResultOptions Options { get; set; }

        /// <summary>
        /// A prettier representation of the feature values.
        /// </summary>
        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine("Fields")
                .AppendLine("======")
                .AppendLine(Fields?.ToString() ?? string.Empty);

            if (Options != null)
            {
                strBuilder.AppendLine("Options")
                    .AppendLine("=======")
                    .AppendLine(Options.ToString());
            }
            string summary = strBuilder.ToString().TrimEnd('\n', '\r');
            return SummaryHelper.Clean(summary);
        }
    }
}
