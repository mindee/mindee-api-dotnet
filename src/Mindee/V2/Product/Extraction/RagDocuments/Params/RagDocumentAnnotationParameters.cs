using System.Collections.Generic;

namespace Mindee.V2.Product.Extraction.RagDocuments.Params
{
    /// <summary>
    /// Upload parameters for RAG documents.
    /// </summary>
    public class RagDocumentAnnotationParameters
    {
        /// <summary>
        /// UUID of the extraction model that the uploaded RAG document is linked to.
        /// </summary>
        public string DocumentId { get; }

        /// <summary>
        /// New public status to apply to the document (for example, to deactivate it).
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Field-level RAG annotation and guidelines configuration for the document.
        /// </summary>
        public string Annotation { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="documentId"><see cref="DocumentId"/></param>
        /// <param name="status"><see cref="Status"/></param>
        /// <param name="annotation"><see cref="Annotation"/></param>
        public RagDocumentAnnotationParameters(
            string documentId, string status = null, string annotation = null)
        {
            DocumentId = documentId;
            Status = status;
            Annotation = annotation;
        }

        /// <summary>
        /// Gets the request parameters for the upload request.
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetRequestParameters()
        {
            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(DocumentId))
            {
                parameters.Add("document_id", DocumentId);
            }
            else
            {
                throw new System.ArgumentException("DocumentId is required in RagDocumentsAnnotationParameters");
            }

            return parameters;
        }
    }
}
