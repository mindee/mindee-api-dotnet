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
        public InferenceResultModel Model { get; set; }

        /// <summary>
        /// ResultFile info.
        /// </summary>
        [JsonPropertyName("file")]
        public InferenceResultFile File { get; set; }

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
            var stringBuilder = new StringBuilder("Inference\n#########\n");
            stringBuilder.Append(Model);
            stringBuilder.Append("\n\n");
            stringBuilder.Append(File);
            stringBuilder.Append("\n\n");
            stringBuilder.Append(Result);
            stringBuilder.Append("\n");

            return SummaryHelper.Clean(stringBuilder.ToString());
        }
    }
}
