using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Common
{
    public class Tax
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("rate")]
        public double Rate { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }
}
