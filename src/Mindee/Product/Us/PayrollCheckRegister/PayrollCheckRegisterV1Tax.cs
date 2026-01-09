using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.PayrollCheckRegister
{
    /// <summary>
    /// The taxes.
    /// </summary>
    public sealed class PayrollCheckRegisterV1Tax : LineItemField
    {
        /// <summary>
        /// The tax line amount.
        /// </summary>
        [JsonPropertyName("amount")]
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Amount { get; set; }

        /// <summary>
        /// The tax line base.
        /// </summary>
        [JsonPropertyName("base")]
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Base { get; set; }

        /// <summary>
        /// The tax line code or type.
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
              + String.Format("{0,-4}", printable["Base"])
              + " | "
              + String.Format("{0,-4}", printable["Code"])
              + " |";
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Amount", SummaryHelper.FormatAmount(Amount)},
                {"Base", SummaryHelper.FormatAmount(Amount)},
                {"Code", SummaryHelper.FormatString(Code)},
            };
        }
    }

    /// <summary>
    /// The taxes.
    /// </summary>
    public class PayrollCheckRegisterV1Taxes : List<PayrollCheckRegisterV1Tax>
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
            int[] columnSizes = { 8, 6, 6 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Amount ");
            outStr.Append("| Base ");
            outStr.Append("| Code ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
