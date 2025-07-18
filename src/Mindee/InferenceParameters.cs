using System.Collections.Generic;
using Mindee.Http;

namespace Mindee
{
    /// <summary>
    /// ResultOptions to pass when calling methods using the predict API V2.
    /// </summary>
    public class InferenceParameters : InferenceOptions
    {
        /// <summary>
        /// Options for polling. Set only if having timeout issues.
        /// </summary>
        public AsyncPollingOptions PollingOptions { get; set; }

        /// <summary>
        /// Inference parameters to set when sending a file.
        /// </summary>
        /// <param name="modelId"><see cref="InferenceOptions.ModelId"/></param>
        /// <param name="rag"><see cref="InferenceOptions.Rag"/></param>
        /// <param name="alias"><see cref="InferenceOptions.Alias"/></param>
        /// <param name="webhookIds"><see cref="InferenceOptions.WebhookIds"/></param>
        /// <param name="pollingOptions"><see cref="PollingOptions"/></param>
        public InferenceParameters(
            string modelId,
            string alias = null,
            List<string> webhookIds = null,
            bool rag = false,
            AsyncPollingOptions pollingOptions = null
        ) : base(modelId, rag, alias, webhookIds)
        {
            PollingOptions = pollingOptions;
        }
    }
}
