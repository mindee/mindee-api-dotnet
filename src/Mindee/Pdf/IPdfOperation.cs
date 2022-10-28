using System;
using System.Threading.Tasks;

namespace Mindee.Pdf
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
        /// <returns><see cref="SplitPdf"/></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        Task<SplitPdf> SplitAsync(SplitQuery splitQuery);

        Task<SplitPdf> NewSplitAsync(SplitQuery splitQuery);

        /// <summary>
        /// Check if the file can be opened.
        /// </summary>
        /// <param name="file">The file data.</param>
        bool CanBeOpen(byte[] file);
    }
}
