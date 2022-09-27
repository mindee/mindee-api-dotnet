namespace Mindee.Prediction.Commun
{
    public abstract class PredictionBase
    {
        public DocumentType DocumentType { get; set; }
        public Locale Locale { get; set; }
        public Orientation Orientation { get; set; }
    }
}
