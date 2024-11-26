using Mindee.Input;

namespace Mindee.Http
{
    /// <summary>
    /// Parameter required to use the predict feature.
    /// </summary>
    public sealed class PredictParameter : GenericParameter
    {
        /// <summary>
        /// Want an OCR result ?
        /// </summary>
        /// <remarks>It is not available on all API.</remarks>
        public bool AllWords { get; }

        /// <summary>
        /// Want the cropping result about the document?
        /// </summary>
        /// <remarks>It is not available in API builder.</remarks>
        public bool Cropper { get; }

        /// <summary>
        /// Prediction parameters for requests.
        /// </summary>
        /// <param name="localSource">Local input source containing the file.<see cref="GenericParameter.LocalSource"/></param>
        /// <param name="urlSource">Source URL to use.<see cref="GenericParameter.UrlSource"/></param>
        /// <param name="allWords">Whether to include the full OCR response in the payload (compatible APIs only).<see cref="AllWords"/></param>
        /// <param name="fullText">Whether to include the full text in the payload (compatible APIs only)<see cref="GenericParameter.FullText"/></param>
        /// <param name="cropper">Whether to crop the document before enqueuing on the API.<see cref="Cropper"/></param>
        public PredictParameter(
            LocalInputSource localSource,
            UrlInputSource urlSource,
            bool allWords,
            bool fullText,
            bool cropper) : base(localSource, urlSource, fullText)
        {
            AllWords = allWords;
            Cropper = cropper;
        }
    }
}
