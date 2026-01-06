using System;
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
        /// A remote input source.
        /// </summary>
        public UrlInputSource UrlSource { get; }

        /// <summary>
        /// Result parameters for requests.
        /// </summary>
        /// <param name="localSource">Local input source containing the file.<see cref="LocalSource"/></param>
        /// <param name="urlSource">Remote input source containing the file.<see cref="UrlInputSource"/></param>
        /// <param name="modelId"><see cref="InferenceOptions.ModelId"/></param>
        /// <param name="rag"><see cref="InferenceOptions.Rag"/></param>
        /// <param name="rawText"><see cref="InferenceOptions.RawText"/></param>
        /// <param name="polygon"><see cref="InferenceOptions.Polygon"/></param>
        /// <param name="confidence"><see cref="InferenceOptions.Confidence"/></param>
        /// <param name="alias"><see cref="InferenceOptions.Alias"/></param>
        /// <param name="webhookIds"><see cref="InferenceOptions.WebhookIds"/></param>
        /// <param name="textContext"><see cref="InferenceOptions.TextContext"/></param>
        /// <param name="dataSchema"><see cref="InferenceOptions.DataSchema"/></param>
        public InferencePostParameters(
            string modelId,
            string alias,
            List<string> webhookIds,
            bool? rag = null,
            bool? rawText = null,
            bool? polygon = null,
            bool? confidence = null,
            string textContext = null,
            LocalInputSource localSource = null,
            UrlInputSource urlSource = null,
            DataSchema dataSchema = null
            )
        : base(
            modelId: modelId,
            rag: rag,
            rawText: rawText,
            polygon: polygon,
            confidence: confidence,
            alias: alias,
            webhookIds: webhookIds,
            textContext: textContext,
            dataSchema: dataSchema)
        {
            if (localSource == null && urlSource == null)
            {
                throw new ArgumentException("Either localSource or urlSource must be specified.");
            }

            if (localSource != null && urlSource != null)
            {
                throw new ArgumentException("localSource and urlSource may not both be specified.");
            }
            LocalSource = localSource;
            UrlSource = urlSource;
        }
    }
}
