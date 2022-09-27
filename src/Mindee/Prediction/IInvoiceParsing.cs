using System.IO;
using System.Threading.Tasks;
using Mindee.Prediction.Invoice;

namespace Mindee.Prediction
{
    /// <summary>
    /// Parse a file. 
    /// </summary>
    public interface IInvoiceParsing
    {
        /// <summary>
        /// Execute the parsing.
        /// </summary>
        /// <param name="file">The file data.</param>
        /// <param name="filename">The filename.</param>
        /// <returns><see cref="InvoicePredictResponse"/></returns>
        Task<InvoicePrediction> ExecuteAsync(Stream file, string filename);
    }
}
