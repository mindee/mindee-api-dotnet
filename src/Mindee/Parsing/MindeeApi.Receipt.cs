using System.Threading.Tasks;
using Mindee.Parsing.Common;
using Mindee.Parsing.Receipt;

namespace Mindee.Parsing
{
    public partial class MindeeApi
    {
        /// <summary>
        /// Call the Mindee Receipt API.
        /// </summary>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        /// <returns><see cref="PredictResponse{ReceiptPrediction}"/></returns>
        /// <exception cref="MindeeApiException"></exception>
        public Task<Document<ReceiptPrediction>> PredictReceiptAsync(
            PredictParameter predictParameter)
        {
            return PredictAsync<ReceiptPrediction>(
                new Endpoint("expense_receipts", "3"),
                predictParameter);
        }
    }
}
