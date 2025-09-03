using System;
using System.Collections.Generic;
using Mindee.Parsing.V2;
using Mindee.Parsing.V2.Field;

namespace Mindee.Http
{
    /// <summary>
    /// Parameters for running an inference.
    /// </summary>
    public class InferenceOptions
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
        /// Enhance extraction accuracy with Retrieval-Augmented Generation.
        /// </summary>
        public bool? Rag { get; }

        /// <summary>
        /// Extract the full text content from the document as strings, and fill the
        /// <see cref="InferenceResult.RawText"/> attribute.
        /// </summary>
        public bool? RawText { get; }

        /// <summary>
        /// Calculate bounding box polygons for all fields, and fill their <see cref="BaseField.Locations"/> attribute
        /// </summary>
        public bool? Polygon { get; }

        /// <summary>
        /// Boost the precision and accuracy of all extractions.
        /// Calculate confidence scores for all fields, and fill their <see cref="BaseField.Confidence"/> attribute.
        /// </summary>
        public bool? Confidence { get; }

        /// <summary>
        /// IDs of webhooks to propagate the API response to.
        /// </summary>
        /// <remarks>Not available on all APIs.</remarks>
        public List<string> WebhookIds { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="modelId">ID of the model<see cref="ModelId"/></param>
        /// <param name="rag"><see cref="Rag"/></param>
        /// <param name="alias"><see cref="Alias"/></param>
        /// <param name="rawText"><see cref="RawText"/></param>
        /// <param name="polygon"><see cref="Polygon"/></param>
        /// <param name="confidence"><see cref="Confidence"/></param>
        /// <param name="webhookIds"><see cref="WebhookIds"/></param>
        public InferenceOptions(
            string modelId,
            bool? rag,
            bool? rawText,
            bool? polygon,
            bool? confidence,
            string alias,
            List<string> webhookIds)
        {
            ModelId = modelId;
            Alias = alias;
            Rag = rag;
            RawText = rawText;
            Polygon = polygon;
            Confidence = confidence;
            WebhookIds = webhookIds;
        }
    }
}
