﻿using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    public sealed class ReceiptPrediction : FinancialPredictionBase
    {
        [JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("time")]
        public Time Time { get; set; }
    }
}