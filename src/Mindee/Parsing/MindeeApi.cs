using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Parsing.Common;
using RestSharp;

namespace Mindee.Parsing
{
    internal sealed class MindeeApi
    {
        private readonly string _baseUrl = "https://api.mindee.net/v1";
        private readonly string _apiKey;
        private readonly RestClient _httpClient;
        private readonly ILogger _logger;

        public MindeeApi(
            IOptions<MindeeSettings> mindeeSettings
            , ILogger logger = null
            , HttpMessageHandler httpMessageHandler = null
            )
        {
            _logger = logger;
            _apiKey = mindeeSettings.Value.ApiKey;
            if (!string.IsNullOrWhiteSpace(mindeeSettings.Value.MindeeBaseUrl))
            {
                _baseUrl = mindeeSettings.Value.MindeeBaseUrl;
            }

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
            var options = new RestClientOptions(_baseUrl);
            var client = new RestClient(options,
                p => p.Add("Authorization", $"Token {_apiKey}")
            );

            client.AddDefaultHeader("User-Agent", BuildUserAgent());

            return client;
        }

        private RestClient BuildClient(HttpMessageHandler httpMessageHandler)
        {
            var options = new RestClientOptions(_baseUrl)
            {
                ConfigureMessageHandler = _ => httpMessageHandler
            };
            var client = new RestClient(options,
                p => p.Add("Authorization", $"Token {_apiKey}")
            );

            return client;
        }

        public Task<Document<TModel>> PredictAsync<TModel>(
            PredictParameter predictParameter)
            where TModel : class, new()
        {
            if (!Attribute.IsDefined(typeof(TModel), typeof(EndpointAttribute)))
            {
                throw new NotSupportedException($"The type {typeof(TModel)} is not supported as a prediction model. " +
                    $"The endpoint attribute is missing. " +
                    $"Please refer to the document or contact the support.");
            }

            EndpointAttribute endpointAttribute =
            (EndpointAttribute)Attribute.GetCustomAttribute(typeof(TModel), typeof(EndpointAttribute));

            return PredictAsync<TModel>(
                new CustomEndpoint(endpointAttribute.EndpointName, endpointAttribute.AccountName, endpointAttribute.Version),
                predictParameter);
        }

        public async Task<Document<TModel>> PredictAsync<TModel>(
                    CustomEndpoint endpoint,
                    PredictParameter predictParameter)
            where TModel : class, new()
        {
            var request = new RestRequest($"/products/{endpoint.AccountName}/{endpoint.EndpointName}/v{endpoint.Version}/predict", Method.Post);

            _logger?.LogInformation($"HTTP request to {_baseUrl}/{request.Resource} started.");

            request.AddFile("document", predictParameter.File, predictParameter.Filename);
            request.AddParameter("include_mvision", predictParameter.WithFullText);
            request.AddQueryParameter("cropper", predictParameter.WithCropper);

            var response = await _httpClient.ExecutePostAsync(request);

            _logger?.LogDebug($"HTTP response : {response.Content}");
            _logger?.LogInformation($"HTTP request to {_baseUrl + request.Resource} finished.");

            PredictResponse<TModel> predictResponse = null;

            if (response.Content != null)
            {
                predictResponse = JsonSerializer.Deserialize<PredictResponse<TModel>>(response.Content);

                if (response.IsSuccessful)
                {
                    return predictResponse.Document;
                }
            }

            var errorMessage = "Mindee API client : ";

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                errorMessage += response.ErrorMessage;
                _logger?.LogCritical(errorMessage);
            }

            if (predictResponse != null)
            {
                errorMessage += predictResponse.ApiRequest.Error.ToString();
                _logger?.LogError(errorMessage);
            }
            else
            {
                errorMessage += $" Unhandled error - {response.ErrorMessage}";
                _logger?.LogError(errorMessage);
            }

            throw new MindeeException(errorMessage);
        }

        private string BuildUserAgent()
        {
            return $"mindee-api-dotnet@v{Assembly.GetExecutingAssembly().GetName().Version}"
                + $" dotnet-v{Environment.Version}"
                + $" {Environment.OSVersion}";
        }
    }
}
