using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Common
{
    public class Inference<TModel> where TModel : class, new()
    {
        [JsonPropertyName("extras")]
        public Extras Extras { get; set; }

        [JsonPropertyName("finished_at")]
        public DateTime FinishedAt { get; set; }

        [JsonPropertyName("pages")]
        public List<Page<TModel>> Pages { get; set; }

        [JsonPropertyName("prediction")]
        public TModel Prediction { get; set; }

        [JsonPropertyName("processing_time")]
        public double ProcessingTime { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; }

        [JsonPropertyName("started_at")]
        public DateTime StartedAt { get; set; }
    }
}
