using Mindee.Parsing.Common;

namespace Mindee.Parsing.Fr.IdCard
{
    /// <summary>
    /// The carte vitale v1 definition.
    /// </summary>
    [Endpoint("carte_vitale", "1")]
    public class CarteVitaleV1Inference : Inference<CarteVitaleV1DocumentPrediction, CarteVitaleV1DocumentPrediction>
    {
    }
}
