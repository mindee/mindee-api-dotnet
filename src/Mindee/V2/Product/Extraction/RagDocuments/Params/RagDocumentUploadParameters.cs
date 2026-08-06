using System.Collections.Generic;

namespace Mindee.V2.Product.Extraction.RagDocuments.Params
{
    /// <summary>
    /// Upload parameters for RAG documents.
    /// </summary>
    public class RagDocumentUploadParameters
    {
        /// <summary>
        /// UUID of the extraction model that the uploaded RAG document is linked to.
        /// </summary>
        public string ModelId { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="modelId"><see cref="ModelId"/></param>
        public RagDocumentUploadParameters(string modelId)
        {
            ModelId = modelId;
        }

        /// <summary>
        /// Gets the request parameters for the upload request.
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetRequestParameters()
        {
            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(ModelId))
            {
                parameters.Add("model_id", ModelId);
            }
            else
            {
                throw new System.ArgumentException("ModelId is required in RagDocumentsParameters");
            }

            return parameters;
        }
    }
}
