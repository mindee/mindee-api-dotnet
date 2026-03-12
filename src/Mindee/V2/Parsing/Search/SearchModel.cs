using System.Text.Json.Serialization;

namespace Mindee.V2.Parsing.Search
{
    /// <summary>
    /// Individual model information.
    /// </summary>
    public class SearchModel
    {
        /// <summary>
        /// ID of the model.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the model.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Type of the model.
        /// </summary>
        [JsonPropertyName("model_type")]
        public string ModelType { get; set; }

        /// <summary>
        /// String representation of the model.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $":Name: {Name}\n:ID: {Id}\n:Model Type: {ModelType}";
        }

        /// <summary>
        /// String representation of the model.
        /// </summary>
        /// <returns></returns>
        public string[] ToListString()
        {
            return [$":Name: {Name}", $":ID: {Id}", $":Model Type: {ModelType}"];
        }
    }
}
