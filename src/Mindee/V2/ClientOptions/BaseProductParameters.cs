using System.Collections.Generic;

namespace Mindee.V2.ClientOptions
{
    /// <summary>
    ///     Base parameters for enqueueing a document.
    /// </summary>
    public abstract class BaseProductParameters
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
            ModelId = modelId;
            Alias = alias;
            WebhookIds = webhookIds;
        }

        /// <summary>
        /// Gets the request parameters for the enqueue request.
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
