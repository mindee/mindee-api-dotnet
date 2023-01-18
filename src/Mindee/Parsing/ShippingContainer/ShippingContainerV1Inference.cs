﻿using Mindee.Parsing.Common;

namespace Mindee.Parsing.ShippingContainer
{
    /// <summary>
    /// The shipping containers v1 definition.
    /// </summary>
    [Endpoint("shipping_containers", "1")]
    public class ShippingContainerV1Inference : Inference<ShippingContainerV1DocumentPrediction, ShippingContainerV1DocumentPrediction>
    {
    }
}
