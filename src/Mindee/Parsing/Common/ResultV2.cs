using System.Text.Json.Serialization;
using Mindee.Product.Generated;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Model result values.
    /// </summary>
    public class ResultV2
    {
        /// <summary>
        /// The prediction model values.
        /// </summary>
        [JsonPropertyName("fields")]
        public GeneratedV2 Fields { get; set; }

        /// <summary>
        /// <see cref="Common.OptionsV2"/>
        /// </summary>
        [JsonPropertyName("options")]
        public OptionsV2 Options { get; set; }
    }
}
