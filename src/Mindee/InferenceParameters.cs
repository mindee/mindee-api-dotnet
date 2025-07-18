using System.Collections.Generic;

namespace Mindee
{
    /// <summary>
    /// ResultOptions to pass when calling methods using the predict API V2.
    /// </summary>
    public class InferenceParameters
    {
        /// <summary>
        /// ID of the model, required.
        /// </summary>
        public string ModelId { get; }

        /// <summary>
        /// If set to `true`, will enable Retrieval-Augmented Generation.
        /// </summary>
        public bool Rag { get; }

        /// <summary>
        /// Use an alias to link the file to your own DB. If empty, no alias will be used.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// IDs of webhooks to propagate the API response to.
        /// </summary>
        public List<string> WebhookIds { get; }

        /// <summary>
        /// Options for polling. Set only if having timeout issues.
        /// </summary>
        public AsyncPollingOptions PollingOptions { get; set; }

        /// <summary>
        /// Inference parameters to set when sending a file.
        /// </summary>
        /// <param name="modelId"><see cref="ModelId"/></param>
        /// <param name="alias"><see cref="Alias"/></param>
        /// <param name="webhookIds"><see cref="WebhookIds"/></param>
        /// <param name="rag"><see cref="Rag"/></param>
        /// <param name="pollingOptions"><see cref="PollingOptions"/></param>
        public InferenceParameters(
            string modelId,
            string alias = null,
            List<string> webhookIds = null,
            bool rag = false,
            AsyncPollingOptions pollingOptions = null
        )
        {
            ModelId = modelId;
            Alias = alias;
            WebhookIds = webhookIds ?? [];
            Rag = rag;
            PollingOptions = pollingOptions;
        }
    }
}
