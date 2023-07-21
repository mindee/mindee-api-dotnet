using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.BankAccountDetails
{
    /// <summary>
    /// The definition for Bank Account Details, API version 1.
    /// </summary>
    [Endpoint("bank_account_details", "1")]
    public sealed class BankAccountDetailsV1 : Inference<BankAccountDetailsV1Document, BankAccountDetailsV1Document>
    {
    }
}
