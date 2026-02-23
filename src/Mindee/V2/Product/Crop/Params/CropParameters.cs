using System.Collections.Generic;
using Mindee.V2.ClientOptions;

namespace Mindee.V2.Product.Crop.Params
{
    /// <summary>
    /// Parameters accepted by the crop utility v2 endpoint.
    /// </summary>
    public class CropParameters : BaseParameters
    {
        /// <summary>
        /// Slug for the extraction product.
        /// </summary>
        public sealed override string Slug { get; protected set; }

        /// <summary>
        /// Crop parameters to set when sending a file.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="alias"></param>
        /// <param name="webhookIds"></param>
        /// <param name="pollingOptions"></param>
        public CropParameters(
            string modelId,
            string alias = null,
            List<string> webhookIds = null,
            PollingOptions pollingOptions = null) : base(modelId, alias, webhookIds, pollingOptions)
        {
            Slug = "crop";
        }
    }
}
