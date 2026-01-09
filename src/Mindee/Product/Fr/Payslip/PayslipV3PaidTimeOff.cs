using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    /// Information about paid time off.
    /// </summary>
    public sealed class PayslipV3PaidTimeOff : LineItemField
    {
        /// <summary>
        /// The amount of paid time off accrued in the period.
        /// </summary>
        [JsonPropertyName("accrued")]
        public double? Accrued { get; set; }

        /// <summary>
        /// The paid time off period.
        /// </summary>
        [JsonPropertyName("period")]
        public string Period { get; set; }

        /// <summary>
        /// The type of paid time off.
        /// </summary>
        [JsonPropertyName("pto_type")]
        public string PtoType { get; set; }

        /// <summary>
        /// The remaining amount of paid time off at the end of the period.
        /// </summary>
        [JsonPropertyName("remaining")]
        public double? Remaining { get; set; }

        /// <summary>
        /// The amount of paid time off used in the period.
        /// </summary>
        [JsonPropertyName("used")]
        public double? Used { get; set; }

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Accrued", SummaryHelper.FormatAmount(Accrued)},
                {"Period", SummaryHelper.FormatString(Period, 6)},
                {"PtoType", SummaryHelper.FormatString(PtoType, 11)},
                {"Remaining", SummaryHelper.FormatAmount(Remaining)},
                {"Used", SummaryHelper.FormatAmount(Used)},
            };
        }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = TablePrintableValues();
            return "| "
              + String.Format("{0,-9}", printable["Accrued"])
              + " | "
              + String.Format("{0,-6}", printable["Period"])
              + " | "
              + String.Format("{0,-11}", printable["PtoType"])
              + " | "
              + String.Format("{0,-9}", printable["Remaining"])
              + " | "
              + String.Format("{0,-9}", printable["Used"])
              + " |";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Accrued: "
              + printable["Accrued"]
              + ", Period: "
              + printable["Period"]
              + ", Type: "
              + printable["PtoType"]
              + ", Remaining: "
              + printable["Remaining"]
              + ", Used: "
              + printable["Used"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Accrued", SummaryHelper.FormatAmount(Accrued)},
                {"Period", SummaryHelper.FormatString(Period)},
                {"PtoType", SummaryHelper.FormatString(PtoType)},
                {"Remaining", SummaryHelper.FormatAmount(Remaining)},
                {"Used", SummaryHelper.FormatAmount(Used)},
            };
        }
    }

    /// <summary>
    /// Information about paid time off.
    /// </summary>
    public class PayslipV3PaidTimeOffs : List<PayslipV3PaidTimeOff>
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
            int[] columnSizes = { 11, 8, 13, 11, 11 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Accrued   ");
            outStr.Append("| Period ");
            outStr.Append("| Type        ");
            outStr.Append("| Remaining ");
            outStr.Append("| Used      ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
