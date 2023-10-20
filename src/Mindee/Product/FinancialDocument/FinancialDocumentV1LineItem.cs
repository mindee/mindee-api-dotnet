using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.FinancialDocument
{
    /// <summary>
    /// List of line item details.
    /// </summary>
    public sealed class FinancialDocumentV1LineItem : LineItemField
    {
        /// <summary>
        /// The item description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The product code referring to the item.
        /// </summary>
        [JsonPropertyName("product_code")]
        public string ProductCode { get; set; }

        /// <summary>
        /// The item quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public double? Quantity { get; set; }

        /// <summary>
        /// The item tax amount.
        /// </summary>
        [JsonPropertyName("tax_amount")]
        public double? TaxAmount { get; set; }

        /// <summary>
        /// The item tax rate in percentage.
        /// </summary>
        [JsonPropertyName("tax_rate")]
        public double? TaxRate { get; set; }

        /// <summary>
        /// The item total amount.
        /// </summary>
        [JsonPropertyName("total_amount")]
        public double? TotalAmount { get; set; }

        /// <summary>
        /// The item unit price.
        /// </summary>
        [JsonPropertyName("unit_price")]
        public double? UnitPrice { get; set; }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "| "
              + String.Format("{0,-36}", printable["Description"])
              + " | "
              + String.Format("{0,-12}", printable["ProductCode"])
              + " | "
              + String.Format("{0,-8}", printable["Quantity"])
              + " | "
              + String.Format("{0,-10}", printable["TaxAmount"])
              + " | "
              + String.Format("{0,-12}", printable["TaxRate"])
              + " | "
              + String.Format("{0,-12}", printable["TotalAmount"])
              + " | "
              + String.Format("{0,-10}", printable["UnitPrice"])
              + " |";
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Description", SummaryHelper.FormatString(Description, 36)},
                {"ProductCode", SummaryHelper.FormatString(ProductCode)},
                {"Quantity", SummaryHelper.FormatAmount(Quantity)},
                {"TaxAmount", SummaryHelper.FormatAmount(TaxAmount)},
                {"TaxRate", SummaryHelper.FormatAmount(TaxRate)},
                {"TotalAmount", SummaryHelper.FormatAmount(TotalAmount)},
                {"UnitPrice", SummaryHelper.FormatAmount(UnitPrice)},
            };
        }
    }

    /// <summary>
    /// List of line item details.
    /// </summary>
    public class FinancialDocumentV1LineItems : List<FinancialDocumentV1LineItem>
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
            int[] columnSizes = { 38, 14, 10, 12, 14, 14, 12 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Description                          ");
            outStr.Append("| Product code ");
            outStr.Append("| Quantity ");
            outStr.Append("| Tax Amount ");
            outStr.Append("| Tax Rate (%) ");
            outStr.Append("| Total Amount ");
            outStr.Append("| Unit Price ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
