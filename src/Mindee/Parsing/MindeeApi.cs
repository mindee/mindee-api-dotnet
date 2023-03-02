using System;
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
using Mindee.Parsing.Common.Jobs;
using RestSharp;

namespace Mindee.Parsing
{
    internal sealed class MindeeApi : IPredictable
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
                _httpClient = BuildClient(mindeeSettings.Value.RequestTimeoutInSeconds);
            }
        }

        private RestClient BuildClient(int timeoutInSeconds)
        {
            var options = new RestClientOptions(_baseUrl)
            {
                MaxTimeout = TimeSpan.FromSeconds(timeoutInSeconds).Milliseconds,
                FollowRedirects = false
            };

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

        public Task<PredictEnqueuedResponse> EnqueuePredictAsync<TModel>(PredictParameter predictParameter)
            where TModel : class, new()

        {
            return EnqueuePredictAsyncInternalAsync(predictParameter, GetEndpoint<TModel>());
        }

        private async Task<PredictEnqueuedResponse> EnqueuePredictAsyncInternalAsync(
            PredictParameter predictParameter,
            CustomEndpoint endpoint
            )
        {
            var request = new RestRequest($"/products/" +
                $"{endpoint.AccountName}/{endpoint.EndpointName}/v{endpoint.Version}/" +
                $"predict_async", Method.Post);

            _logger?.LogInformation($"HTTP request to {_baseUrl}/{request.Resource} started.");

            request.AddFile("document", predictParameter.File, predictParameter.Filename);
            request.AddParameter("include_mvision", predictParameter.WithFullText);
            request.AddQueryParameter("cropper", predictParameter.WithCropper);

            var response = await _httpClient.ExecutePostAsync(request);

            _logger?.LogDebug($"HTTP response : {response.Content}");
            _logger?.LogInformation($"HTTP request to {_baseUrl + request.Resource} finished.");

            PredictEnqueuedResponse predictEnqueuedResponse = null;

            if (response.Content != null)
            {
                predictEnqueuedResponse = JsonSerializer.Deserialize<PredictEnqueuedResponse>(response.Content);

                return predictEnqueuedResponse;
            }

            var errorMessage = "Mindee API client : ";

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

        public Task<Document<TModel>> PredictAsync<TModel>(PredictParameter predictParameter)
            where TModel : class, new()
        {
            return PredictAsync<TModel>(
                GetEndpoint<TModel>(),
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

            PredictResponse<TModel> predictResponse = ResponseHandler<PredictResponse<TModel>>(response);

            return predictResponse.Document;
        }

        public Task<GetJobResponse<TModel>> GetJobAsync<TModel>(string jobId) where TModel : class, new()
        {
            return GetJobInternalAsync<TModel>(
                jobId,
                GetEndpoint<TModel>());
        }

        private async Task<GetJobResponse<TModel>> GetJobInternalAsync<TModel>(
            string jobId,
            CustomEndpoint endpoint)
            where TModel : class, new()
        {
            var request = new RestRequest($"/products/" +
                $"{endpoint.AccountName}/{endpoint.EndpointName}/v{endpoint.Version}/" +
                $"documents/queue/{jobId}", Method.Get);

            _logger?.LogInformation($"HTTP request to {_baseUrl}/{request.Resource} started.");

            var response = await _httpClient.ExecuteGetAsync(request);

            _logger?.LogInformation($"HTTP request to {_baseUrl + request.Resource} finished.");

            _logger?.LogDebug($"HTTP response : {response.Content}");

            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                var locationHeader = response.ContentHeaders.First(h => h.Name == "Location");

                request = new RestRequest(locationHeader.Value?.ToString());

                _logger?.LogInformation($"HTTP request to {_baseUrl}/{request.Resource} started.");
                response = await _httpClient.ExecuteGetAsync(request);
                _logger?.LogInformation($"HTTP request to {_baseUrl + request.Resource} finished.");
            }

            GetJobResponse<TModel> getJobResponse = ResponseHandler<GetJobResponse<TModel>>(response);

            return getJobResponse;
        }

        private CustomEndpoint GetEndpoint<TModel>()
        {
            if (!Attribute.IsDefined(typeof(TModel), typeof(EndpointAttribute)))
            {
                throw new NotSupportedException($"The type {typeof(TModel)} is not supported as a prediction model. " +
                    $"The endpoint attribute is missing. " +
                    $"Please refer to the document or contact the support.");
            }

            EndpointAttribute endpointAttribute =
            (EndpointAttribute)Attribute.GetCustomAttribute(typeof(TModel), typeof(EndpointAttribute));

            return new CustomEndpoint(endpointAttribute.EndpointName, endpointAttribute.AccountName, endpointAttribute.Version);
        }

        private string BuildUserAgent()
        {
            return $"mindee-api-dotnet@v{Assembly.GetExecutingAssembly().GetName().Version}"
                + $" dotnet-v{Environment.Version}"
                + $" {Environment.OSVersion}";
        }

        private TModel ResponseHandler<TModel>(RestResponse restResponse)
            where TModel : CommonResponse, new()
        {
            string errorMessage = "Mindee API client : ";
            TModel model = null;

            if (!string.IsNullOrWhiteSpace(restResponse.Content))
            {
                try
                {
                    model = JsonSerializer.Deserialize<TModel>(restResponse.Content);
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

            errorMessage += model.ApiRequest.Error.ToString();

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
