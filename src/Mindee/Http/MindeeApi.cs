using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Mindee.Http
{
    internal sealed class MindeeApi : IHttpApi
    {
        private readonly string _baseUrl = "https://api.mindee.net/";
        private readonly Dictionary<string, string> _defaultHeaders;
        private readonly RestClient _httpClient;
        private readonly ILogger<MindeeApi> _logger;

        public MindeeApi(
            IOptions<MindeeSettings> mindeeSettings
            , ILogger<MindeeApi> logger = null
            , HttpMessageHandler httpMessageHandler = null
            )
        {
            _logger = logger;

            if (!string.IsNullOrWhiteSpace(mindeeSettings.Value.MindeeBaseUrl))
            {
                _baseUrl = mindeeSettings.Value.MindeeBaseUrl;
            }

            RestClientOptions clientOptions = new RestClientOptions(_baseUrl)
            {
                FollowRedirects = false,
                MaxTimeout = TimeSpan.FromSeconds(mindeeSettings.Value.RequestTimeoutSeconds).Milliseconds
            };
            if (httpMessageHandler != null)
            {
                clientOptions.ConfigureMessageHandler = _ => httpMessageHandler;
            }

            _httpClient = new RestClient(clientOptions);

            _defaultHeaders = new Dictionary<string, string>
            {
                {
                    "Authorization", $"Token {mindeeSettings.Value.ApiKey}"
                },
                {
                    "User-Agent", BuildUserAgent()
                },
            };
            _httpClient.AddDefaultHeaders(_defaultHeaders);
        }

        public Task<AsyncPredictResponse<TModel>> PredictAsyncPostAsync<TModel>(PredictParameter predictParameter)
            where TModel : class, new()
        {
            return PredictAsyncPostAsync<TModel>(predictParameter, GetEndpoint<TModel>());
        }

        private async Task<AsyncPredictResponse<TModel>> PredictAsyncPostAsync<TModel>(
            PredictParameter predictParameter,
            CustomEndpoint endpoint
            )
            where TModel : class, new()
        {
            var request = new RestRequest("v1/products/" +
                $"{endpoint.AccountName}/{endpoint.EndpointName}/v{endpoint.Version}/" +
                "predict_async", Method.Post);

            AddPredictRequestParameters(predictParameter, request);

            _logger?.LogInformation($"HTTP POST to {_baseUrl + request.Resource} ...");

            var response = await _httpClient.ExecutePostAsync(request);

            _logger?.LogDebug($"HTTP response : {response.Content}");

            if (response.Content != null)
            {
                _logger?.LogInformation("Parsing response ...");
                AsyncPredictResponse<TModel> asyncPredictResponse = JsonSerializer.Deserialize<AsyncPredictResponse<TModel>>(response.Content);
                asyncPredictResponse.RawResponse = response.Content;
                return asyncPredictResponse;
            }

            var errorMessage = "Mindee API client: ";

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                errorMessage += response.ErrorMessage;
                _logger?.LogCritical(errorMessage);
            }
            else
            {
                errorMessage += $" Unhandled error - {response.ErrorMessage}";
                _logger?.LogError(errorMessage);
            }

            throw new MindeeException(errorMessage);
        }

        public Task<PredictResponse<TModel>> PredictPostAsync<TModel>(PredictParameter predictParameter)
            where TModel : class, new()
        {
            return PredictPostAsync<TModel>(GetEndpoint<TModel>(), predictParameter);
        }

        public async Task<PredictResponse<TModel>> PredictPostAsync<TModel>(
                    CustomEndpoint endpoint,
                    PredictParameter predictParameter)
            where TModel : class, new()
        {
            var request = new RestRequest("v1/products/" +
                $"{endpoint.AccountName}/{endpoint.EndpointName}/v{endpoint.Version}/" +
                "predict", Method.Post);

            AddPredictRequestParameters(predictParameter, request);

            _logger?.LogInformation($"HTTP POST to {_baseUrl + request.Resource} ...");

            var response = await _httpClient.ExecutePostAsync(request);
            return ResponseHandler<PredictResponse<TModel>>(response);
        }

        public Task<AsyncPredictResponse<TModel>> DocumentQueueGetAsync<TModel>(string jobId)
            where TModel : class, new()
        {
            return DocumentQueueGetAsync<TModel>(jobId, GetEndpoint<TModel>());
        }

        private async Task<AsyncPredictResponse<TModel>> DocumentQueueGetAsync<TModel>(
            string jobId,
            CustomEndpoint endpoint)
            where TModel : class, new()
        {
            var queueRequest = new RestRequest($"v1/products/" +
                $"{endpoint.AccountName}/{endpoint.EndpointName}/v{endpoint.Version}/" +
                $"documents/queue/{jobId}");

            _logger?.LogInformation($"HTTP GET to {_baseUrl + queueRequest.Resource} ...");

            var queueResponse = await _httpClient.ExecuteGetAsync(queueRequest);

            _logger?.LogDebug($"HTTP response: {queueResponse.Content}");

            if (queueResponse.StatusCode == HttpStatusCode.Redirect && queueResponse.Headers != null)
            {
                var locationHeader = queueResponse.Headers.First(h => h.Name == "Location");

                var docRequest = new RestRequest(locationHeader.Value?.ToString());

                _logger?.LogInformation($"HTTP GET to {_baseUrl + docRequest.Resource} ...");
                var docResponse = await _httpClient.ExecuteGetAsync(docRequest);
                return ResponseHandler<AsyncPredictResponse<TModel>>(docResponse);
            }
            return ResponseHandler<AsyncPredictResponse<TModel>>(queueResponse);
        }

        private static CustomEndpoint GetEndpoint<TModel>()
        {
            if (!Attribute.IsDefined(typeof(TModel), typeof(EndpointAttribute)))
            {
                throw new NotSupportedException($"The type {typeof(TModel)} is not supported as a prediction model. " +
                    "The endpoint attribute is missing. " +
                    "Please refer to the document or contact the support.");
            }

            EndpointAttribute endpointAttribute = (EndpointAttribute)Attribute.GetCustomAttribute(
                element: typeof(TModel), typeof(EndpointAttribute));

            return new CustomEndpoint(endpointAttribute.EndpointName, endpointAttribute.AccountName, endpointAttribute.Version);
        }

        private static void AddPredictRequestParameters(PredictParameter predictParameter, RestRequest request)
        {
            request.AddFile("document", predictParameter.File, predictParameter.Filename);
            if (predictParameter.AllWords)
            {
                request.AddParameter(name: "include_mvision", value: "true");
            }
            if (predictParameter.Cropper)
            {
                request.AddQueryParameter(name: "cropper", value: "true");
            }
        }

        private static string BuildUserAgent()
        {
            return $"mindee-api-dotnet@v{Assembly.GetExecutingAssembly().GetName().Version}"
                + $" dotnet-v{Environment.Version}"
                + $" {Environment.OSVersion}";
        }

        private TModel ResponseHandler<TModel>(RestResponse restResponse)
            where TModel : CommonResponse, new()
        {
            _logger?.LogDebug($"HTTP response: {restResponse.Content}");

            string errorMessage = "Mindee API client: ";
            TModel model = null;

            if (!string.IsNullOrWhiteSpace(restResponse.Content))
            {
                _logger?.LogInformation($"Parsing response {typeof(TModel).Name} ...");
                try
                {
                    model = JsonSerializer.Deserialize<TModel>(restResponse.Content);
                    model.RawResponse = restResponse.Content;
                }
                catch (Exception ex)
                {
                    errorMessage += ex.Message;
                    _logger?.LogCritical(errorMessage);
                    throw new MindeeException(errorMessage);
                }

                if (restResponse.IsSuccessful)
                {
                    return model;
                }
            }

            errorMessage += model?.ApiRequest.Error.ToString();

            _logger?.LogError(errorMessage);

            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                    throw new Mindee500Exception(errorMessage);
                case HttpStatusCode.BadRequest:
                    throw new Mindee400Exception(errorMessage);
                case HttpStatusCode.Unauthorized:
                    throw new Mindee401Exception(errorMessage);
                case HttpStatusCode.Forbidden:
                    throw new Mindee403Exception(errorMessage);
                case HttpStatusCode.NotFound:
                    throw new Mindee404Exception(errorMessage);
                case (HttpStatusCode)429:
                    throw new Mindee429Exception(errorMessage);
                default:
                    throw new MindeeException(errorMessage);
            }
        }
    }
}
