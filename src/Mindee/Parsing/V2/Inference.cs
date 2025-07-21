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
            var result = new StringBuilder("Inference\n#########");
            result.Append("\nModel\n=====");
            result.Append($"\n:ID: {Model.Id}");
            result.Append("\n\n:File:\n====");
            result.Append($"\n:Name: {File.Name}");
            result.Append($"\n:Alias: {File.Alias}");
            result.Append("\nResult\n======");
            result.Append("\n");
            result.Append(Result);

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
