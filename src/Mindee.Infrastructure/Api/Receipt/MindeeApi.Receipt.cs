using System.Threading.Tasks;
using Mindee.Infrastructure.Api.Common;
using Mindee.Infrastructure.Api.Receipt;

namespace Mindee.Infrastructure.Api
{
    internal partial class MindeeApi
    {
        /// <summary>
        /// Call the Mindee Receipt API.
        /// </summary>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        /// <returns><see cref="PredictResponse{ReceiptPrediction}"/></returns>
        /// <exception cref="MindeeApiException"></exception>
        public Task<PredictResponse<ReceiptPrediction>> PredictReceiptAsync(
            PredictParameter predictParameter)
        {
            return PredictAsync<ReceiptPrediction>(
                new Endpoint("expense_receipts", "3"),
                predictParameter);
        }
    }
}
