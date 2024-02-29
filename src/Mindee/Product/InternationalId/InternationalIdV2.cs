

using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.InternationalId
{
    /// <summary>
    /// The definition for International ID, API version 2.
    /// </summary>
    [Endpoint("international_id", "2")]
    public sealed class InternationalIdV2 : Inference<InternationalIdV2Document, InternationalIdV2Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<InternationalIdV2Document>))]
        public override Pages<InternationalIdV2Document> Pages { get; set; }
    }
}
