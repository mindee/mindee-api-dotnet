using System.IO;
using System.Threading.Tasks;
using Mindee.Infrastructure.Api.Commun;
using Mindee.Infrastructure.Api.Receipt;

namespace Mindee.Infrastructure.Api
{
    internal partial class MindeeApi
    {
        /// <summary>
        /// Call the Mindee Receipt API.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="filename"></param>
        /// <returns><see cref="ReceiptPredictResponse"/></returns>
        /// <exception cref="MindeeApiException"></exception>
        public Task<PredictResponse<ReceiptPrediction>> PredictReceiptAsync(
            Stream file,
            string filename)
        {
            return PredictAsync<ReceiptPrediction>(
                new Credential("receipts", "3"), 
                file, 
                filename);
        }
    }
}
