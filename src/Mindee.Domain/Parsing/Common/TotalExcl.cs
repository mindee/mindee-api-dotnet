using System.Text.Json.Serialization;

namespace Mindee.Domain.Parsing.Common
{
    public class TotalExcl : BaseField
    {
        /// <summary>
        /// The total without the tax amount.
        /// </summary>
        /// <example>5.01</example>
        [JsonPropertyName("value")]
        public double? Value { get; set; }
    }
}
