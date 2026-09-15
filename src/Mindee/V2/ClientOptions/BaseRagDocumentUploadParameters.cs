using System;
using System.Collections.Generic;
using Mindee.V2.Parsing;

namespace Mindee.V2.ClientOptions
{
    /// <summary>
    /// Base parameters for document upload operations.
    /// </summary>
    public abstract class BaseRagDocumentUploadParameters<TAnnotationResponse>
        where TAnnotationResponse : BaseRagAnnotationResponse
    {
        /// <summary>
        /// UUID of the model that the uploaded RAG document is linked to.
        /// </summary>
        public string ModelId { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="modelId"><see cref="ModelId"/></param>
        protected BaseRagDocumentUploadParameters(string modelId)
        {
            if (string.IsNullOrWhiteSpace(modelId))
                throw new ArgumentException("ModelId cannot be null or whitespace.", nameof(modelId));
            ModelId = modelId.Trim();
        }

        /// <summary>
        /// Gets the request parameters for the upload request.
        /// </summary>
        public virtual Dictionary<string, string> GetRequestParameters()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("model_id", ModelId);
            return parameters;
        }
    }
}
