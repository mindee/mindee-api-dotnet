using System.Collections.Generic;
using System.Text.Json;
using Mindee.Exceptions;
using Mindee.V2.ClientOptions;

namespace Mindee.V2.Product.Extraction.RagDocuments.Params
{
    /// <summary>
    /// Annotation parameters for RAG documents.
    /// </summary>
    public class RagDocumentAnnotationParameters : BaseAnnotationParameters
    {
        /// <summary>
        /// New public status to apply to the document (for example, to deactivate it).
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Field-level RAG annotation and guidelines configuration for the document.
        /// </summary>
        public RagAnnotation Annotation { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="documentId"><see cref="BaseAnnotationParameters.DocumentId"/></param>
        /// <param name="status"><see cref="Status"/></param>
        /// <param name="annotation"><see cref="Annotation"/></param>
        public RagDocumentAnnotationParameters(
            string documentId
            , string status = null
            , object annotation = null) : base(documentId)
        {
            Status = status;
            Annotation = annotation switch
            {
                RagAnnotation ragAnnotation => ragAnnotation,
                string jsonString => JsonSerializer.Deserialize<RagAnnotation>(jsonString),
                null => null,
                _ => throw new MindeeInputException("Invalid RAG Annotation format.")
            };
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Dictionary<string, object> GetRequestParameters()
        {
            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(Status))
                parameters.Add("status", Status);

            if (Annotation != null)
                parameters.Add("annotation", Annotation);

            return parameters;
        }
    }
}
