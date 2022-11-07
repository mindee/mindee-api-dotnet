using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent a tax.
    /// </summary>
    public class Tax : BaseField
    {
        /// <summary>
        /// The rate of the taxe.
        /// </summary>
        /// <example>5</example>
        [JsonPropertyName("rate")]
        public double? Rate { get; set; }

        /// <summary>
        /// The total amount of the tax.
        /// </summary>
        /// <example>10.5</example>
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        /// <summary>
        /// The tax base.
        /// </summary>
        [JsonPropertyName("base")]
        public double? Base { get; set; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            if (Value != null)
            {
                result.Append(Value);
            }

            if (Rate != null)
            {
                result.Append($" {Rate}%");
            }

            if (Base != null)
            {
                result.Append($" {Base}%");
            }

            return result.ToString().Trim();
        }
    }
}
