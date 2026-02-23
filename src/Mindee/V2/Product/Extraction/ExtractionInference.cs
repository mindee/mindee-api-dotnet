using System;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.V2.Parsing;
using Mindee.V2.Parsing.Inference;

namespace Mindee.V2.Product.Extraction
{
    /// <summary>
    ///     ExtractionInference object for the V2 API.
    /// </summary>
    public sealed class ExtractionInference : BaseInference
    {
        /// <summary>
        ///     Options which were activated during the inference.
        /// </summary>
        [JsonPropertyName("active_options")]
        public InferenceActiveOptions ActiveOptions { get; set; }

        /// <summary>
        ///     Result of the inference.
        /// </summary>
        [JsonPropertyName("result")]
        public ExtractionResult Result { get; set; }

        /// <summary>
        /// Type of the product's response.
        /// </summary>
        public static new Type ResponseType => typeof(ExtractionResponse);

        /// <summary>
        ///     A prettier representation.
        /// </summary>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder(base.ToString());
            stringBuilder.Append(ActiveOptions);
            stringBuilder.Append("\n\n");
            stringBuilder.Append(Result);
            stringBuilder.Append("\n");

            return SummaryHelper.Clean(stringBuilder.ToString());
        }
    }
}
