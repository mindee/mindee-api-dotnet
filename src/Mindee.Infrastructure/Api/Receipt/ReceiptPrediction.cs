using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Infrastructure.Api.Commun;

namespace Mindee.Infrastructure.Api.Receipt
{
    internal class ReceiptPrediction : FinancialPredictionBase
    {
        [JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("time")]
        public Time Time { get; set; }
    }

    public class Category
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class Time
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }
    }

}
