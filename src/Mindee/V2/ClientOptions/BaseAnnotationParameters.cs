using System;
using System.Collections.Generic;

namespace Mindee.V2.ClientOptions
{
    /// <summary>
    /// Base parameters for document annotations.
    /// </summary>
    public abstract class BaseAnnotationParameters
    {
        /// <summary>
        /// UUID of the annotated document.
        /// </summary>
        public string DocumentId { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="documentId"></param>
        protected BaseAnnotationParameters(string documentId)
        {
            if (string.IsNullOrWhiteSpace(documentId))
                throw new ArgumentException("DocumentId cannot be null or whitespace.", nameof(documentId));

            // Note: DocumentId is included in the request URL path, it is not a parameter.
            DocumentId = documentId.Trim();
        }

        /// <summary>
        /// Gets the request parameters for the upload request.
        /// </summary>
        public abstract Dictionary<string, object> GetRequestParameters();
    }
}
