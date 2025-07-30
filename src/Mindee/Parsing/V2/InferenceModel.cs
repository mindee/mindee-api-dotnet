using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// ResultModel information for a V2 API inference.
    /// </summary>
    public class InferenceModel
    {
        /// <summary>
        /// The Mindee ID of the model.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Pretty display.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Model\n=====");
            stringBuilder.Append($"\n:ID: {Id}");
            return stringBuilder.ToString();
        }

    }
}
