using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Classification
{
    /// <summary>
    ///   Classification inference for the V2 API.
    /// </summary>
    public sealed class ClassificationInference : BaseInference
    {
        /// <summary>
        ///     Result of the inference.
        /// </summary>
        [JsonPropertyName("result")]
        public ClassificationResult Result { get; set; }

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
