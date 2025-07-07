using Mindee.Exceptions;
using Mindee.Input;

namespace Mindee.Http
{
    /// <summary>
    /// Generic prediction parameters.
    /// </summary>
    public class GenericParameter
    {
        /// <summary>
        /// A local input source.
        /// </summary>
        public LocalInputSource LocalSource { get; }

        /// <summary>
        /// A URL input source.
        /// </summary>
        public UrlInputSource UrlSource { get; }

        /// <summary>
        /// Whether to include the full text data for async APIs.
        /// This performs a full OCR operation on the server and will increase response time and payload size.
        /// </summary>
        /// <remarks>Not available on all APIs.</remarks>
        public bool FullText { get; }

        /// <summary>
        /// If set, will enqueue to a workflow queue instead of a product's endpoint.
        /// </summary>
        public string WorkflowId { get; }

        /// <summary>
        /// If set, will enable Retrieval-Augmented Generation (only works if a valid WorkflowId is set).
        /// </summary>
        public bool Rag { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="localSource"></param>
        /// <param name="urlSource"></param>
        /// <param name="fullText"></param>
        /// <param name="workflowId"></param>
        /// <param name="rag"></param>
        /// <exception cref="MindeeException"></exception>
        public GenericParameter(
            LocalInputSource localSource,
            UrlInputSource urlSource,
            bool fullText,
            string workflowId,
            bool rag
        )
        {
            if (localSource != null && urlSource != null)
            {
                throw new MindeeException("localSource and urlSource may not both be specified.");
            }

            if (localSource == null && urlSource == null)
            {
                throw new MindeeException("One of localSource or urlSource must be specified.");
            }

            LocalSource = localSource;
            UrlSource = urlSource;
            FullText = fullText;
            WorkflowId = workflowId;
            Rag = rag;
        }
    }
}
