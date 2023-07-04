using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.FinancialDocument
{
    /// <summary>
    /// The financial v1 definition.
    /// </summary>
    [Endpoint("financial_document", "1")]
    public class FinancialDocumentV1 : Inference<FinancialDocumentV1Document, FinancialDocumentV1Document>
    {
    }
}
