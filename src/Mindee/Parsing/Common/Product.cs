using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// The product on which the document was parsed.
    /// </summary>
    public sealed class Product
    {
        /// <summary>
        /// Name of the product used.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Version of the product used.
        /// </summary>

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
