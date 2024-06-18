using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    /// Represent a company registration.
    /// </summary>
    public class CompanyRegistration : BaseField
    {
        /// <summary>
        /// Type of the company registration number.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The value of the field.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }


        /// <summary>
        /// Print as a table line for RST display.
        /// </summary>
        /// <returns></returns>
        public string ToTableLine()
        {
            var printable = PrintableValues();
            return $"| {printable["type"],-15} | {printable["value"],-20} ";
        }

        /// <summary>
        /// A pretty summary of the value.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return $"Type: {printable["type"]}, Value: {printable["value"]}";
        }

        private Dictionary<string, string> PrintableValues()
        {
            var printable = new Dictionary<string, string>
            {
                ["type"] = SummaryHelper.FormatString(Type),
                ["value"] = SummaryHelper.FormatString(Value)
            };
            return printable;
        }
    }
}
