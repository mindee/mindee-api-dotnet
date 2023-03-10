using Mindee.Parsing.Common;

namespace Mindee.Parsing.ShippingContainer
{
    /// <summary>
    /// The definition for shipping_containers v1.
    /// </summary>
    [Endpoint("shipping_containers", "1")]
    public class ShippingContainerV1Inference : Inference<ShippingContainerV1DocumentPrediction, ShippingContainerV1DocumentPrediction>
    {
    }
}
