using Mindee.Parsing.Common;

namespace Mindee.Parsing.InvoiceSplitter
{
    /// <summary>
    /// The invoice splitter v1 definition.
    /// </summary>
    [Endpoint("invoice_splitter", "1")]
    public class InvoiceSplitterV1Inference : Inference<InvoiceSplitterV1DocumentPrediction, InvoiceSplitterV1DocumentPrediction>
    {
    }
}
