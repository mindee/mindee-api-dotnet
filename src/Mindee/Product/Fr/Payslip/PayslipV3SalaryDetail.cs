using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    ///     Detailed information about the earnings.
    /// </summary>
    public sealed class PayslipV3SalaryDetail : LineItemField
    {
        /// <summary>
        ///     The amount of the earning.
        /// </summary>
        [JsonPropertyName("amount")]
        public double? Amount { get; set; }

        /// <summary>
        ///     The base rate value of the earning.
        /// </summary>
        [JsonPropertyName("base")]
        public double? Base { get; set; }

        /// <summary>
        ///     The description of the earnings.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The number of units in the earning.
        /// </summary>
        [JsonPropertyName("number")]
        public double? Number { get; set; }

        /// <summary>
        ///     The rate of the earning.
        /// </summary>
        [JsonPropertyName("rate")]
        public double? Rate { get; set; }

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "Amount", SummaryHelper.FormatAmount(Amount) },
                { "Base", SummaryHelper.FormatAmount(Base) },
                { "Description", SummaryHelper.FormatString(Description, 36) },
                { "Number", SummaryHelper.FormatAmount(Number) },
                { "Rate", SummaryHelper.FormatAmount(Rate) }
            };
        }

        /// <summary>
        ///     Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            var printable = TablePrintableValues();
            return "| "
                   + string.Format("{0,-12}", printable["Amount"])
                   + " | "
                   + string.Format("{0,-9}", printable["Base"])
                   + " | "
                   + string.Format("{0,-36}", printable["Description"])
                   + " | "
                   + string.Format("{0,-6}", printable["Number"])
                   + " | "
                   + string.Format("{0,-9}", printable["Rate"])
                   + " |";
        }

        /// <summary>
        ///     A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "Amount: "
                   + printable["Amount"]
                   + ", Base: "
                   + printable["Base"]
                   + ", Description: "
                   + printable["Description"]
                   + ", Number: "
                   + printable["Number"]
                   + ", Rate: "
                   + printable["Rate"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "Amount", SummaryHelper.FormatAmount(Amount) },
                { "Base", SummaryHelper.FormatAmount(Base) },
                { "Description", SummaryHelper.FormatString(Description) },
                { "Number", SummaryHelper.FormatAmount(Number) },
                { "Rate", SummaryHelper.FormatAmount(Rate) }
            };
        }
    }

    /// <summary>
    ///     Detailed information about the earnings.
    /// </summary>
    public class PayslipV3SalaryDetails : List<PayslipV3SalaryDetail>
    {
        /// <summary>
        ///     Default string representation.
        /// </summary>
        public override string ToString()
        {
            if (Count == 0)
            {
                return "\n";
            }

            int[] columnSizes = { 14, 11, 38, 8, 11 };
            var outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Amount       ");
            outStr.Append("| Base      ");
            outStr.Append("| Description                          ");
            outStr.Append("| Number ");
            outStr.Append("| Rate      ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
