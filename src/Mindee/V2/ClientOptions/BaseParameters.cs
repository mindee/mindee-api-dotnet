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

        /// <summary>
        /// Gets the request parameters for the POST enqueue request.
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetRequestParameters()
        {
            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(Alias))
                parameters.Add("alias", Alias);

            if (WebhookIds is { Count: > 0 })
                parameters.Add("webhook_ids", string.Join(",", WebhookIds));

            return parameters;
        }
    }
}
