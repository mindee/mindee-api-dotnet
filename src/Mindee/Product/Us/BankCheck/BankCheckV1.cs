using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Us.BankCheck
{
    /// <summary>
    /// The bank check v4 definition.
    /// </summary>
    [Endpoint("bank_check", "1")]
    public class BankCheckV1 : Inference<BankCheckV1Document, BankCheckV1Document>
    {
    }
}
