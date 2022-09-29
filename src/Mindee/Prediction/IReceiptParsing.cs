using System.IO;
using System.Threading.Tasks;
using Mindee.Prediction.Commun;
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
        /// <returns><see cref="Inference{ReceiptPrediction}"/></returns>
        Task<Inference<ReceiptPrediction>> ExecuteAsync(Stream file, string filename);
    }
}
