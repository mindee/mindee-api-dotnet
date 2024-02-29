

using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Us.BankCheck
{
    /// <summary>
    /// The definition for Bank Check, API version 1.
    /// </summary>
    [Endpoint("bank_check", "1")]
    public sealed class BankCheckV1 : Inference<BankCheckV1Page, BankCheckV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<BankCheckV1Page>))]
        public override Pages<BankCheckV1Page> Pages { get; set; }
    }
}
