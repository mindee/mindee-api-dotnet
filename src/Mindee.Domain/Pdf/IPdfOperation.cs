using System;
using System.IO;
using System.Threading.Tasks;

namespace Mindee.Domain.Pdf
{
    /// <summary>
    /// Pdf operations.
    /// </summary>
    public interface IPdfOperation
    {
        /// <summary>
        /// To split a pdf file.
        /// </summary>
        /// <param name="splitQuery"><see cref="SplitQuery"/></param>
        /// <returns><see cref="SplittedPdf"/></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        Task<SplittedPdf> SplitAsync(SplitQuery splitQuery);

        /// <summary>
        /// Check if the file can be opened.
        /// </summary>
        /// <param name="file">The file data.</param>
        bool CanBeOpen(Stream file);
    }
}
