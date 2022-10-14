using System.Text.Json.Serialization;
using Mindee.Domain.Parsing.Common;

namespace Mindee.Domain.Parsing.Receipt
{
    public sealed class ReceiptPrediction : FinancialPredictionBase
    {
        [JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("time")]
        public Time Time { get; set; }
    }
}
