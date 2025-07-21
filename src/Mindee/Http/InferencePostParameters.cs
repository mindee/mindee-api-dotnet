using System.Collections.Generic;
using Mindee.Input;

namespace Mindee.Http
{
    /// <summary>
    /// Parameter required to use the predict feature on the API V2.
    /// </summary>
    public sealed class InferencePostParameters : InferenceOptions
    {
        /// <summary>
        /// A local input source.
        /// </summary>
        public LocalInputSource LocalSource { get; }

        /// <summary>
        /// Result parameters for requests.
        /// </summary>
        /// <param name="localSource">Local input source containing the file.<see cref="LocalSource"/></param>
        /// <param name="modelId"><see cref="InferenceOptions.ModelId"/></param>
        /// <param name="rag"><see cref="InferenceOptions.Rag"/></param>
        /// <param name="alias"><see cref="InferenceOptions.Alias"/></param>
        /// <param name="webhookIds"><see cref="InferenceOptions.WebhookIds"/></param>
        public InferencePostParameters(
            LocalInputSource localSource,
            string modelId,
            bool rag,
            string alias,
            List<string> webhookIds)
        : base(modelId, rag, alias, webhookIds)
        {
            LocalSource = localSource;
        }
    }
}
