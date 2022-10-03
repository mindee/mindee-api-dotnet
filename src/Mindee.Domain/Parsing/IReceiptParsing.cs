using System.IO;
using System.Threading.Tasks;
using Mindee.Domain.Parsing.Common;
using Mindee.Domain.Parsing.Receipt;

namespace Mindee.Domain.Parsing
{
    /// <summary>
    /// Parse a receipt file. 
    /// </summary>
    public interface IReceiptParsing
    {
        /// <summary>
        /// Execute the parsing.
        /// </summary>
        /// <param name="file">The file data.</param>
        /// <param name="filename">The filename.</param>
        /// <returns><see cref="ReceiptInference"/></returns>
        Task<ReceiptInference> ExecuteAsync(Stream file, string filename);
    }
}
