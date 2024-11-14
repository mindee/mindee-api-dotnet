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
        /// Prediction parameters for requests.
        /// </summary>
        /// <param name="localSource">Local input source containing the file.<see cref="LocalSource"/></param>
        /// <param name="urlSource">Source URL to use.<see cref="UrlSource"/></param>
        /// <param name="allWords">Whether to include the full OCR response in the payload (compatible APIs only).<see cref="AllWords"/></param>
        /// <param name="fullText">Whether to include the full text in the payload (compatible APIs only)<see cref="FullText"/></param>
        /// <param name="cropper">Whether to crop the document before enqueuing on the API.<see cref="Cropper"/></param>
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
