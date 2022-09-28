using System.IO;
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
        /// <param name="file"></param>
        /// <param name="filename"></param>
        /// <returns><see cref="PredictResponse{InvoicePrediction}"/></returns>
        /// <exception cref="MindeeApiException"></exception>
        public Task<PredictResponse<InvoicePrediction>> PredictInvoiceAsync(
            Stream file,
            string filename)
        {
            return PredictAsync<InvoicePrediction>(
                new Credential("invoices", "3"),
                file,
                filename);
        }
    }
}
