using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Inference object for the V2 API.
    /// </summary>
    public class Inference
    {
        /// <summary>
        /// Model info.
        /// </summary>
        [JsonPropertyName("model")]
        public InferenceModel Model { get; set; }

        /// <summary>
        /// File info.
        /// </summary>
        [JsonPropertyName("file")]
        public InferenceFile File { get; set; }

        /// <summary>
        /// The model result values.
        /// </summary>
        [JsonPropertyName("result")]
        public InferenceResult Result { get; set; }

        /// <summary>
        /// A prettier representation.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder("#########\n");
            result.Append("Inference\n");
            result.Append("#########\n");
            result.Append($":Model: {Model.Id}\n");
            result.Append(":File:\n");
            result.Append($"  :Name: {File.Name}\n");
            result.Append($"  :Alias: {File.Alias}\n");
            result.Append("\nResult\n");
            result.Append("======\n");
            result.Append(Result);

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
