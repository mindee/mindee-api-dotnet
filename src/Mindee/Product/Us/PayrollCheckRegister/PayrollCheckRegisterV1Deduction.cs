using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.PayrollCheckRegister
{
    /// <summary>
    /// The deductions.
    /// </summary>
    public sealed class PayrollCheckRegisterV1Deduction : LineItemField
    {
        /// <summary>
        /// The deduction line amount.
        /// </summary>
        [JsonPropertyName("amount")]
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Amount { get; set; }

        /// <summary>
        /// The deduction line code or type.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "| "
              + String.Format("{0,-6}", printable["Amount"])
              + " | "
              + String.Format("{0,-14}", printable["Code"])
              + " |";
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Amount", Amount.ToString()},
                {"Code", SummaryHelper.FormatString(Code)},
            };
        }
    }

    /// <summary>
    /// The deductions.
    /// </summary>
    public class PayrollCheckRegisterV1Deductions : List<PayrollCheckRegisterV1Deduction>
    {
        /// <summary>
        /// Default string representation.
        /// </summary>
        public override string ToString()
        {
            if (this.Count == 0)
            {
                return "\n";
            }
            int[] columnSizes = { 8, 16 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Amount ");
            outStr.Append("| Deduction Code ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
