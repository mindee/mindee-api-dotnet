using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent an amount.
    /// </summary>
    public class Amount : BaseField
    {
        /// <summary>
        /// An amount value.
        /// </summary>
        /// <example>5.89</example>
        [JsonPropertyName("value")]
        public double? Value { get; set; }
    }
}
