using Mindee.Domain.Parsing.Common;

namespace Mindee.Domain.Parsing.Receipt
{
    public sealed class ReceiptPrediction : FinancialPredictionBase
    {
        public Category Category { get; set; }

        public Time Time { get; set; }
    }
}
