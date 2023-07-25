namespace Mindee.Http
{
    /// <summary>
    /// Parameter required to use the predict feature.
    /// </summary>
    public sealed class PredictParameter
    {
        /// <summary>
        /// The file.
        /// </summary>
        public byte[] File { get; }

        /// <summary>
        /// The name of the file.
        /// </summary>
        public string Filename { get; }

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
        ///
        /// </summary>
        /// <param name="file"><see cref="File"/></param>
        /// <param name="filename"><see cref="Filename"/></param>
        /// <param name="allWords"><see cref="AllWords"/></param>
        /// <param name="cropper"><see cref="Cropper"/></param>
        public PredictParameter(
            byte[] file,
            string filename,
            bool allWords,
            bool cropper)
        {
            File = file;
            Filename = filename;
            AllWords = allWords;
            Cropper = cropper;
        }
    }
}
