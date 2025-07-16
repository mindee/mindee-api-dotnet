using System.Collections.Generic;
using Mindee.Input;

namespace Mindee
{
    /// <summary>
    /// ResultOptions to pass when calling methods using the predict API V2.
    /// </summary>
    public class InferenceParameters
    {
        /// <summary>
        /// ID of the model.
        /// </summary>
        public string ModelId { get; }

        /// <summary>
        /// If set, will enable Retrieval-Augmented Generation.
        /// </summary>
        public bool Rag { get; }

        /// <summary>
        /// Optional alias for the file.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// IDs of webhooks to propagate the API response to.
        /// </summary>
        public List<string> WebhookIds { get; }

        /// <summary>
        /// Page options.
        /// </summary>
        public PageOptions PageOptions { get; set; }

        /// <summary>
        /// Polling options.
        /// </summary>
        public AsyncPollingOptions PollingOptions { get; set; }


        /// <summary>
        /// ResultOptions to pass when calling methods using the predict API.
        /// </summary>
        /// <param name="modelId"><see cref="ModelId"/></param>
        /// <param name="alias"><see cref="Alias"/></param>
        /// <param name="webhookIds"><see cref="WebhookIds"/></param>
        /// <param name="rag"><see cref="Rag"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <param name="pollingOptions"><see cref="PollingOptions"/></param>
        public InferenceParameters(
            string modelId,
            string alias = null,
            List<string> webhookIds = null,
            bool rag = false,
            PageOptions pageOptions = null,
            AsyncPollingOptions pollingOptions = null
        )
        {
            ModelId = modelId;
            Alias = alias;
            WebhookIds = webhookIds ?? [];
            Rag = rag;
            PageOptions = pageOptions;
            PollingOptions = pollingOptions;
        }
    }
}
