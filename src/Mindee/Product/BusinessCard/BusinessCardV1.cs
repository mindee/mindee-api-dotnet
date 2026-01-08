using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.BusinessCard
{
    /// <summary>
    ///     Business Card API version 1 inference prediction.
    /// </summary>
    [Endpoint("business_card", "1")]
    public sealed class BusinessCardV1 : Inference<BusinessCardV1Document, BusinessCardV1Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<BusinessCardV1Document>))]
        public override Pages<BusinessCardV1Document> Pages { get; set; }
    }
}
