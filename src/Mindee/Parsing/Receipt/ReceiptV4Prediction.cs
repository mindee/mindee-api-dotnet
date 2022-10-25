using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    [Endpoint("expense_receipts", "4")]
    public sealed class ReceiptV4Prediction : FinancialPredictionBase
    {
        [JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("time")]
        public Time Time { get; set; }

        [JsonPropertyName("total_amount")]
        public Amount TotalAmount { get; set; }
        
        [JsonPropertyName("total_net")]
        public Amount TotalNet { get; set; }
        
        [JsonPropertyName("total_tax")]
        public Amount TotalTax { get; set; }

        [JsonPropertyName("tip")]
        public Amount Tip { get; set; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("-----Receipt data-----\n");
            result.Append($"Total amount including taxes: {TotalAmount}\n");
            result.Append($"Total amount excluding taxes: {TotalNet}\n");
            result.Append($"Tip: {Tip}\n");
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
