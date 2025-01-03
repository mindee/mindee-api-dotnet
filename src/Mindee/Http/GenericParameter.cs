using Mindee.Exceptions;
using Mindee.Input;

namespace Mindee.Http
{
    /// <summary>
    /// G
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
        /// <remarks>It is not available on all API.</remarks>
        public bool FullText { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="localSource"></param>
        /// <param name="urlSource"></param>
        /// <param name="fullText"></param>
        /// <exception cref="MindeeException"></exception>
        public GenericParameter(LocalInputSource localSource, UrlInputSource urlSource, bool fullText)
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
        }
    }
}
