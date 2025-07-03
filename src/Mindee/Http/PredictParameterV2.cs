using System.Collections.Generic;
using Mindee.Input;

namespace Mindee.Http
{
    /// <summary>
    /// Parameter required to use the predict feature on the API V2.
    /// </summary>
    public sealed class PredictParameterV2
    {
        /// <summary>
        /// Optional alias for the file.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// ID of the model.
        /// </summary>
        public string ModelId { get; }

        /// <summary>
        /// If true, activate Retrieval-Augmented Generation.
        /// </summary>
        public bool Rag { get; }

        /// <summary>
        /// IDs of webhooks to propagate the API response to.
        /// </summary>
        /// <remarks>Not available on all APIs.</remarks>
        public List<string> WebhookIds { get; }

        /// <summary>
        /// A local input source.
        /// </summary>
        public LocalInputSource LocalSource { get; }


        /// <summary>
        /// Result parameters for requests.
        /// </summary>
        /// <param name="localSource">Local input source containing the file.<see cref="GenericParameter.LocalSource"/></param>
        /// <param name="modelId">ID of the model<see cref="ModelId"/></param>
        /// <param name="rag">If set, will enqueue to the workflows queue.<see cref="GenericParameter.Rag"/></param>
        /// <param name="alias">Optional alias for the filename.<see cref="Alias"/></param>
        /// <param name="webhookIds">List of webhook IDs to propagate the response to.<see cref="WebhookIds"/></param>
        public PredictParameterV2(
            LocalInputSource localSource,
            string modelId,
            bool rag,
            string alias,
            List<string> webhookIds)
        {
            ModelId = modelId;
            Alias = alias;
            Rag = rag;
            LocalSource = localSource;
            WebhookIds = webhookIds ?? [];
        }
    }
}
