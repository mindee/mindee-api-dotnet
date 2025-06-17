using System.Text.Json.Serialization;
using Mindee.Product.Generated;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Model result values.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// The prediction model values.
        /// </summary>
        [JsonPropertyName("fields")]
        public GeneratedV2 Fields { get; set; }
    }
}
