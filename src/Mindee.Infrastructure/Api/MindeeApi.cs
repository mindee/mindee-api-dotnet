using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mindee.Infrastructure.Api.Commun;
using RestSharp;

namespace Mindee.Infrastructure.Api
{
    internal partial class MindeeApi
    {
        private const string BaseUrl = "https://api.mindee.net/v1";
        private readonly string _apiKey;
        private readonly RestClient _httpClient;
        private readonly ILogger _logger;

        public MindeeApi(
            ILogger<MindeeApi> logger
            , IOptions<MindeeApiSettings> mindeeApiSettings
            , IConfiguration configuration
            , HttpMessageHandler httpMessageHandler = null
            )
        {
            _logger = logger;
            _apiKey = configuration.GetSection($"{nameof(MindeeApiSettings)}:{nameof(MindeeApiSettings.ApiKey)}").Value;
            if (httpMessageHandler != null)
            {
                _httpClient = BuildClient(httpMessageHandler);
            }
            else
            {
                _httpClient = BuildClient();
            }
        }

        private RestClient BuildClient()
        {
            var options = new RestClientOptions(BaseUrl);
            var client = new RestClient(options,
                p => p.Add("Authorization", $"Token {_apiKey}")
            );

            return client;
        }

        private RestClient BuildClient(HttpMessageHandler httpMessageHandler)
        {
            var options = new RestClientOptions(BaseUrl)
            {
                ConfigureMessageHandler = _ => httpMessageHandler
            };
            var client = new RestClient(options,
                p => p.Add("Authorization", $"Token {_apiKey}")
            );

            return client;
        }

        private async Task<PredictResponse<TModel>> PredictAsync<TModel>(
                    Credential credential, 
                    Stream file,
                    string filename)
            where TModel : class, new()
        {
            var request = new RestRequest($"products/mindee/{credential.ProductName}/v{credential.Version}/predict", Method.Post);

            _logger.LogInformation($"HTTP request to {BaseUrl}/{request.Resource} started.");

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                request.AddFile("document", memoryStream.ToArray(), filename);
            }

            var response = await _httpClient.ExecutePostAsync<PredictResponse<TModel>> (request);

            _logger.LogInformation($"HTTP request to {BaseUrl + request.Resource} finished.");

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            string errorMessage = "Mindee API client : ";

            if (response.StatusCode == HttpStatusCode.InternalServerError)
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
