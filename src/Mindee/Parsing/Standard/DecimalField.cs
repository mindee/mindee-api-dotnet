using System.Text.Json.Serialization;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    /// Represent an amount.
    /// </summary>
    public class DecimalField : BaseField
    {
        /// <summary>
        /// An amount value.
        /// </summary>
        /// <example>5.89</example>
        [JsonPropertyName("value")]
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Value { get; set; }

        /// <summary>
        /// A pretty summary of the value.
        /// </summary>
        public override string ToString()
        {
            return SummaryHelper.FormatAmount(Value);
        }
    }
}
