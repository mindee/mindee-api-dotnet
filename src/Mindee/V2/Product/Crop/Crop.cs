using System;
using Mindee.V2.Product.Crop;

namespace Mindee.V2.Product.Crop
{
    /// <summary>
    /// Send a file to the asynchronous processing queue for a crop utility inference.
    /// </summary>
    public class Crop : BaseProduct
    {
        /// <summary>
        /// Type of the product's response.
        /// </summary>
        public static new Type ResponseType => typeof(CropResponse);
    }
}
