using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.BankAccountDetails
{
    /// <summary>
    /// The definition for bank_account_details v1.
    /// </summary>
    [Endpoint("bank_account_details", "1")]
    public class BankAccountDetailsV1 : Inference<BankAccountDetailsV1Document, BankAccountDetailsV1Document>
    {
    }
}
