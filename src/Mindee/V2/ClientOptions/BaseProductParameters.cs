using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindee.V2.ClientOptions
{
    /// <summary>
    ///     Base parameters for sending a file to a Mindee V2 product.
    /// </summary>
    public abstract class BaseProductParameters
    {
        /// <summary>
        ///     Model ID to use for the inference. Required.
        /// </summary>
        public string ModelId { get; }

        /// <summary>
        ///     Optional: a free-form string to tag the request with your own identifier.
        ///     For example, an internal document ID, reference number, or database key.
        ///     If set, it will be included in the job and result responses.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        ///     Webhook IDs to call after all processing is finished.
        ///     If empty, no webhooks will be used.
        /// </summary>
        public List<string> WebhookIds { get; }

        /// <summary>
        /// Base constructor.
        /// </summary>
        /// <param name="modelId"><see cref="ModelId" /></param>
        /// <param name="alias"> <see cref="Alias" /></param>
        /// <param name="webhookIds"> <see cref="WebhookIds" /></param>
        protected BaseProductParameters(
            string modelId,
            string alias,
            List<string> webhookIds
        )
        {
            if (string.IsNullOrWhiteSpace(modelId))
                throw new ArgumentException("ModelId cannot be null or whitespace.", nameof(modelId));
            if (alias == "")
                throw new ArgumentException("Alias cannot be an empty string.", nameof(alias));
            if (webhookIds != null && webhookIds.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("WebhookIds cannot contain null, empty, or whitespace values.", nameof(webhookIds));

            ModelId = modelId.Trim();
            Alias = alias;
            WebhookIds = webhookIds;
        }

        /// <summary>
        /// Gets the request parameters for the enqueue request.
        /// </summary>
        public virtual Dictionary<string, string> GetRequestParameters()
        {
            var parameters = new Dictionary<string, string>
            {
                { "model_id", ModelId }
            };

            if (Alias != null)
                parameters.Add("alias", Alias);

            if (WebhookIds is { Count: > 0 })
                parameters.Add("webhook_ids", string.Join(",", WebhookIds));

            return parameters;
        }
    }
}
