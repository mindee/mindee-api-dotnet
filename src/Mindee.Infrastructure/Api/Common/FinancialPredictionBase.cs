﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Common
{
    public abstract class FinancialPredictionBase : PredictionBase
    {
        [JsonPropertyName("date")]
        public Date Date { get; set; }

        [JsonPropertyName("supplier")]
        public Supplier Supplier { get; set; }

        [JsonPropertyName("taxes")]
        public List<Tax> Taxes { get; set; }

        [JsonPropertyName("total_incl")]
        public TotalIncl TotalIncl { get; set; }
    }
}