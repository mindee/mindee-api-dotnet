using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Product.Generated;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Define the inference model of values.
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
        /// Potential extras present in the inference.
        /// </summary>
        private InferenceExtras _extras;

        /// <summary>
        /// Optional information.
        /// </summary>
        [JsonPropertyName("extras")]
        public InferenceExtras Extras
        {
            get
            {
                if (this.Pages.Count > 0 && (this._extras?.FullTextOcr == null))
                {
                    this._extras ??= new InferenceExtras();
                    if (this.Pages.First().Extras is { FullTextOcr: not null })
                    {
                        this._extras.FullTextOcr = string.Join("\n",
                            this.Pages.Select(page => page.Extras.FullTextOcr.Content));
                    }
                }

                return this._extras;
            }
            set => _extras = value;
        }


        /// <summary>
        /// The prediction model values.
        /// </summary>
        [JsonPropertyName("prediction")]
        public GeneratedV2 Prediction { get; set; }

        /// <summary>
        /// A prettier representation.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("\nInference\n");
            result.Append("#########\n");
            result.Append($":Product: {Product.Name} v{Product.Version}\n");
            result.Append(
                $":Rotation applied: {(IsRotationApplied.HasValue && IsRotationApplied.Value ? "Yes" : "No")}\n");
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
