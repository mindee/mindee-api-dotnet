using System.Threading.Tasks;
using Mindee.Infrastructure.Api.Commun;
using Mindee.Infrastructure.Api.Invoice;

namespace Mindee.Infrastructure.Api
{
    internal partial class MindeeApi
    {
        /// <summary>
        /// Call the Mindee Invoice API.
        /// </summary>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        /// <returns><see cref="PredictResponse{InvoicePrediction}"/></returns>
        /// <exception cref="MindeeApiException"></exception>
        public Task<PredictResponse<InvoicePrediction>> PredictInvoiceAsync(
            PredictParameter predictParameter)
        {
            return PredictAsync<InvoicePrediction>(
                new Endpoint("invoices", "3"),
                predictParameter);
        }
    }
}
