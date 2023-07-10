using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Geometry;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Invoice
{
    /// <summary>
    /// Line items details.
    /// </summary>
    public class InvoiceV4LineItem : ILineItemField
    {
        /// <summary>
        /// The product code referring to the item.
        /// </summary>
        [JsonPropertyName("product_code")]
        public string ProductCode { get; set; }

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
        /// The item unit price.
        /// </summary>
        [JsonPropertyName("unit_price")]
        public double? UnitPrice { get; set; }

        /// <summary>
        /// The item total amount.
        /// </summary>
        [JsonPropertyName("total_amount")]
        public double? TotalAmount { get; set; }

        /// <summary>
        /// The item tax rate in percentage.
        /// </summary>
        [JsonPropertyName("tax_rate")]
        public double? TaxRate { get; set; }

        /// <summary>
        /// The item tax amount.
        /// </summary>
        [JsonPropertyName("tax_amount")]
        public double? TaxAmount { get; set; }

        /// <summary>
        /// Confidence score.
        /// </summary>
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; } = 0.0;

        /// <summary>
        /// The document page on which the information was found.
        /// </summary>
        [JsonPropertyName("page_id")]
        public double PageId { get; set; }

        /// <summary>
        /// Contains the relative vertices's coordinates (points) of a polygon containing
        /// the field in the document.
        /// </summary>
        [JsonPropertyName("polygon")]
        [JsonConverter(typeof(PolygonJsonConverter))]
        public Polygon Polygon { get; set; }

        private Dictionary<string, string> PrintableValues()
        {
            var printable = new Dictionary<string, string>();
            printable.Add("ProductCode", SummaryHelper.FormatString(ProductCode));
            printable.Add("Quantity", SummaryHelper.FormatAmount(Quantity));
            printable.Add("UnitPrice", SummaryHelper.FormatAmount(UnitPrice));
            printable.Add("TotalAmount", SummaryHelper.FormatAmount(TotalAmount));
            printable.Add("TaxAmount", SummaryHelper.FormatAmount(TaxAmount));
            printable.Add("TaxRate", SummaryHelper.FormatAmount(TaxRate));
            printable.Add("Description", SummaryHelper.FormatString(Description, 36));
            return printable;
        }

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
