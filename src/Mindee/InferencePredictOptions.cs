using System.Collections.Generic;

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
        /// Options to pass when calling methods using the predict API.
        /// </summary>
        /// <param name="modelId"><see cref="ModelId"/></param>
        /// <param name="fullText"><see cref="FullText"/></param>
        /// <param name="alias"><see cref="Alias"/></param>
        /// <param name="webhookIds"><see cref="WebhookIds"/></param>
        /// <param name="rag"><see cref="Rag"/></param>
        public InferencePredictOptions(
            string modelId,
            bool fullText = false,
            string alias = null,
            List<string> webhookIds = null,
            bool rag = false
        )
        {
            ModelId = modelId;
            FullText = fullText;
            Alias = alias;
            WebhookIds = webhookIds ?? [];
            Rag = rag;
        }
    }
}
