using System;
using System.Collections.Generic;
using Mindee.Input;
using Mindee.V2.ClientOptions;
using Mindee.V2.Product.Extraction.Params;

namespace Mindee.V2.Http
{
    /// <summary>
    ///     Parameter required to use the predict feature on the API V2.
    /// </summary>
    public sealed class InferencePostParameters : BaseParameters
    {
        /// <summary>
        ///     Result parameters for requests.
        /// </summary>
        /// <param name="localSource">Local input source containing the file.<see cref="LocalSource" /></param>
        /// <param name="urlSource">Remote input source containing the file.<see cref="UrlInputSource" /></param>
        /// <param name="modelId">
        ///     <see cref="BaseParameters.ModelId" />
        /// </param>
        /// <param name="rag">
        ///     <see cref="BaseParameters.Rag" />
        /// </param>
        /// <param name="rawText">
        ///     <see cref="BaseParameters.RawText" />
        /// </param>
        /// <param name="polygon">
        ///     <see cref="BaseParameters.Polygon" />
        /// </param>
        /// <param name="confidence">
        ///     <see cref="BaseParameters.Confidence" />
        /// </param>
        /// <param name="alias">
        ///     <see cref="BaseParameters.Alias" />
        /// </param>
        /// <param name="webhookIds">
        ///     <see cref="BaseParameters.WebhookIds" />
        /// </param>
        /// <param name="textContext">
        ///     <see cref="BaseParameters.TextContext" />
        /// </param>
        /// <param name="dataSchema">
        ///     <see cref="BaseParameters.DataSchema" />
        /// </param>
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
                modelId,
                rag,
                rawText,
                polygon,
                confidence,
                alias,
                webhookIds,
                textContext,
                dataSchema)
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

        /// <summary>
        ///     A local input source.
        /// </summary>
        public LocalInputSource LocalSource { get; }

        /// <summary>
        ///     A remote input source.
        /// </summary>
        public UrlInputSource UrlSource { get; }
    }
}
