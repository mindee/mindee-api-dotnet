using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;


namespace Mindee.Product.InternationalId
{
    /// <summary>
    /// The definition for International ID, API version 1.
    /// </summary>
    [Endpoint("international_id", "1")]
    public sealed class InternationalIdV1 : Inference<InternationalIdV1Document, InternationalIdV1Document>
    {
        /// <summary>
        /// The pages and the associated values which was detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<InternationalIdV1Document>))]
        public override Pages<InternationalIdV1Document> Pages { get; set; }
    }
}
