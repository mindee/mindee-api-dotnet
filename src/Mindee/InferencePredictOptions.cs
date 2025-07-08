using System;
using System.Collections.Generic;
using System.Linq;
using Mindee.Input;

namespace Mindee
{
    /// <summary>
    /// Options to pass when calling methods using the predict API V2.
    /// </summary>
    public class InferencePredictOptions
    {
        /// <summary>
        /// ID of the model.
        /// </summary>
        public string ModelId { get; }

        /// <summary>
        /// Whether to include the full text data for async APIs.
        /// This performs a full OCR operation on the server and will increase response time and payload size.
        /// </summary>
        public bool FullText { get; }

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
        /// Options to pass when calling methods using the predict API.
        /// </summary>
        /// <param name="modelId"><see cref="ModelId"/></param>
        /// <param name="fullText"><see cref="FullText"/></param>
        /// <param name="alias"><see cref="Alias"/></param>
        /// <param name="webhookIds"><see cref="WebhookIds"/></param>
        /// <param name="rag"><see cref="Rag"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <param name="pollingOptions"><see cref="PollingOptions"/></param>
        private InferencePredictOptions(
            string modelId,
            bool fullText = false,
            string alias = null,
            List<string> webhookIds = null,
            bool rag = false,
            PageOptions pageOptions = null,
            AsyncPollingOptions pollingOptions = null
        )
        {
            ModelId = modelId;
            FullText = fullText;
            Alias = alias;
            WebhookIds = webhookIds ?? [];
            Rag = rag;
            PageOptions = pageOptions;
            PollingOptions = pollingOptions;
        }


        /// <summary>
        /// Builder entry point.
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public static Builder Create(string modelId) => new Builder(modelId);

        /// <summary>
        /// Returns the builder for an instance.
        /// </summary>
        /// <returns></returns>
        public Builder ToBuilder() => new Builder(ModelId);

        /// <summary>
        /// Builder class for InferencePredictOptions.
        /// </summary>
        public sealed class Builder
        {
            private readonly string _modelId;
            private bool _fullText;
            private string _alias;
            private List<string> _webhookIds;
            private bool _rag;
            private PageOptions _pageOptions;
            private AsyncPollingOptions _pollingOptions;

            internal Builder(string modelId)
            {
                if (string.IsNullOrWhiteSpace(modelId))
                    throw new ArgumentNullException(nameof(modelId), "Model ID cannot be null or empty.");
                _modelId = modelId;
            }

            /// <summary>
            /// Toggle FullText.
            /// </summary>
            /// <param name="fullText"></param>
            /// <returns></returns>
            public Builder WithFullText(bool fullText)
            {
                _fullText = fullText;
                return this;
            }

            /// <summary>
            /// Set an alias for the file.
            /// </summary>
            /// <param name="alias"></param>
            /// <returns></returns>
            public Builder WithAlias(string alias)
            {
                _alias = alias;
                return this;
            }

            /// <summary>
            /// Toggle Retrieval-Augmented Generation.
            /// </summary>
            /// <param name="rag"></param>
            /// <returns></returns>
            public Builder WithRag(bool rag)
            {
                _rag = rag;
                return this;
            }

            /// <summary>
            /// Set webhook IDs ist.
            /// </summary>
            /// <param name="webhookIds"></param>
            /// <returns></returns>
            public Builder WithWebhookIds(IEnumerable<string> webhookIds)
            {
                _webhookIds = webhookIds.ToList();
                return this;
            }

            /// <summary>
            /// Add a single webhook ID.
            /// </summary>
            /// <param name="webhookId"></param>
            /// <returns></returns>
            public Builder AddWebhookId(string webhookId)
            {
                if (_webhookIds == null)
                {
                    _webhookIds = new List<string>();
                }

                _webhookIds.Add(webhookId);
                return this;
            }

            /// <summary>
            /// Set page options.
            /// </summary>
            /// <param name="pageOptions"></param>
            /// <returns></returns>
            public Builder WithPageOptions(PageOptions pageOptions)
            {
                _pageOptions = pageOptions;
                return this;
            }

            /// <summary>
            /// Set the polling options.
            /// </summary>
            /// <param name="pollingOptions"></param>
            /// <returns></returns>
            public Builder WithPollingOptions(AsyncPollingOptions pollingOptions)
            {
                _pollingOptions = pollingOptions;
                return this;
            }

            /// <summary>
            /// Build instance.
            /// </summary>
            /// <returns></returns>
            public InferencePredictOptions Build() =>
                new(
                    _modelId,
                    _fullText,
                    _alias,
                    _webhookIds,
                    _rag,
                    _pageOptions,
                    _pollingOptions
                );
        }
    }
}
