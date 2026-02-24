using System.Collections.Generic;
using Mindee.V2.ClientOptions;

namespace Mindee.V2.Product.Classification.Params
{
    /// <summary>
    ///   Parameters for a classification utility inference.
    /// </summary>
    public class ClassificationParameters : BaseParameters
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
        public ClassificationParameters(
            string modelId,
            string alias = null,
            List<string> webhookIds = null,
            PollingOptions pollingOptions = null) : base(modelId, alias, webhookIds, pollingOptions)
        {
            Slug = "classification";
        }
    }
}
