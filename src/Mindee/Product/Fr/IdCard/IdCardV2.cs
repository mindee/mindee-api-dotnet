using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.IdCard
{
    /// <summary>
    /// Carte Nationale d'Identité API version 2 inference prediction.
    /// </summary>
    [Endpoint("idcard_fr", "2")]
    public sealed class IdCardV2 : Inference<IdCardV2Page, IdCardV2Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<IdCardV2Page>))]
        public override Pages<IdCardV2Page> Pages { get; set; }
    }
}
