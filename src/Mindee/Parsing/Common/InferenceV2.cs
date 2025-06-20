using System.Text;
using System.Text.Json.Serialization;

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
        /// Model info.
        /// </summary>
        [JsonPropertyName("model")]
        public ModelV2 Model { get; set; }

        /// <summary>
        /// File info.
        /// </summary>
        [JsonPropertyName("file")]
        public FileV2 File { get; set; }

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
            result.Append($":Model: {Model.Id}\n");
            result.Append(":File:\n");
            result.Append($"  :Name:{File.Name}\n");
            if (File.Alias != null)
            {
                result.Append($"  :Alias:{File.Alias}\n");
            }
            result.Append(
                $":Rotation applied: {(IsRotationApplied.HasValue && IsRotationApplied.Value ? "Yes" : "No")}\n");
            result.Append("\nResult\n");
            result.Append("==========\n");
            result.Append(Result);
            if (Pages != null && Pages.HasPredictions())
            {
                result.Append("\nPage Predictions\n");
                result.Append("================\n\n");
                result.Append(Pages);
            }

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
