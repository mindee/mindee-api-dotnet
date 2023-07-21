using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.FinancialDocument
{
    /// <summary>
    /// The definition for Financial Document, API version 1.
    /// </summary>
    [Endpoint("financial_document", "1")]
    public sealed class FinancialDocumentV1 : Inference<FinancialDocumentV1Document, FinancialDocumentV1Document>
    {
    }
}
