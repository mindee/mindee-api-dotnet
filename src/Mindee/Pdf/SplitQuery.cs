using System.IO;

namespace Mindee.Pdf
{
    /// <summary>
    /// Represent parameter to split a pdf.
    /// </summary>
    public sealed class SplitQuery
    {
        /// <summary>
        /// The file.
        /// </summary>
        public Stream Stream { get; }
        
        /// <summary>
        /// The start number page.
        /// </summary>
        public ushort PageNumberStart { get; }
        
        /// <summary>
        /// The end number page.
        /// </summary>
        public ushort PageNumberEnd { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"><see cref="Stream"/></param>
        /// <param name="pageNumberStart"><see cref="PageNumberStart"/></param>
        /// <param name="pageNumberEnd"><see cref="PageNumberEnd"/></param>
        public SplitQuery(Stream stream, ushort pageNumberStart, ushort pageNumberEnd)
        {
            Stream = stream;
            PageNumberStart = pageNumberStart;
            PageNumberEnd = pageNumberEnd;
        }
    }
}
