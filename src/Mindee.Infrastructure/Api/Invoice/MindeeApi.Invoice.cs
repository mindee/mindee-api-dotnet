using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mindee.Infrastructure.Api.Invoice;
using RestSharp;

namespace Mindee.Infrastructure.Api
{
    internal partial class MindeeApi
    {
        public async Task<InvoicePredictResponse> PredictAsync(
            Stream file,
            string filename)
        {
            var request = new RestRequest($"products/mindee/invoices/v3/predict", Method.Post);

            _logger.LogInformation($"HTTP request to {BaseUrl + request.Resource} started.");

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                request.AddFile("document", memoryStream.ToArray(), filename);
            }

            var response = await _httpClient.PostAsync<InvoicePredictResponse>(request);

            _logger.LogInformation($"HTTP request to {BaseUrl + request.Resource} finished.");

            return response;
        }
    }
}
