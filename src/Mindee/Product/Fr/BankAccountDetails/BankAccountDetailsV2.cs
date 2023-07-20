using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.BankAccountDetails
{
    /// <summary>
    /// The definition for bank_account_details v2.
    /// </summary>
    [Endpoint("bank_account_details", "2")]
    public sealed class BankAccountDetailsV2 : Inference<BankAccountDetailsV2Document, BankAccountDetailsV2Document>
    {
    }
}
