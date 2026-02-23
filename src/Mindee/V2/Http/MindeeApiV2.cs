using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Input;
using Mindee.V2.Parsing;
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

        public override async Task<JobResponse> ReqPostEnqueueInferenceAsync(
            InputSource inputSource,
            InferenceParameters predictParameter
        )
        {
            var request = new RestRequest("v2/inferences/enqueue", Method.Post);

            request.AddParameter("model_id", predictParameter.ModelId);
            AddPredictRequestParameters(inputSource, predictParameter, request);

            Logger?.LogInformation("HTTP POST to {RequestResource} ...", _baseUrl + request.Resource);

            var response = await _httpClient.ExecutePostAsync(request);
            return ResponseHandler<JobResponse>(response);
        }

        public override async Task<JobResponse> ReqGetJobAsync(string jobId)
        {
            var request = new RestRequest($"v2/jobs/{jobId}");
            Logger?.LogInformation("HTTP GET to {RequestResource}...", _baseUrl + request.Resource);
            var response = await _httpClient.ExecuteGetAsync(request);
            Logger?.LogDebug("HTTP response: {ResponseContent}", response.Content);
            var handledResponse = ResponseHandler<JobResponse>(response);
            return handledResponse;
        }


        public override async Task<InferenceResponse> ReqGetInferenceAsync(string inferenceId)
        {
            var request = new RestRequest($"v2/inferences/{inferenceId}");
            Logger?.LogInformation("HTTP GET to {RequestResource}...", _baseUrl + request.Resource);
            var queueResponse = await _httpClient.ExecuteGetAsync(request);
            return ResponseHandler<InferenceResponse>(queueResponse);
        }

        private static void AddPredictRequestParameters(InputSource inputSource, InferenceParameters predictParameter, RestRequest request)
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
                    throw new MindeeInputException($"Input source cannot be null");
                default:
                    throw new MindeeInputException($"Unsupported input source type '{inputSource.GetType()}'");
            }

            if (!string.IsNullOrWhiteSpace(predictParameter.Alias))
            {
                request.AddParameter("alias", predictParameter.Alias);
            }

            if (predictParameter.Rag != null)
            {
                request.AddParameter("rag", predictParameter.Rag.Value.ToString());
            }

            if (predictParameter.RawText != null)
            {
                request.AddParameter("raw_text", predictParameter.RawText.Value.ToString());
            }

            if (predictParameter.Polygon != null)
            {
                request.AddParameter("polygon", predictParameter.Polygon.Value.ToString());
            }

            if (predictParameter.Confidence != null)
            {
                request.AddParameter("confidence", predictParameter.Confidence.Value.ToString());
            }

            if (predictParameter.TextContext != null)
            {
                request.AddParameter("text_context", predictParameter.TextContext);
            }

            if (predictParameter.DataSchema != null)
            {
                request.AddParameter("data_schema", predictParameter.DataSchema.ToString());
            }

            if (predictParameter.WebhookIds is { Count: > 0 })
            {
                request.AddParameter("webhook_ids", string.Join(",", predictParameter.WebhookIds));
            }
        }

        private TResponse ResponseHandler<TResponse>(RestResponse restResponse)
            where TResponse : CommonResponse, new()
        {
            Logger?.LogDebug("HTTP response: {RestResponseContent}", restResponse.Content);

            var statusCode = (int)restResponse.StatusCode;

            if (statusCode is > 199 and < 400)
            {
                return DeserializeResponse<TResponse>(restResponse.Content);
            }

            throw new MindeeHttpExceptionV2(
                GetErrorFromContent((int)restResponse.StatusCode, restResponse.Content));
        }
    }
}
