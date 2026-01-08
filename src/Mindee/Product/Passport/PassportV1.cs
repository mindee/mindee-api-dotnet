using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Passport
{
    /// <summary>
    ///     Passport API version 1 inference prediction.
    /// </summary>
    [Endpoint("passport", "1")]
    public sealed class PassportV1 : Inference<PassportV1Document, PassportV1Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<PassportV1Document>))]
        public override Pages<PassportV1Document> Pages { get; set; }
    }
}
