using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Common
{
    public class Error
    {
        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        public override string ToString()
        {
            return $"{Code} : {Message} - {Details}";
        }
    }
}
