using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Common
{
    public class Page<TModel> where TModel : class, new()
    {
        [JsonPropertyName("extras")]
        public Extras Extras { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("prediction")]
        public TModel Prediction { get; set; }
    }
}
