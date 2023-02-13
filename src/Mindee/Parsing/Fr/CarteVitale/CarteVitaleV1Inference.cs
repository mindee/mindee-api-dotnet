using Mindee.Parsing.Common;

namespace Mindee.Parsing.Fr.CarteVitale
{
    /// <summary>
    /// The definition for carte_vitale v1.
    /// </summary>
    [Endpoint("carte_vitale", "1")]
    public class CarteVitaleV1Inference : Inference<CarteVitaleV1DocumentPrediction, CarteVitaleV1DocumentPrediction>
    {
    }
}
