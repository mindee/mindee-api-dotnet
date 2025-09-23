using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Parsing.V2;
using RestSharp;

namespace Mindee.Http
{
    internal sealed class MindeeApiV2 : HttpApiV2
    {
        private readonly string _baseUrl;
        private readonly RestClient _httpClient;

        public MindeeApiV2(IOptions<MindeeSettings> mindeeSettings, RestClient httpClient,
            ILogger<MindeeApiV2> logger = null)
        {
            Logger = logger ?? NullLogger<MindeeApiV2>.Instance;
            _httpClient = httpClient;

            if (!string.IsNullOrWhiteSpace(mindeeSettings.Value.MindeeBaseUrl))
            {
                _baseUrl = mindeeSettings.Value.MindeeBaseUrl;
            }

            var defaultHeaders = new Dictionary<string, string>
            {
                { "Authorization", $"{mindeeSettings.Value.ApiKey}" }, { "Connection", "close" }
            };
            _httpClient.AddDefaultHeaders(defaultHeaders);
        }

        public override async Task<JobResponse> ReqPostEnqueueInferenceAsync(
            InferencePostParameters predictParameter
        )
        {
            var request = new RestRequest("v2/inferences/enqueue", Method.Post);

            request.AddParameter("model_id", predictParameter.ModelId);
            AddPredictRequestParameters(predictParameter, request);

            Logger?.LogInformation($"HTTP POST to {_baseUrl + request.Resource} ...");

            var response = await _httpClient.ExecutePostAsync(request);
            return ResponseHandler<JobResponse>(response);
        }

        public override async Task<JobResponse> ReqGetJobAsync(string jobId)
        {
            var request = new RestRequest($"v2/jobs/{jobId}");
            Logger?.LogInformation($"HTTP GET to {_baseUrl + request.Resource}...");
            var response = await _httpClient.ExecuteGetAsync(request);
            Logger?.LogDebug($"HTTP response: {response.Content}");
            JobResponse handledResponse = ResponseHandler<JobResponse>(response);
            return handledResponse;
        }


        public override async Task<InferenceResponse> ReqGetInferenceAsync(string inferenceId)
        {
            var request = new RestRequest($"v2/inferences/{inferenceId}");
            Logger?.LogInformation($"HTTP GET to {_baseUrl + request.Resource}...");
            var queueResponse = await _httpClient.ExecuteGetAsync(request);
            var handledResponse = ResponseHandler<InferenceResponse>(queueResponse);
            return handledResponse;
        }

        private static void AddPredictRequestParameters(InferencePostParameters predictParameter, RestRequest request)
        {
            if (predictParameter.LocalSource != null)
            {
                request.AddFile(
                    "file",
                    predictParameter.LocalSource.FileBytes,
                    predictParameter.LocalSource.Filename);
            }
            else if (predictParameter.UrlSource != null)
            {
                request.AddParameter("url", predictParameter.UrlSource.FileUrl.ToString());
            }
            if (!string.IsNullOrWhiteSpace(predictParameter.Alias))
            {
                request.AddParameter(name: "alias", value: predictParameter.Alias);
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

            if (predictParameter.WebhookIds is { Count: > 0 })
            {
                request.AddParameter(name: "webhook_ids", value: string.Join(",", predictParameter.WebhookIds));
            }
        }

        private TResponse ResponseHandler<TResponse>(RestResponse restResponse)
            where TResponse : CommonResponse, new()
        {
            Logger?.LogDebug($"HTTP response: {restResponse.Content}");

            int statusCode = (int)restResponse.StatusCode;

            if (statusCode is > 199 and < 400)
                return DeserializeResponse<TResponse>(restResponse.Content);

            if (restResponse.Content?.Contains("status") ?? false)
            {
                ErrorResponse error = GetErrorFromContent(restResponse.Content);
                throw new MindeeHttpExceptionV2(error.Status, error.Detail);
            }
            throw new MindeeHttpExceptionV2(
                statusCode, restResponse.StatusDescription ?? "Unknown error.");
        }
    }
}
