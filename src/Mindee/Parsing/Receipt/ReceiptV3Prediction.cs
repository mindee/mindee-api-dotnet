using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    [Endpoint("expense_receipts", "3")]
    public sealed class ReceiptV3Prediction : FinancialPredictionBase
    {
        [JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("time")]
        public Time Time { get; set; }

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
