using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mindee.Domain.Exceptions;
using Mindee.Infrastructure.Api.Common;
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
                    Endpoint endpoint,
                    PredictParameter predictParameter)
            where TModel : class, new()
        {
            var request = new RestRequest($"products/mindee/{endpoint.ProductName}/v{endpoint.Version}/predict", Method.Post);

            _logger.LogInformation($"HTTP request to {BaseUrl}/{request.Resource} started.");

            using (var memoryStream = new MemoryStream())
            {
                await predictParameter.File.CopyToAsync(memoryStream);
                request.AddFile("document", memoryStream.ToArray(), predictParameter.Filename);
                request.AddParameter("include_mvision", predictParameter.WithFullText);
            }

            var response = await _httpClient.ExecutePostAsync(request);

            _logger.LogDebug($"HTTP response : {response.Content}");
            _logger.LogInformation($"HTTP request to {BaseUrl + request.Resource} finished.");

            PredictResponse<TModel> predictResponse = null; 

            if (response.Content != null)
            {
                predictResponse = JsonSerializer.Deserialize<PredictResponse<TModel>>(response.Content);

                if (response.IsSuccessful)
                {
                    return predictResponse;
                }
            }

            string errorMessage = "Mindee API client : ";

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                errorMessage += response.ErrorMessage;
                _logger.LogCritical(errorMessage);
            }

            if (predictResponse != null)
            {
                errorMessage += predictResponse.ApiRequest.Error.ToString();
                _logger.LogError(errorMessage);
            }
            else
            {
                errorMessage += $" Unhandled error - {response.ErrorMessage}";
                _logger.LogError(errorMessage);
            }

            throw new MindeeException(errorMessage);
        }
    }
}
