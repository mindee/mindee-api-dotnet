using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Input;
using Mindee.V2.ClientOptions;
using Mindee.V2.Parsing;
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
            var slug = parameters.Slug;
            var request = new RestRequest($"v2/products/{slug}/enqueue", Method.Post);

            request.AddParameter("model_id", parameters.ModelId);
            AddPredictRequestParameters(inputSource, parameters, request);

            Logger?.LogInformation("HTTP POST to {RequestResource} ...", _baseUrl + request.Resource);
            var response = await _httpClient.ExecutePostAsync(request);
            return HandleJobResponse(response);
        }

        public override async Task<JobResponse> ReqGetJobAsync(string pollingUrl)
        {
            var request = new RestRequest(new Uri(pollingUrl));
            Logger?.LogInformation("HTTP GET to {RequestResource}...", _baseUrl + request.Resource);
            var response = await _httpClient.ExecuteGetAsync(request);
            Logger?.LogDebug("HTTP response: {ResponseContent}", response.Content);
            var handledResponse = HandleJobResponse(response);
            return handledResponse;
        }


        public override async Task<CommonResponse<TProduct>> ReqGetInferenceAsync<TProduct>(string resultUrl)
        {
            var request = new RestRequest(new Uri(resultUrl));
            Logger?.LogInformation("HTTP GET to {RequestResource}...", resultUrl);
            var queueResponse = await _httpClient.ExecuteGetAsync(request);
            return HandleProductResponse<TProduct>(queueResponse);
        }

        private static void AddPredictRequestParameters(InputSource inputSource, BaseParameters parameters, RestRequest request)
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

            if (!string.IsNullOrWhiteSpace(parameters.Alias))
            {
                request.AddParameter("alias", parameters.Alias);
            }

            if (parameters is ExtractionParameters extractionParameters)
            {
                AssignExtractionParameters(request, extractionParameters);
            }

            if (parameters.WebhookIds is { Count: > 0 })
            {
                request.AddParameter("webhook_ids", string.Join(",", parameters.WebhookIds));
            }
        }

        private static void AssignExtractionParameters(RestRequest request, ExtractionParameters parameters)
        {
            if (parameters.Rag != null)
            {
                request.AddParameter("rag", parameters.Rag.Value.ToString());
            }

            if (parameters.RawText != null)
            {
                request.AddParameter("raw_text", parameters.RawText.Value.ToString());
            }

            if (parameters.Polygon != null)
            {
                request.AddParameter("polygon", parameters.Polygon.Value.ToString());
            }

            if (parameters.Confidence != null)
            {
                request.AddParameter("confidence", parameters.Confidence.Value.ToString());
            }

            if (parameters.TextContext != null)
            {
                request.AddParameter("text_context", parameters.TextContext);
            }

            if (parameters.DataSchema != null)
            {
                request.AddParameter("data_schema", parameters.DataSchema.ToString());
            }
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

        private CommonResponse<TProduct> HandleProductResponse<TProduct>(RestResponse restResponse)
            where TProduct : BaseProduct, new()
        {
            Logger?.LogDebug("HTTP response: {RestResponseContent}", restResponse.Content);

            var statusCode = (int)restResponse.StatusCode;

            if (statusCode is <= 199 or >= 400)
            {
                throw new MindeeHttpExceptionV2(
                    GetErrorFromContent((int)restResponse.StatusCode, restResponse.Content));
            }

            var responseType = typeof(TProduct).GetProperty("ResponseType")?.GetValue(null) as Type
                               ?? typeof(CommonResponse<TProduct>);
            return DeserializeResponse<TProduct>(restResponse.Content, responseType);

        }
    }
}
