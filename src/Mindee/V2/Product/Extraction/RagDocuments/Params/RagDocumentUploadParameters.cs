using System;
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
            if (string.IsNullOrWhiteSpace(modelId))
                throw new ArgumentException("ModelId cannot be null or whitespace.", nameof(modelId));
            ModelId = modelId.Trim();
        }

        /// <summary>
        /// Gets the request parameters for the upload request.
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetRequestParameters()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("model_id", ModelId);
            return parameters;
        }
    }
}
