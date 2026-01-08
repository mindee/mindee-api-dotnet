using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.HealthCard
{
    /// <summary>
    ///     Health Card API version 1 inference prediction.
    /// </summary>
    [Endpoint("french_healthcard", "1")]
    public sealed class HealthCardV1 : Inference<HealthCardV1Document, HealthCardV1Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<HealthCardV1Document>))]
        public override Pages<HealthCardV1Document> Pages { get; set; }
    }
}
