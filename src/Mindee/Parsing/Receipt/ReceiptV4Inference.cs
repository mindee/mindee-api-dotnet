using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    /// <summary>
    /// The receipt v4 definition.
    /// </summary>
    [Endpoint("expense_receipts", "4")]
    public class ReceiptV4Inference : Inference<ReceiptV4DocumentPrediction, ReceiptV4DocumentPrediction>
    {
    }
}
