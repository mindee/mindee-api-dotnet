using System.Text.Json.Serialization;

namespace Mindee.V2.Parsing.Search
{
    /// <summary>
    ///  Information about a model's webhook.
    /// </summary>
    public class ModelWebhook
    {
        /// <summary>
        /// ID of the webhook.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the webhook.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// URL of the webhook.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// String representation of the webhook.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $":Name: {Name}\n:ID: {Id}\n:URL: {Url}";
        }
    }
}
