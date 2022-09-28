using System.Collections.Generic;

namespace Mindee.Prediction.Commun
{
    public abstract class FinancialPredictionBase : PredictionBase
    {
        public Date Date { get; set; }

        public Supplier Supplier { get; set; }

        public List<Taxis> Taxes { get; set; }

        public TotalIncl TotalIncl { get; set; }
    }
}
