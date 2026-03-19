using System.Collections.Generic;
using Mindee.V2.ClientOptions;

namespace Mindee.V2.Product.Classification.Params
{
    /// <summary>
    ///   Parameters for a classification utility inference.
    /// </summary>
    [ProductAttributes("classification")]
    public class ClassificationParameters : BaseParameters
    {
        /// <summary>
        /// Classification parameters to set when sending a file.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="alias"></param>
        /// <param name="webhookIds"></param>
        public ClassificationParameters(
            string modelId,
            string alias = null,
            List<string> webhookIds = null) : base(modelId, alias, webhookIds) { }
    }
}
