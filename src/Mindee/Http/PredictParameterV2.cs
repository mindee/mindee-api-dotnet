using System.Collections.Generic;
using Mindee.Input;
using Mindee.Parsing.Common;

namespace Mindee.Http
{
    /// <summary>
    /// Parameter required to use the predict feature on the API V2.
    /// </summary>
    public sealed class PredictParameterV2 : PredictParameter
    {
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
        /// Result parameters for requests.
        /// </summary>
        /// <param name="localSource">Local input source containing the file.<see cref="GenericParameter.LocalSource"/></param>
        /// <param name="urlSource">Source URL to use.<see cref="GenericParameter.UrlSource"/></param>
        /// <param name="fullText">Whether to include the full text in the payload (compatible APIs only)<see cref="GenericParameter.FullText"/></param>
        /// <param name="cropper">Whether to crop the document before enqueuing on the API.<see cref="Cropper"/></param>
        /// <param name="rag">If set, will enqueue to the workflows queue.<see cref="GenericParameter.Rag"/></param>
        /// <param name="alias">Optional alias for the filename.<see cref="Alias"/></param>
        /// <param name="webhookIds">List of webhook IDs to propagate the response to.<see cref="WebhookIds"/></param>
        public PredictParameterV2(
            LocalInputSource localSource,
            UrlInputSource urlSource,
            bool fullText,
            bool cropper,
            bool rag,
            string alias,
            List<string> webhookIds) : base(localSource, urlSource, false, fullText, cropper, null, rag)
        {
            Alias = alias;
            WebhookIds = webhookIds;
        }
    }
}
