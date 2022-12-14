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
        SplitPdf Split(SplitQuery splitQuery);
    }
}
