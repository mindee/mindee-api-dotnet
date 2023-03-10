using Mindee.Parsing.Common;

namespace Mindee.Parsing.Fr.BankAccountDetails
{
    /// <summary>
    /// The definition for bank_account_details v1.
    /// </summary>
    [Endpoint("bank_account_details", "1")]
    public class BankAccountDetailsV1Inference : Inference<BankAccountDetailsV1DocumentPrediction, BankAccountDetailsV1DocumentPrediction>
    {
    }
}
