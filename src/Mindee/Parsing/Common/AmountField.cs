using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent an amount.
    /// </summary>
    public class AmountField : BaseField
    {
        /// <summary>
        /// An amount value.
        /// </summary>
        /// <example>5.89</example>
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        /// <summary>
        /// A pretty summary of the value.
        /// </summary>
        public override string ToString()
        {
            return SummaryHelper.FormatAmount(Value);
        }
    }
}
