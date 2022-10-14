using System.Text.Json.Serialization;

namespace Mindee.Domain.Parsing.Common
{
    public class TotalIncl : BaseField
    {
        /// <summary>
        /// The total with the tax amount.
        /// </summary>
        /// <example>5.89</example>
        [JsonPropertyName("value")]
        public double? Value { get; set; }
    }
}
