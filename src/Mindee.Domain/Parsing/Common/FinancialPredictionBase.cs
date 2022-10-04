using System.Collections.Generic;

namespace Mindee.Domain.Parsing.Common
{
    public abstract class FinancialPredictionBase : PredictionBase
    {
        public DocumentType DocumentType { get; set; }

        public Date Date { get; set; }

        public Supplier Supplier { get; set; }

        public List<Tax> Taxes { get; set; }

        public TotalIncl TotalIncl { get; set; }
    }
}
