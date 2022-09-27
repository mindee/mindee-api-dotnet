using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mindee.Infrastructure.Api.Invoice;
using RestSharp;

namespace Mindee.Infrastructure.Api
{
    internal partial class MindeeApi
    {
        /// <summary>
        /// Call the Mindee Invoice API.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="filename"></param>
        /// <exception cref="MindeeApiException"></exception>
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
            
            var response = await _httpClient.ExecutePostAsync<InvoicePredictResponse>(request);

            _logger.LogInformation($"HTTP request to {BaseUrl + request.Resource} finished.");

            if(response.IsSuccessful)
            {
                return response.Data;
            }

            string errorMessage = "Mindee API client : ";

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                errorMessage += response.ErrorMessage;
                _logger.LogCritical(errorMessage);
            }

            if (response.Data != null)
            {
                errorMessage += response.Data.ApiRequest.Error.ToString();
                _logger.LogError(errorMessage);
            }
            else
            {
                errorMessage += $" Unhandled error - {response.ErrorMessage}";
                _logger.LogError(errorMessage);
            }

            throw new MindeeApiException(errorMessage);
        }
    }
}
