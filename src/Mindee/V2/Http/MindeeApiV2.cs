using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Input;
using Mindee.V2.ClientOptions;
using Mindee.V2.Parsing;
using Mindee.V2.Parsing.Search;
using Mindee.V2.Product;
using Mindee.V2.Product.Extraction.Params;
using RestSharp;
#if NET6_0_OR_GREATER
using Microsoft.Extensions.DependencyInjection;
#endif

namespace Mindee.V2.Http
{
    internal sealed class MindeeApiV2 : HttpApiV2
    {
        private readonly string _baseUrl;
        private readonly RestClient _httpClient;

        public MindeeApiV2(
            IOptions<Settings> settings,
#if NET6_0_OR_GREATER
            [FromKeyedServices("MindeeV2RestClient")]
#endif
            RestClient httpClient,
            ILogger<MindeeApiV2> logger = null)
        {
            Logger = logger ?? NullLogger<MindeeApiV2>.Instance;
            _httpClient = httpClient;

            if (!string.IsNullOrWhiteSpace(settings.Value.MindeeBaseUrl))
            {
                _baseUrl = settings.Value.MindeeBaseUrl;
            }

            var defaultHeaders = new Dictionary<string, string>
            {
                { "Authorization", $"{settings.Value.ApiKey}" }, { "Connection", "close" }
            };
            _httpClient.AddDefaultHeaders(defaultHeaders);
        }

        public override async Task<JobResponse> ReqPostEnqueueAsync(
            InputSource inputSource,
            BaseParameters parameters
        )
        {
            var productAttributes = parameters.GetType().GetCustomAttribute<ProductAttributes>();
            if (productAttributes == null)
                throw new Exception($"ProductAttributes must be set for class: {parameters.GetType().Name}");
            var request = new RestRequest(
                $"v2/products/{productAttributes.Slug}/enqueue", Method.Post);

            request.AddParameter("model_id", parameters.ModelId);
            AddPredictRequestParameters(inputSource, parameters, request);

            Logger?.LogInformation("HTTP POST to {RequestResource} ...", _baseUrl + request.Resource);
            var response = await _httpClient.ExecutePostAsync(request);
            return HandleJobResponse(response);
        }

        public override async Task<SearchResponse> SearchModels(string name, string modelType)
        {
            var request = new RestRequest("v2/search/models");
            Logger?.LogInformation("Fetching models...");
            if (!string.IsNullOrWhiteSpace(name))
            {
                Logger?.LogInformation("Models matching name like {Name}", name);
                request.AddParameter("name", name);
            }

            if (!string.IsNullOrWhiteSpace(modelType))
            {
                Logger?.LogInformation("Models matching model_type={ModelType}", modelType);
                request.AddParameter("model_type", modelType);
            }

            var response = await _httpClient.ExecuteGetAsync(request);
            return HandleSearchResponse(response);
        }

        public override async Task<JobResponse> ReqGetJobAsync(string jobId)
        {
            var request = new RestRequest($"v2/jobs/{jobId}");
            Logger?.LogInformation("HTTP GET to {RequestResource}...", _baseUrl + request.Resource);
            var response = await _httpClient.ExecuteGetAsync(request);
            Logger?.LogDebug("HTTP response: {ResponseContent}", response.Content);
            var handledResponse = HandleJobResponse(response);
            return handledResponse;
        }

        public override async Task<JobResponse> ReqGetJobFromUrlAsync(string pollingUrl)
        {
            var request = new RestRequest(new Uri(pollingUrl));
            Logger?.LogInformation("HTTP GET to {RequestResource}...", request.Resource);
            var response = await _httpClient.ExecuteGetAsync(request);
            Logger?.LogDebug("HTTP response: {ResponseContent}", response.Content);
            var handledResponse = HandleJobResponse(response);
            return handledResponse;
        }

        public override async Task<TResponse> ReqGetResultAsync<TResponse>(string inferenceId)
        {
            var productAttributes = typeof(TResponse).GetCustomAttribute<ProductAttributes>();
            if (productAttributes == null)
                throw new Exception($"ProductAttributes must be set for class: {typeof(TResponse).Name}");
            var request = new RestRequest(
                $"v2/products/{productAttributes.Slug}/results/{inferenceId}");
            Logger?.LogInformation("HTTP GET to {RequestResource}...", request.Resource);
            var queueResponse = await _httpClient.ExecuteGetAsync(request);
            return HandleProductResponse<TResponse>(queueResponse);
        }

        public override async Task<TResponse> ReqGetResultFromUrlAsync<TResponse>(string resultUrl)
        {
            var request = new RestRequest(new Uri(resultUrl));
            Logger?.LogInformation("HTTP GET to {RequestResource}...", resultUrl);
            var queueResponse = await _httpClient.ExecuteGetAsync(request);
            return HandleProductResponse<TResponse>(queueResponse);
        }

        private static void AddPredictRequestParameters(
            InputSource inputSource, BaseParameters parameters, RestRequest request)
        {
            switch (inputSource)
            {
                case LocalInputSource localInputSource:
                    request.AddFile(
                        "file",
                        localInputSource.FileBytes,
                        localInputSource.Filename);
                    break;
                case UrlInputSource urlInputSource:
                    request.AddParameter("url", urlInputSource.FileUrl.ToString());
                    break;
                case null:
                    throw new MindeeInputException("Input source cannot be null");
                default:
                    throw new MindeeInputException($"Unsupported input source type '{inputSource.GetType()}'");
            }

            foreach (KeyValuePair<string, string> entry in parameters.GetRequestParameters())
            {
                request.AddParameter(entry.Key, entry.Value);
            }
        }

        private SearchResponse HandleSearchResponse(RestResponse restResponse)
        {
            Logger?.LogDebug("HTTP response: {RestResponseContent}", restResponse.Content);
            var statusCode = (int)restResponse.StatusCode;

            if (statusCode is <= 199 or >= 400)
            {
                throw new MindeeHttpExceptionV2(
                    GetErrorFromContent(statusCode, restResponse.Content));
            }

            if (restResponse.Content == null)
            {
                throw new MindeeException("Couldn't deserialize SearchResponse.");
            }
            var model = JsonSerializer.Deserialize<SearchResponse>(restResponse.Content);
            return model ?? throw new MindeeException("Couldn't deserialize SearchResponse.");
        }

        private JobResponse HandleJobResponse(RestResponse restResponse)
        {
            Logger?.LogDebug("HTTP response: {RestResponseContent}", restResponse.Content);
            var statusCode = (int)restResponse.StatusCode;

            if (statusCode is <= 199 or >= 400)
            {
                throw new MindeeHttpExceptionV2(
                    GetErrorFromContent(statusCode, restResponse.Content));
            }

            if (restResponse.Content == null)
            {
                throw new MindeeException("Couldn't deserialize JobResponse.");
            }

            var model = JsonSerializer.Deserialize<JobResponse>(restResponse.Content);
            return model ?? throw new MindeeException("Couldn't deserialize JobResponse.");
        }

        private TResponse HandleProductResponse<TResponse>(RestResponse restResponse)
            where TResponse : BaseResponse, new()
        {
            Logger?.LogDebug("HTTP response: {RestResponseContent}", restResponse.Content);

            var statusCode = (int)restResponse.StatusCode;

            if (statusCode is <= 199 or >= 400)
            {
                throw new MindeeHttpExceptionV2(
                    GetErrorFromContent((int)restResponse.StatusCode, restResponse.Content));
            }

            return DeserializeResponse<TResponse>(restResponse.Content);

        }
    }
}
