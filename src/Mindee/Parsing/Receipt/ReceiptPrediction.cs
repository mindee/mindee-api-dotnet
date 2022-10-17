using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    [Endpoint("expense_receipts", "v3")]
    public sealed class ReceiptPrediction : FinancialPredictionBase
    {
        [JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("time")]
        public Time Time { get; set; }

    }
}
