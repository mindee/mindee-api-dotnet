

using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.IdCard
{
    /// <summary>
    /// Carte Nationale d'Identité API version 1 inference prediction.
    /// </summary>
    [Endpoint("idcard_fr", "1")]
    public sealed class IdCardV1 : Inference<IdCardV1Page, IdCardV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<IdCardV1Page>))]
        public override Pages<IdCardV1Page> Pages { get; set; }
    }
}
