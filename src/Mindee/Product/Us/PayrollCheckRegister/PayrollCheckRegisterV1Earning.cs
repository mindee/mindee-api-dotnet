using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.PayrollCheckRegister
{
    /// <summary>
    /// The earnings.
    /// </summary>
    public sealed class PayrollCheckRegisterV1Earning : ILineItemField
    {
        /// <summary>
        /// The earning line amount.
        /// </summary>
        [JsonPropertyName("amount")]
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Amount { get; set; }

        /// <summary>
        /// The earning line title or type.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// The earning line hours.
        /// </summary>
        [JsonPropertyName("hours")]
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Hours { get; set; }

        /// <summary>
        /// The earning line rate.
        /// </summary>
        [JsonPropertyName("rate")]
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Rate { get; set; }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public string ToTableLine()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "| "
              + String.Format("{0,-6}", printable["Amount"])
              + " | "
              + String.Format("{0,-12}", printable["Code"])
              + " | "
              + String.Format("{0,-5}", printable["Hours"])
              + " | "
              + String.Format("{0,-4}", printable["Rate"])
              + " |";
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Amount", SummaryHelper.FormatAmount(Amount)},
                {"Code", SummaryHelper.FormatString(Code)},
                {"Hours", SummaryHelper.FormatAmount(Hours)},
                {"Rate", SummaryHelper.FormatAmount(Rate)},
            };
        }
    }

    /// <summary>
    /// The earnings.
    /// </summary>
    public class PayrollCheckRegisterV1Earnings : List<PayrollCheckRegisterV1Earning>
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
            int[] columnSizes = { 8, 14, 7, 6 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Amount ");
            outStr.Append("| Earning Code ");
            outStr.Append("| Hours ");
            outStr.Append("| rate ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
