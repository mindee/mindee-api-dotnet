using Mindee.Prediction.Commun;

namespace Mindee.Prediction.Receipt
{
    public sealed class ReceiptPrediction : FinancialPredictionBase
    {
        public Category Category { get; set; }

        public Time Time { get; set; }
    }
}
