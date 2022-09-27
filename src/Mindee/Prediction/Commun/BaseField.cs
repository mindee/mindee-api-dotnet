using System.Collections.Generic;

namespace Mindee.Prediction.Commun
{
    public abstract class BaseField
    {
        public double Confidence { get; set; }

        public List<List<double>> Polygon { get; set; }

        public int? PageId { get; set; }

    }
}
