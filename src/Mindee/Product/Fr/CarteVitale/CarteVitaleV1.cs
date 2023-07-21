using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.CarteVitale
{
    /// <summary>
    /// The definition for Carte Vitale, API version 1.
    /// </summary>
    [Endpoint("carte_vitale", "1")]
    public sealed class CarteVitaleV1 : Inference<CarteVitaleV1Document, CarteVitaleV1Document>
    {
    }
}
