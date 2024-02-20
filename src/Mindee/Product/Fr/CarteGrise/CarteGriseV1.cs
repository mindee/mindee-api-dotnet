using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.CarteGrise
{
    /// <summary>
    /// The definition for Carte Grise, API version 1.
    /// </summary>
    [Endpoint("carte_grise", "1")]
    public sealed class CarteGriseV1 : Inference<CarteGriseV1Document, CarteGriseV1Document>
    {
    }
}
