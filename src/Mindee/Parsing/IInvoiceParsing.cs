using System.Threading.Tasks;
using Mindee.Parsing.Common;
using Mindee.Parsing.Invoice;

namespace Mindee.Parsing
{
    /// <summary>
    /// Parse a file. 
    /// </summary>
    public interface IInvoiceParsing
    {
        /// <summary>
        /// Execute the parsing.
        /// </summary>
        /// <param name="parseParameter"><see cref="ParseParameter"/></param>
        /// <returns><see cref="InvoiceInference"/></returns>
        Task<Document<InvoicePrediction>> ExecuteAsync(ParseParameter parseParameter);
    }
}
