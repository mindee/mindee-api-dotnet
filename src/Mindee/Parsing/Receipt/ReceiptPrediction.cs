using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    public sealed class ReceiptPrediction : FinancialPredictionBase
    {
        public Category Category { get; set; }

        public Time Time { get; set; }
    }
}
