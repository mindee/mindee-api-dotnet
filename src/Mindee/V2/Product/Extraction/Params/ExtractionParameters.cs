using System.Collections.Generic;
using System.Text.Json;
using Mindee.Exceptions;
using Mindee.V2.ClientOptions;
using Mindee.V2.Parsing.Field;

namespace Mindee.V2.Product.Extraction.Params
{
    /// <summary>
    ///     ResultOptions to pass when calling methods using the predict API V2.
    /// </summary>
    public class ExtractionParameters : BaseParameters
    {
        /// <summary>
        ///     Enhance extraction accuracy with Retrieval-Augmented Generation.
        /// </summary>
        public bool? Rag { get; }

        /// <summary>
        ///     Extract the full text content from the document as strings, and fill the
        ///     <see cref="Parsing.RawText" /> attribute.
        /// </summary>
        public bool? RawText { get; }

        /// <summary>
        ///     Calculate bounding box polygons for all fields, and fill their <see cref="BaseField.Locations" /> attribute
        /// </summary>
        public bool? Polygon { get; }

        /// <summary>
        ///     Boost the precision and accuracy of all extractions.
        ///     Calculate confidence scores for all fields, and fill their <see cref="BaseField.Confidence" /> attribute.
        /// </summary>
        public bool? Confidence { get; }

        /// <summary>
        ///     Additional text context used by the model during inference. Not recommended, for specific use only.
        /// </summary>
        public string TextContext { get; }

        /// <summary>
        ///     Dynamic changes to the data schema of the model for this inference.
        ///     Not recommended, for specific use only.
        /// </summary>
        public DataSchema DataSchema { get; }

        /// <summary>
        /// Slug for the extraction product.
        /// </summary>
        public sealed override string Slug { get; protected set; }

        /// <summary>
        ///     Extraction parameters to set when sending a file.
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
        ///     <see cref="V1.Parsing.Common.Rag" />
        /// </param>
        /// <param name="rawText">
        ///     <see cref="Parsing.RawText" />
        /// </param>
        /// <param name="polygon">
        ///     <see cref="Geometry.Polygon" />
        /// </param>
        /// <param name="confidence">
        ///     <see cref="Confidence" />
        /// </param>
        /// <param name="textContext">
        ///     <see cref="TextContext" />
        /// </param>
        /// <param name="pollingOptions">
        ///     <see cref="PollingOptions" />
        /// </param>
        /// <param name="dataSchema">
        ///     <see cref="DataSchema" />
        /// </param>
        public ExtractionParameters(
            string modelId,
            string alias = null,
            List<string> webhookIds = null,
            bool? rag = null,
            bool? rawText = null,
            bool? polygon = null,
            bool? confidence = null,
            string textContext = null,
            PollingOptions pollingOptions = null,
            object dataSchema = null
        ) : base(
            modelId,
            alias,
            webhookIds,
            pollingOptions)
        {
            Rag = rag;
            RawText = rawText;
            Polygon = polygon;
            Confidence = confidence;
            TextContext = textContext;
            DataSchema = dataSchema switch
            {
                DataSchema dataSchemaClass => dataSchemaClass,
                JsonElement element => element.ValueKind switch
                {
                    JsonValueKind.Object => new DataSchema(
                        JsonSerializer.Deserialize<Dictionary<string, object>>(element.GetRawText())),
                    JsonValueKind.String => new DataSchema(element.GetString()),
                    _ => throw new MindeeInputException("Invalid Data Schema format.")
                },
                Dictionary<string, object> dataSchemaDict => new DataSchema(dataSchemaDict),
                string dataSchemaStr => new DataSchema(dataSchemaStr),
                null => null,
                _ => throw new MindeeInputException("Invalid Data Schema format.")
            };
            Slug = "extraction";
        }
    }
}
