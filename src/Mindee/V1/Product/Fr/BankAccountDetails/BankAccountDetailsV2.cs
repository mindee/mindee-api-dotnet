using System.Text.Json.Serialization;
using Mindee.V1.Http;
using Mindee.V1.Parsing.Common;

namespace Mindee.V1.Product.Fr.BankAccountDetails
{
    /// <summary>
    /// Bank Account Details API version 2 inference prediction.
    /// </summary>
    [Endpoint("bank_account_details", "2")]
    public sealed class BankAccountDetailsV2 : Inference<BankAccountDetailsV2Document, BankAccountDetailsV2Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<BankAccountDetailsV2Document>))]
        public override Pages<BankAccountDetailsV2Document> Pages { get; set; }
    }
}
