using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Define the inference model of values.
    /// </summary>
    /// <typeparam name="TPagePrediction">Page prediction (could be the same that TDocumentPrediction).</typeparam>
    /// <typeparam name="TDocumentPrediction">Document prediction (could be the same that TPagePrediction).</typeparam>
    public abstract class Inference<TPagePrediction, TDocumentPrediction>
        where TPagePrediction : IPrediction, new()
        where TDocumentPrediction : IPrediction, new()
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
        public virtual Pages<TPagePrediction> Pages { get; set; }

        /// <summary>
        /// The prediction model values.
        /// </summary>
        [JsonPropertyName("prediction")]
        public TDocumentPrediction Prediction { get; set; }

        /// <summary>
        /// A prettier representation.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("\nInference\n");
            result.Append("#########\n");
            result.Append($":Product: {Product.Name} v{Product.Version}\n");
            result.Append($":Rotation applied: {(IsRotationApplied.HasValue && IsRotationApplied.Value ? "Yes" : "No")}\n");
            result.Append("\nPrediction\n");
            result.Append("==========\n");
            result.Append(Prediction.ToString());
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
