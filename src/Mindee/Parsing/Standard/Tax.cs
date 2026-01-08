using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    ///     Represent a tax.
    /// </summary>
    public class Tax : LineItemField
    {
        /// <summary>
        ///     The rate of the taxe.
        /// </summary>
        /// <example>5</example>
        [JsonPropertyName("rate")]
        public double? Rate { get; set; }

        /// <summary>
        ///     The total amount of the tax.
        /// </summary>
        /// <example>10.5</example>
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        /// <summary>
        ///     The tax base.
        /// </summary>
        [JsonPropertyName("base")]
        public double? Base { get; set; }

        /// <summary>
        ///     The tax code.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        ///     Output a summary of the line.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "Base: "
                   + printable["base"]
                   + ", Code: "
                   + printable["code"]
                   + ", Rate (%): "
                   + printable["rate"]
                   + ", Amount: "
                   + printable["value"].Trim();
        }

        /// <summary>
        ///     Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            var printable = PrintableValues();
            return "| "
                   + string.Format("{0,-13}", printable["Base"])
                   + " | "
                   + string.Format("{0,-6}", printable["Code"])
                   + " | "
                   + string.Format("{0,-8}", printable["Rate"])
                   + " | "
                   + string.Format("{0,-13}", printable["Value"])
                   + " |";
        }

        private Dictionary<string, string> PrintableValues()
        {
            var printable = new Dictionary<string, string>();
            printable.Add("Base", SummaryHelper.FormatAmount(Base));
            printable.Add("Code", SummaryHelper.FormatString(Code));
            printable.Add("Rate", SummaryHelper.FormatAmount(Rate));
            printable.Add("Value", SummaryHelper.FormatAmount(Value));
            return printable;
        }
    }

    /// <summary>
    ///     Represent all the tax lines.
    /// </summary>
    public class Taxes : List<Tax>
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

            int[] columnSizes = { 15, 8, 10, 15 };
            var outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-'));
            outStr.Append("  | Base          | Code   | Rate (%) | Amount        |\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
