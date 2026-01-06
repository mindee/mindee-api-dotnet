using System.Collections.Generic;
using System.Text.Json;
using Mindee.Exceptions;
using Mindee.Input;
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
        public List<string> WebhookIds { get; }

        /// <summary>
        /// Additional text context used by the model during inference. Not recommended, for specific use only.
        /// </summary>
        public string TextContext { get; }

        /// <summary>
        /// Dynamic changes to the data schema of the model for this inference.
        /// Not recommended, for specific use only.
        /// </summary>
        public DataSchema DataSchema { get; }

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
        /// <param name="textContext"><see cref="TextContext"/></param>
        /// <param name="dataSchema"><see cref="DataSchema"/></param>
        public InferenceOptions(
            string modelId,
            bool? rag,
            bool? rawText,
            bool? polygon,
            bool? confidence,
            string alias,
            List<string> webhookIds,
            string textContext,
            object dataSchema
            )
        {
            ModelId = modelId;
            Alias = alias;
            Rag = rag;
            RawText = rawText;
            Polygon = polygon;
            Confidence = confidence;
            WebhookIds = webhookIds;
            TextContext = textContext;
            DataSchema = dataSchema switch
            {
                DataSchema dataSchemaClass => dataSchemaClass,
                JsonElement element => element.ValueKind switch
                {
                    JsonValueKind.Object => new DataSchema(JsonSerializer.Deserialize<Dictionary<string, object>>(element.GetRawText())),
                    JsonValueKind.String => new DataSchema(element.GetString()),
                    _ => throw new MindeeInputException("Invalid Data Schema format.")
                },
                Dictionary<string, object> dataSchemaDict => new DataSchema(dataSchemaDict),
                string dataSchemaStr => new DataSchema(dataSchemaStr),
                null => null,
                _ => throw new MindeeInputException("Invalid Data Schema format.")
            };
        }
    }
}
