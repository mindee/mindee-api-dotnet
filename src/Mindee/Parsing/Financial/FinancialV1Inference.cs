using Mindee.Parsing.Common;

namespace Mindee.Parsing.Financial
{
    /// <summary>
    /// The financial v1 definition.
    /// </summary>
    [Endpoint("financial_document", "1")]
    public class FinancialV1Inference : Inference<FinancialV1DocumentPrediction, FinancialV1DocumentPrediction>
    {
    }
}
