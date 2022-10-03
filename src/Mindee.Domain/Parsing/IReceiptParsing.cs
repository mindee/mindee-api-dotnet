using System.Threading.Tasks;
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
        /// <param name="parseParameter"><see cref="ParseParameter"/></param>
        /// <returns><see cref="ReceiptInference"/></returns>
        Task<ReceiptInference> ExecuteAsync(ParseParameter parseParameter);
    }
}
