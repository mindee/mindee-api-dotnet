using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Us.BankCheck
{
    /// <summary>
    /// The definition for bank_check v1.
    /// </summary>
    [Endpoint("bank_check", "1")]
    public sealed class BankCheckV1 : Inference<BankCheckV1Page, BankCheckV1Document>
    {
    }
}
