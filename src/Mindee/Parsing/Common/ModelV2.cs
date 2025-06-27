using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Model information for a V2 API inference.
    /// </summary>
    public class ModelV2
    {
        /// <summary>
        /// The Mindee ID of the model.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
