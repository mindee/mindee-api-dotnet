using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Us.HealthcareCard
{
    /// <summary>
    ///     Healthcare Card API version 1 inference prediction.
    /// </summary>
    [Endpoint("us_healthcare_cards", "1")]
    public sealed class HealthcareCardV1 : Inference<HealthcareCardV1Document, HealthcareCardV1Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<HealthcareCardV1Document>))]
        public override Pages<HealthcareCardV1Document> Pages { get; set; }
    }
}
