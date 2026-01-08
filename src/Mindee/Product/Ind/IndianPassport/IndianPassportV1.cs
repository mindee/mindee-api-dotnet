using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Ind.IndianPassport
{
    /// <summary>
    ///     Passport - India API version 1 inference prediction.
    /// </summary>
    [Endpoint("ind_passport", "1")]
    public sealed class IndianPassportV1 : Inference<IndianPassportV1Document, IndianPassportV1Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<IndianPassportV1Document>))]
        public override Pages<IndianPassportV1Document> Pages { get; set; }
    }
}
