using System.Collections.Generic;

namespace Mindee.V2.ClientOptions
{
    /// <summary>
    ///     Parameters for running an inference.
    /// </summary>
    public abstract class BaseParameters
    {
        /// <summary>
        ///     Optional alias for the file.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        ///     ID of the model.
        /// </summary>
        public string ModelId { get; }

        /// <summary>
        ///     IDs of webhooks to propagate the API response to.
        /// </summary>
        public List<string> WebhookIds { get; }

        /// <summary>
        ///     Options for polling. Set only if having timeout issues.
        /// </summary>
        public PollingOptions PollingOptions { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="modelId">ID of the model<see cref="ModelId" /></param>
        /// <param name="alias">
        /// <see cref="Alias" />
        /// </param>
        /// <param name="webhookIds">
        ///     <see cref="WebhookIds" />
        /// </param>
        /// <param name="pollingOptions">
        ///     <see cref="PollingOptions" />
        /// </param>
        protected BaseParameters(
            string modelId,
            string alias,
            List<string> webhookIds,
            PollingOptions pollingOptions
        )
        {
            ModelId = modelId;
            Alias = alias;
            WebhookIds = webhookIds;
            PollingOptions = pollingOptions;
        }
    }
}
