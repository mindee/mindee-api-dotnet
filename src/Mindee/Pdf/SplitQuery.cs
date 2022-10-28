using System.IO;
using Mindee.Input;

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
        /// <see cref="Input.PageOptions"/>
        /// </summary>
        public PageOptions PageOptions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"><see cref="Stream"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        public SplitQuery(Stream stream, PageOptions pageOptions)
        {
            Stream = stream;
            PageOptions = pageOptions;
        }
    }
}
