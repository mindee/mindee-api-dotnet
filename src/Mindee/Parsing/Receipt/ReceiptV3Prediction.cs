using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    /// <summary>
    /// The receipt model for the v3.
    /// </summary>
    [Endpoint("expense_receipts", "3")]
    public sealed class ReceiptV3Prediction : FinancialPredictionBase
    {
        /// <summary>
        /// The category of the receipt.
        /// </summary>
        [JsonPropertyName("category")]
        public StringField Category { get; set; }
        
        /// <summary>
        /// <see cref="Receipt.Time"/>
        /// </summary>
        [JsonPropertyName("time")]
        public Time Time { get; set; }

        /// <summary>
        /// <see cref="Common.TotalIncl"/>
        /// </summary>
        [JsonPropertyName("total_incl")]
        public TotalIncl TotalIncl { get; set; }

        /// <summary>
        /// <see cref="Common.TotalExcl"/>
        /// </summary>
        [JsonPropertyName("total_excl")]
        public TotalExcl TotalExcl { get; set; }

        /// <summary>
        /// Generate a pretty to read summary of the model.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("-----Receipt data-----\n");
            result.Append($"Total amount including taxes: {TotalIncl.Value}\n");
            result.Append($"Total amount excluding taxes: {TotalExcl.Value}\n");
            result.Append($"Date: {Date.Value}\n");
            result.Append($"Category: {Category.Value}\n");
            result.Append($"Time: {Time.Value}\n");
            result.Append($"Merchant name: {Supplier.Value}\n");
            result.Append($"Taxes: {string.Join("\n                 ", Taxes.Select(t => t))}\n");
            result.Append($"Total taxes: {Locale}\n");
            result.Append($"Locale: {Locale}\n");

            result.Append("----------------------");

            return result.ToString();
        }
    }
}
