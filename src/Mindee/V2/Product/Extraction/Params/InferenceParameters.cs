using System.Collections.Generic;
using Mindee.V1;
using Mindee.V2.ClientOptions;
using Mindee.V2.Http;

namespace Mindee.V2.Product.Extraction.Params
{
    /// <summary>
    ///     ResultOptions to pass when calling methods using the predict API V2.
    /// </summary>
    public class InferenceParameters : BaseParameters
    {
        /// <summary>
        ///     Inference parameters to set when sending a file.
        /// </summary>
        /// <param name="modelId">
        ///     <see cref="BaseParameters.ModelId" />
        /// </param>
        /// <param name="alias">
        ///     <see cref="BaseParameters.Alias" />
        /// </param>
        /// <param name="webhookIds">
        ///     <see cref="BaseParameters.WebhookIds" />
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
        /// <param name="textContext">
        ///     <see cref="BaseParameters.TextContext" />
        /// </param>
        /// <param name="pollingOptions">
        ///     <see cref="PollingOptions" />
        /// </param>
        /// <param name="dataSchema">
        ///     <see cref="DataSchema" />
        /// </param>
        public InferenceParameters(
            string modelId,
            string alias = null,
            List<string> webhookIds = null,
            bool? rag = null,
            bool? rawText = null,
            bool? polygon = null,
            bool? confidence = null,
            string textContext = null,
            AsyncPollingOptions pollingOptions = null,
            object dataSchema = null
        ) : base(
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
            PollingOptions = pollingOptions;
        }

        /// <summary>
        ///     Options for polling. Set only if having timeout issues.
        /// </summary>
        public AsyncPollingOptions PollingOptions { get; set; }
    }
}
