namespace Mindee.Parsing
{
    /// <summary>
    /// Parameter required to use the predict feature.
    /// </summary>
    internal sealed class PredictParameter
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
        public bool WithFullText { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"><see cref="File"/></param>
        /// <param name="filename"><see cref="Filename"/></param>
        public PredictParameter(
            byte[] file,
            string filename)
            : this(file, filename, false)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"><see cref="File"/></param>
        /// <param name="filename"><see cref="Filename"/></param>
        /// <param name="withFullText"><see cref="WithFullText"/></param>
        public PredictParameter(
            byte[] file,
            string filename,
            bool withFullText)
        {
            File = file;
            Filename = filename;
            WithFullText = withFullText;
        }
    }
}
