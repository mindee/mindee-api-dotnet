using System.Threading.Tasks;
using Mindee.Parsing.Common;
using Mindee.Parsing.Receipt;

namespace Mindee.Parsing
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
        /// <returns><see cref="Document{ReceiptPrediction}"/></returns>
        Task<Document<ReceiptPrediction>> ExecuteAsync(ParseParameter parseParameter);
    }
}
