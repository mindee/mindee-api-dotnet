using System.IO;
using System.Threading.Tasks;
using Mindee.Prediction.Receipt;

namespace Mindee.Prediction
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
        /// <returns><see cref="ReceiptPrediction"/></returns>
        Task<ReceiptPrediction> ExecuteAsync(Stream file, string filename);
    }
}
