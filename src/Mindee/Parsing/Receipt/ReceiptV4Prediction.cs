using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    /// <summary>
    /// The receipt model for the v4.
    /// </summary>
    [Endpoint("expense_receipts", "4")]
    public sealed class ReceiptV4Prediction : FinancialPredictionBase
    {
        /// <summary>
        /// The category of the receipt.
        /// </summary>
        [JsonPropertyName("category")]
        public StringField Category { get; set; }

        /// <summary>
        /// The subcategory of the receipt.
        /// </summary>
        [JsonPropertyName("subcategory")]
        public StringField SubCategory { get; set; }

        /// <summary>
        /// The supplier name.
        /// </summary>
        [JsonPropertyName("supplier")]
        public StringField Supplier { get; set; }

        /// <summary>
        /// <see cref="Time"/>
        /// </summary>
        [JsonPropertyName("time")]
        public Time Time { get; set; }

        /// <summary>
        /// Total amount including taxes and tips.
        /// </summary>
        [JsonPropertyName("total_amount")]
        public AmountField TotalAmount { get; set; }

        /// <summary>
        /// Total amount excluding taxes.
        /// </summary>
        [JsonPropertyName("total_net")]
        public AmountField TotalNet { get; set; }

        /// <summary>
        /// Total taxes.
        /// </summary>
        [JsonPropertyName("total_tax")]
        public AmountField TotalTax { get; set; }

        /// <summary>
        /// The tip.
        /// </summary>
        [JsonPropertyName("tip")]
        public AmountField Tip { get; set; }

        /// <summary>
        /// Generate a pretty to read summary of the model.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("----- Receipt V4 -----\n");
            result.Append($"Total amount: {TotalAmount}\n");
            result.Append($"Total net: {TotalNet}\n");
            result.Append($"Tip: {Tip}\n");
            result.Append($"Date: {Date.Value}\n");
            result.Append($"Category: {Category.Value}\n");
            result.Append($"Subcategory: {SubCategory.Value}\n");
            result.Append($"Document type: {DocumentType.Value}\n");
            result.Append($"Time: {Time.Value}\n");
            result.Append($"Supplier name: {Supplier.Value}\n");
            result.Append($"Taxes: {string.Join("\n                 ", Taxes.Select(t => t))}\n");
            result.Append($"Total taxes: {TotalTax}\n");
            result.Append($"Locale: {Locale}\n");

            result.Append("----------------------\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
