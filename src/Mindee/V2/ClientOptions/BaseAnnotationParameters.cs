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
            this.DocumentId = documentId;
        }

        /// <summary>
        /// Gets the request parameters for the upload request.
        /// </summary>
        public abstract Dictionary<string, object> GetRequestParameters();
    }
}
