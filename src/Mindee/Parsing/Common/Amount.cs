using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    public class Amount : BaseField
    {
        /// <summary>
        /// The amount value.
        /// </summary>
        /// <example>5.89</example>
        [JsonPropertyName("value")]
        public double? Value { get; set; }
    }
}
