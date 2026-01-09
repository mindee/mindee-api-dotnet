namespace Mindee.Pdf
{
    /// <summary>
    ///     The split pdf.
    /// </summary>
    public sealed class SplitPdf
    {
        /// <summary>
        /// </summary>
        /// <param name="file">
        ///     <see cref="File" />
        /// </param>
        /// <param name="totalPageNumber">
        ///     <see cref="TotalPageNumber" />
        /// </param>
        public SplitPdf(byte[] file, ushort totalPageNumber)
        {
            File = file;
            TotalPageNumber = totalPageNumber;
        }

        /// <summary>
        ///     The file.
        /// </summary>
        public byte[] File { get; }

        /// <summary>
        ///     The total page number.
        /// </summary>
        public ushort TotalPageNumber { get; }
    }
}
