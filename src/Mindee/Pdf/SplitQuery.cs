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
        public byte[] File { get; }

        /// <summary>
        /// <see cref="Input.PageOptions"/>
        /// </summary>
        public PageOptions PageOptions { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"><see cref="File"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        public SplitQuery(byte[] file, PageOptions pageOptions)
        {
            File = file;
            PageOptions = pageOptions;
        }
    }
}
