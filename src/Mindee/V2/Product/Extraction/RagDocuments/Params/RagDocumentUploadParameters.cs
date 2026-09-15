using System;
using System.Collections.Generic;
using Mindee.V2.ClientOptions;

namespace Mindee.V2.Product.Extraction.RagDocuments.Params
{
    /// <summary>
    /// Upload parameters for RAG documents.
    /// </summary>
    public class RagDocumentUploadParameters : BaseRagDocumentUploadParameters<ExtractionRagAnnotationResponse>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="modelId"><inheritdoc/></param>
        public RagDocumentUploadParameters(string modelId) : base(modelId)
        { }
    }
}
