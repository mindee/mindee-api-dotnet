using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.EnergyBill
{
    /// <summary>
    /// Details of Taxes and Contributions.
    /// </summary>
    public sealed class EnergyBillV1TaxesAndContribution : LineItemField
    {
        /// <summary>
        /// Description or details of the Taxes and Contributions.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The end date of the Taxes and Contributions.
        /// </summary>
        [JsonPropertyName("end_date")]
        public string EndDate { get; set; }

        /// <summary>
        /// The start date of the Taxes and Contributions.
        /// </summary>
        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        /// <summary>
        /// The rate of tax applied to the total cost.
        /// </summary>
        [JsonPropertyName("tax_rate")]
        public double? TaxRate { get; set; }

        /// <summary>
        /// The total cost of Taxes and Contributions.
        /// </summary>
        [JsonPropertyName("total")]
        public double? Total { get; set; }

        /// <summary>
        /// The price per unit of Taxes and Contributions.
        /// </summary>
        [JsonPropertyName("unit_price")]
        public double? UnitPrice { get; set; }

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Description", SummaryHelper.FormatString(Description, 36)},
                {"EndDate", SummaryHelper.FormatString(EndDate, 10)},
                {"StartDate", SummaryHelper.FormatString(StartDate)},
                {"TaxRate", SummaryHelper.FormatAmount(TaxRate)},
                {"Total", SummaryHelper.FormatAmount(Total)},
                {"UnitPrice", SummaryHelper.FormatAmount(UnitPrice)},
            };
        }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = TablePrintableValues();
            return "| "
              + String.Format("{0,-36}", printable["Description"])
              + " | "
              + String.Format("{0,-10}", printable["EndDate"])
              + " | "
              + String.Format("{0,-10}", printable["StartDate"])
              + " | "
              + String.Format("{0,-8}", printable["TaxRate"])
              + " | "
              + String.Format("{0,-9}", printable["Total"])
              + " | "
              + String.Format("{0,-10}", printable["UnitPrice"])
              + " |";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Description: "
              + printable["Description"]
              + ", End Date: "
              + printable["EndDate"]
              + ", Start Date: "
              + printable["StartDate"]
              + ", Tax Rate: "
              + printable["TaxRate"]
              + ", Total: "
              + printable["Total"]
              + ", Unit Price: "
              + printable["UnitPrice"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Description", SummaryHelper.FormatString(Description)},
                {"EndDate", SummaryHelper.FormatString(EndDate)},
                {"StartDate", SummaryHelper.FormatString(StartDate)},
                {"TaxRate", SummaryHelper.FormatAmount(TaxRate)},
                {"Total", SummaryHelper.FormatAmount(Total)},
                {"UnitPrice", SummaryHelper.FormatAmount(UnitPrice)},
            };
        }
    }

    /// <summary>
    /// Details of Taxes and Contributions.
    /// </summary>
    public class EnergyBillV1TaxesAndContributions : List<EnergyBillV1TaxesAndContribution>
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
            int[] columnSizes = { 38, 12, 12, 10, 11, 12 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Description                          ");
            outStr.Append("| End Date   ");
            outStr.Append("| Start Date ");
            outStr.Append("| Tax Rate ");
            outStr.Append("| Total     ");
            outStr.Append("| Unit Price ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
