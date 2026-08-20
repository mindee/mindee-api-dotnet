using System.Collections.Generic;
using Mindee.V2.ClientOptions;

namespace Mindee.V2.Product.Crop.Params
{
    /// <summary>
    /// Parameters for sending a file to a Crop product.
    /// </summary>
    [ProductAttributes("crop")]
    public class CropParameters : BaseProductParameters
    {
        /// <summary>
        /// Crop parameters to set when sending a file.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="alias"></param>
        /// <param name="webhookIds"></param>
        public CropParameters(
            string modelId,
            string alias = null,
            List<string> webhookIds = null) : base(modelId, alias, webhookIds) { }
    }
}
