namespace Mindee
{
    /// <summary>
    /// Options to pass when calling methods using the predict API.
    /// </summary>
    public sealed class PredictOptions
    {
        /// <summary>
        /// Whether to include all the words on each page.
        /// This performs a full OCR operation on the server and will increase response time.
        /// </summary>
        /// <remarks>It is not available on all API.</remarks>
        public bool AllWords { get; }

        /// <summary>
        /// Whether to include cropper results for each page.
        /// This performs a cropping operation on the server and will increase response time.
        /// </summary>
        /// <remarks>It is not available in API builder.</remarks>
        public bool Cropper { get; }

        /// <summary>
        /// Options to pass when calling methods using the predict API.
        /// </summary>
        /// <param name="allWords"><see cref="AllWords"/></param>
        /// <param name="cropper"><see cref="Cropper"/></param>
        public PredictOptions(bool allWords = false, bool cropper = false)
        {
            AllWords = allWords;
            Cropper = cropper;
        }
    }
}
