using Mindee.Exceptions;
using Mindee.Input;

namespace Mindee.Http
{
    /// <summary>
    /// Parameter required to use the predict feature.
    /// </summary>
    public sealed class PredictParameter
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
        /// Want an OCR result ?
        /// </summary>
        /// <remarks>It is not available on all API.</remarks>
        public bool AllWords { get; }

        /// <summary>
        /// Whether to include the full text data for async APIs.
        /// This performs a full OCR operation on the server and will increase response time and payload size.
        /// </summary>
        /// <remarks>It is not available on all API.</remarks>
        public bool FullText { get; }

        /// <summary>
        /// Want the cropping result about the document?
        /// </summary>
        /// <remarks>It is not available in API builder.</remarks>
        public bool Cropper { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="localSource"><see cref="LocalSource"/></param>
        /// <param name="urlSource"><see cref="UrlSource"/></param>
        /// <param name="allWords"><see cref="AllWords"/></param>
        /// <param name="fullText"><see cref="FullText"/></param>
        /// <param name="cropper"><see cref="Cropper"/></param>
        public PredictParameter(
            LocalInputSource localSource,
            UrlInputSource urlSource,
            bool allWords,
            bool fullText,
            bool cropper)
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
            AllWords = allWords;
            FullText = fullText;
            Cropper = cropper;
        }
    }
}
