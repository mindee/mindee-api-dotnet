using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Model information for a V2 API inference.
    /// </summary>
    public class InferenceModel
    {
        /// <summary>
        /// The Mindee ID of the model.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
