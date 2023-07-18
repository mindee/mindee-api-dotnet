using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Invoice
{
    /// <summary>
    /// List of line item details.
    /// </summary>
    public class InvoiceV4LineItem : ILineItemField
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
        public string ToTableLine()
        {
            Dictionary<string, string> printable = PrintableValues();

            string tax = SummaryHelper.FormatAmount(TaxAmount);
            tax += TaxRate != null ? $" ({SummaryHelper.FormatAmount(TaxRate)}%)" : "";

            return "| "
                + String.Format("{0,-20}", printable["ProductCode"])
                + " | "
                + String.Format("{0,-7}", printable["Quantity"])
                + " | "
                + String.Format("{0,-7}", printable["UnitPrice"])
                + " | "
                + String.Format("{0,-8}", printable["TotalAmount"])
                + " | "
                + String.Format("{0,-16}", tax)
                + " | "
                + String.Format("{0,-36}", printable["Description"])
                + " |";
        }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Product Code: "
                + printable["ProductCode"]
                + ", Quantity: "
                + printable["Quantity"]
                + ", Unit Price: "
                + printable["UnitPrice"]
                + ", Total Amount: "
                + printable["TotalAmount"]
                + ", Tax Amount: "
                + printable["TaxAmount"]
                + ", Tax Rate: "
                + printable["TaxRate"]
                + ", Description: "
                + printable["Description"].Trim();
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
    /// Represent all the tax lines.
    /// </summary>
    public class InvoiceV4LineItems : List<InvoiceV4LineItem>
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
            int[] columnSizes = { 22, 9, 9, 10, 18, 38 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-'));
            outStr.Append("  | Code                 | QTY     | Price   | Amount   | Tax (Rate)       | Description                          |\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
