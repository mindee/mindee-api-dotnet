using System.Collections.Generic;

namespace Mindee
{
    /// <summary>
    /// Options to pass when calling methods using the predict API V2.
    /// </summary>
    public sealed class PredictOptionsV2
    {
        /// <summary>
        /// Whether to include the full text data for async APIs.
        /// This performs a full OCR operation on the server and will increase response time and payload size.
        /// </summary>
        public bool FullText { get; }

        /// <summary>
        /// Whether to include cropper results for each page.
        /// This performs a cropping operation on the server and will increase response time.
        /// </summary>
        /// <remarks>It is not available in API builder.</remarks>
        public bool Cropper { get; }

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
        /// <remarks>It is not available on all API.</remarks>
        public List<string> WebhookIds { get; }



        /// <summary>
        /// Options to pass when calling methods using the predict API.
        /// </summary>
        /// <param name="cropper"><see cref="Cropper"/></param>
        /// <param name="fullText"><see cref="FullText"/></param>
        /// <param name="alias"><see cref="Alias"/></param>
        /// <param name="webhookIds"><see cref="WebhookIds"/></param>
        /// <param name="rag"><see cref="Rag"/></param>
        public PredictOptionsV2(
            bool fullText = false,
            bool cropper = false,
            string alias = null,
            List<string> webhookIds = null,
            bool rag = false
        )
        {
            Cropper = cropper;
            FullText = fullText;
            Alias = alias;
            WebhookIds = webhookIds;
            Rag = rag;
        }
    }
}
