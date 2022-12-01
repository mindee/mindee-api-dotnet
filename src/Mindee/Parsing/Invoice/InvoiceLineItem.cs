using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    /// <summary>
    /// Line items details.
    /// </summary>
    public sealed class InvoiceLineItem : FinancialPredictionBase
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
        /// Contains the relative vertices coordinates (points) of a polygon containing
        /// the field in the document.
        /// </summary>
        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        /// <summary>
        /// A prettier reprensentation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string productCode = ProductCode?.ToString() ?? "";
            string quantity = Quantity?.ToString() ?? "";
            string unitPrice = UnitPrice?.ToString() ?? "";
            string totalAmount = TotalAmount?.ToString() ?? "";
            string tax = TotalAmount?.ToString() ?? "";
            tax += TaxRate != null ? $" ({TaxRate} %)" : "";
            string description = Description ?? "";
            if (description.Length > 32)
            {
                description = description.Substring(0, 32) + "...";
            }

            return string.Join(" | ",
                productCode.PadRight(14),
                quantity.ToString().PadRight(6),
                unitPrice.PadRight(7),
                totalAmount.PadRight(8),
                tax.PadRight(14),
                description
                );
        }
    }
}
