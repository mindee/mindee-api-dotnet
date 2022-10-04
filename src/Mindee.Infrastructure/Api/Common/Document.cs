using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Common
{
    public class Document<TModel> where TModel : class, new()
    {
        [JsonPropertyName("annotations")]
        public Annotations Annotations { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("inference")]
        public Inference<TModel> Inference { get; set; }

        [JsonPropertyName("n_pages")]
        public int NPages { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ocr")]
        public Ocr Ocr { get; set; }
    }
}
