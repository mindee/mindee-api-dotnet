using System.Collections.Generic;
using Mindee.V2.ClientOptions;

namespace Mindee.V2.Product.Split.Params
{
    /// <summary>
    /// Parameters accepted by the split utility v2 endpoint.
    /// </summary>
    public class SplitParameters : BaseParameters
    {
        /// <summary>
        /// Slug for the split product.
        /// </summary>
        public sealed override string Slug { get; protected set; }

        /// <summary>
        /// Split parameters to set when sending a file.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="alias"></param>
        /// <param name="webhookIds"></param>
        /// <param name="pollingOptions"></param>
        public SplitParameters(
            string modelId,
            string alias = null,
            List<string> webhookIds = null,
            PollingOptions pollingOptions = null) : base(modelId, alias, webhookIds, pollingOptions)
        {
            Slug = "split";
        }
    }
}
