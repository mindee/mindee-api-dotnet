using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.CarteVitale
{
    /// <summary>
    /// Carte Vitale API version 1 inference prediction.
    /// </summary>
    [Endpoint("carte_vitale", "1")]
    public sealed class CarteVitaleV1 : Inference<CarteVitaleV1Document, CarteVitaleV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<CarteVitaleV1Document>))]
        public override Pages<CarteVitaleV1Document> Pages { get; set; }
    }
}
