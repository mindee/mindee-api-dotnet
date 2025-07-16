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
        /// ResultModel info.
        /// </summary>
        [JsonPropertyName("model")]
        public InferenceResultModel ResultModel { get; set; }

        /// <summary>
        /// ResultFile info.
        /// </summary>
        [JsonPropertyName("file")]
        public InferenceResultFile ResultFile { get; set; }

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
            result.Append($":ResultModel: {ResultModel.Id}\n");
            result.Append(":ResultFile:\n");
            result.Append($"  :Name: {ResultFile.Name}\n");
            result.Append($"  :Alias: {ResultFile.Alias}\n");
            result.Append("\nResult\n");
            result.Append("======\n");
            result.Append(Result);

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
