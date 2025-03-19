using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Receipt
{
    /// <summary>
    /// List of all line items on the receipt.
    /// </summary>
    public sealed class ReceiptV5LineItem : LineItemField
    {
        /// <summary>
        /// The item description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The item quantity.
        /// </summary>
        [JsonPropertyName("quantity")]
        public double? Quantity { get; set; }

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

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Description", SummaryHelper.FormatString(Description, 36)},
                {"Quantity", SummaryHelper.FormatAmount(Quantity)},
                {"TotalAmount", SummaryHelper.FormatAmount(TotalAmount)},
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
              + String.Format("{0,-8}", printable["Quantity"])
              + " | "
              + String.Format("{0,-12}", printable["TotalAmount"])
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
              + ", Quantity: "
              + printable["Quantity"]
              + ", Total Amount: "
              + printable["TotalAmount"]
              + ", Unit Price: "
              + printable["UnitPrice"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Description", SummaryHelper.FormatString(Description)},
                {"Quantity", SummaryHelper.FormatAmount(Quantity)},
                {"TotalAmount", SummaryHelper.FormatAmount(TotalAmount)},
                {"UnitPrice", SummaryHelper.FormatAmount(UnitPrice)},
            };
        }
    }

    /// <summary>
    /// List of all line items on the receipt.
    /// </summary>
    public class ReceiptV5LineItems : List<ReceiptV5LineItem>
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
            int[] columnSizes = { 38, 10, 14, 12 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Description                          ");
            outStr.Append("| Quantity ");
            outStr.Append("| Total Amount ");
            outStr.Append("| Unit Price ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
