using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.CarteGrise
{
    /// <summary>
    /// Carte Grise API version 1 inference prediction.
    /// </summary>
    [Endpoint("carte_grise", "1")]
    public sealed class CarteGriseV1 : Inference<CarteGriseV1Document, CarteGriseV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<CarteGriseV1Document>))]
        public override Pages<CarteGriseV1Document> Pages { get; set; }
    }
}
