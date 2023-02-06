using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Geometry;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    /// <summary>
    /// Line items details.
    /// </summary>
    public class InvoiceLineItem
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

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string productCode = ProductCode?.ToString() ?? "";
            string quantity = SummaryHelper.FormatAmount(Quantity);
            string unitPrice = SummaryHelper.FormatAmount(UnitPrice);
            string totalAmount = SummaryHelper.FormatAmount(TotalAmount);
            string tax = SummaryHelper.FormatAmount(TaxAmount);
            tax += TaxRate != null ? $" ({SummaryHelper.FormatAmount(TaxRate)}%)" : "";
            string description = Description ?? "";
            if (description.Length > 33)
            {
                description = description.Substring(0, 33) + "...";
            }

            return string.Join(" ",
                productCode.PadRight(22),
                quantity.PadRight(8),
                unitPrice.PadRight(9),
                totalAmount.PadRight(10),
                tax.PadRight(18),
                description
                );
        }
    }
}
