using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Product.Generated;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Inference object for the V2 API.
    /// </summary>
    public class InferenceV2
    {
        /// <summary>
        /// Was a rotation applied to parse the document ?
        /// </summary>
        [JsonPropertyName("is_rotation_applied")]
        public bool? IsRotationApplied { get; set; }

        /// <summary>
        /// Type of product.
        /// </summary>
        [JsonPropertyName("product")]
        public Product Product { get; set; }

        /// <summary>
        /// The pages and the associated values which was detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        public PagesV2 Pages { get; set; }

        /// <summary>
        /// The model result values.
        /// </summary>
        [JsonPropertyName("result")]
        public ResultV2 Result { get; set; }

        /// <summary>
        /// A prettier representation.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder("########\n");
            result.Append("Inference\n");
            result.Append("########\n");
            result.Append("#########\n");
            result.Append($":Product: {Product.Name} v{Product.Version}\n");
            result.Append(
                $":Rotation applied: {(IsRotationApplied.HasValue && IsRotationApplied.Value ? "Yes" : "No")}\n");
            result.Append("\nResult\n");
            result.Append("==========\n");
            result.Append(Result.ToString());
            if (Pages.HasPredictions())
            {
                result.Append("\nPage Predictions\n");
                result.Append("================\n\n");
                result.Append(Pages);
            }

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
