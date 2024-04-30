

using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.BankAccountDetails
{
    /// <summary>
    /// Bank Account Details API version 1 inference prediction.
    /// </summary>
    [Endpoint("bank_account_details", "1")]
    public sealed class BankAccountDetailsV1 : Inference<BankAccountDetailsV1Document, BankAccountDetailsV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<BankAccountDetailsV1Document>))]
        public override Pages<BankAccountDetailsV1Document> Pages { get; set; }
    }
}
