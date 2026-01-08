using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Invoice
{
    /// <summary>
    ///     List of all the line items present on the invoice.
    /// </summary>
    public sealed class InvoiceV4LineItem : LineItemField
    {
        /// <summary>
        ///     The item description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The product code of the item.
        /// </summary>
        [JsonPropertyName("product_code")]
        public string ProductCode { get; set; }

        /// <summary>
        ///     The item quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public double? Quantity { get; set; }

        /// <summary>
        ///     The item tax amount.
        /// </summary>
        [JsonPropertyName("tax_amount")]
        public double? TaxAmount { get; set; }

        /// <summary>
        ///     The item tax rate in percentage.
        /// </summary>
        [JsonPropertyName("tax_rate")]
        public double? TaxRate { get; set; }

        /// <summary>
        ///     The item total amount.
        /// </summary>
        [JsonPropertyName("total_amount")]
        public double? TotalAmount { get; set; }

        /// <summary>
        ///     The item unit of measure.
        /// </summary>
        [JsonPropertyName("unit_measure")]
        public string UnitMeasure { get; set; }

        /// <summary>
        ///     The item unit price.
        /// </summary>
        [JsonPropertyName("unit_price")]
        public double? UnitPrice { get; set; }

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "Description", SummaryHelper.FormatString(Description, 36) },
                { "ProductCode", SummaryHelper.FormatString(ProductCode) },
                { "Quantity", SummaryHelper.FormatAmount(Quantity) },
                { "TaxAmount", SummaryHelper.FormatAmount(TaxAmount) },
                { "TaxRate", SummaryHelper.FormatAmount(TaxRate) },
                { "TotalAmount", SummaryHelper.FormatAmount(TotalAmount) },
                { "UnitMeasure", SummaryHelper.FormatString(UnitMeasure) },
                { "UnitPrice", SummaryHelper.FormatAmount(UnitPrice) }
            };
        }

        /// <summary>
        ///     Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            var printable = TablePrintableValues();
            return "| "
                   + string.Format("{0,-36}", printable["Description"])
                   + " | "
                   + string.Format("{0,-12}", printable["ProductCode"])
                   + " | "
                   + string.Format("{0,-8}", printable["Quantity"])
                   + " | "
                   + string.Format("{0,-10}", printable["TaxAmount"])
                   + " | "
                   + string.Format("{0,-12}", printable["TaxRate"])
                   + " | "
                   + string.Format("{0,-12}", printable["TotalAmount"])
                   + " | "
                   + string.Format("{0,-15}", printable["UnitMeasure"])
                   + " | "
                   + string.Format("{0,-10}", printable["UnitPrice"])
                   + " |";
        }

        /// <summary>
        ///     A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "Description: "
                   + printable["Description"]
                   + ", Product code: "
                   + printable["ProductCode"]
                   + ", Quantity: "
                   + printable["Quantity"]
                   + ", Tax Amount: "
                   + printable["TaxAmount"]
                   + ", Tax Rate (%): "
                   + printable["TaxRate"]
                   + ", Total Amount: "
                   + printable["TotalAmount"]
                   + ", Unit of measure: "
                   + printable["UnitMeasure"]
                   + ", Unit Price: "
                   + printable["UnitPrice"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "Description", SummaryHelper.FormatString(Description) },
                { "ProductCode", SummaryHelper.FormatString(ProductCode) },
                { "Quantity", SummaryHelper.FormatAmount(Quantity) },
                { "TaxAmount", SummaryHelper.FormatAmount(TaxAmount) },
                { "TaxRate", SummaryHelper.FormatAmount(TaxRate) },
                { "TotalAmount", SummaryHelper.FormatAmount(TotalAmount) },
                { "UnitMeasure", SummaryHelper.FormatString(UnitMeasure) },
                { "UnitPrice", SummaryHelper.FormatAmount(UnitPrice) }
            };
        }
    }

    /// <summary>
    ///     List of all the line items present on the invoice.
    /// </summary>
    public class InvoiceV4LineItems : List<InvoiceV4LineItem>
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

            int[] columnSizes = { 38, 14, 10, 12, 14, 14, 17, 12 };
            var outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Description                          ");
            outStr.Append("| Product code ");
            outStr.Append("| Quantity ");
            outStr.Append("| Tax Amount ");
            outStr.Append("| Tax Rate (%) ");
            outStr.Append("| Total Amount ");
            outStr.Append("| Unit of measure ");
            outStr.Append("| Unit Price ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
