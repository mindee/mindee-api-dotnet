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
            result.Append("\nResult\n");
            result.Append("==========\n");
            result.Append(Result);

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
