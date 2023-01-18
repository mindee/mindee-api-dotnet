using Mindee.Parsing.Common;

namespace Mindee.Parsing.Us.BankCheck
{
    /// <summary>
    /// The bank check v4 definition.
    /// </summary>
    [Endpoint("bank_check", "1")]
    public class BankCheckV1Inference : Inference<BankCheckV1DocumentPrediction, BankCheckV1DocumentPrediction>
    {
    }
}
