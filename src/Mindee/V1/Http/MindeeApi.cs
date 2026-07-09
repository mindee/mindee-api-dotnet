using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.V1.Exceptions;
using Mindee.V1.Parsing.Common;
using RestSharp;

namespace Mindee.V1.Http
{
    internal sealed class MindeeApi : IHttpApi
    {
        private readonly string _baseUrl;
        private readonly RestClient _httpClient;
        private readonly ILogger<MindeeApi> _logger;

        public MindeeApi(
            IOptions<Settings> settings,
            RestClient httpClient,
            ILogger<MindeeApi> logger = null)
        {
            _logger = logger;
            _httpClient = httpClient;

            if (!string.IsNullOrWhiteSpace(settings.Value.MindeeBaseUrl))
            {
                _baseUrl = settings.Value.MindeeBaseUrl;
            }

            var defaultHeaders = new Dictionary<string, string>
            {
                { "Authorization", $"Token {settings.Value.ApiKey}" }, { "Connection", "close" }
            };
            _httpClient.AddDefaultHeaders(defaultHeaders);
        }

        public async Task<AsyncPredictResponse<TModel>> PredictAsyncPostAsync<TModel>(
            PredictParameter predictParameter
            , CustomEndpoint endpoint = null
        )
            where TModel : class, new()
        {
            endpoint ??= CustomEndpoint.GetEndpoint<TModel>();

            string url;
            if (predictParameter.WorkflowId != null)
            {
                url = $"v1/workflows/{predictParameter.WorkflowId}/predict_async";
            }
            else
            {
                url = $"v1/products/{endpoint.GetBaseUrl()}/predict_async";
            }

            var request = new RestRequest(url, Method.Post);

            if (predictParameter.Rag)
            {
                request.AddQueryParameter("rag", "true");
            }

            AddPredictRequestParameters(predictParameter, request);

            _logger?.LogInformation("HTTP POST to {RequestResource} ...", _baseUrl + request.Resource);

            var response = await _httpClient.ExecutePostAsync(request);
            return ResponseHandler<AsyncPredictResponse<TModel>>(response);
        }

        public async Task<PredictResponse<TModel>> PredictPostAsync<TModel>(
            PredictParameter predictParameter
            , CustomEndpoint endpoint = null)
            where TModel : class, new()
        {
            if (endpoint is null)
            {
                endpoint = CustomEndpoint.GetEndpoint<TModel>();
            }

            var request = new RestRequest(
                $"v1/products/{endpoint.GetBaseUrl()}/predict"
                , Method.Post);

            AddPredictRequestParameters(predictParameter, request);

            _logger?.LogInformation("HTTP POST to {RequestResource} ...", _baseUrl + request.Resource);

            var response = await _httpClient.ExecutePostAsync(request);
            return ResponseHandler<PredictResponse<TModel>>(response);
        }

        public async Task<AsyncPredictResponse<TModel>> DocumentQueueGetAsync<TModel>(
            string jobId
            , CustomEndpoint endpoint = null
            , CancellationToken ct = default)
            where TModel : class, new()
        {
            if (endpoint is null)
            {
                endpoint = CustomEndpoint.GetEndpoint<TModel>();
            }

            var queueRequest = new RestRequest(
                $"v1/products/{endpoint.GetBaseUrl()}/documents/queue/{jobId}");

            _logger?.LogInformation("HTTP GET to {QueueRequestResource} ...", _baseUrl + queueRequest.Resource);

            var queueResponse = await _httpClient.ExecuteGetAsync(queueRequest, ct);

            _logger?.LogDebug("HTTP response: {QueueResponseContent}", queueResponse.Content);

            if (queueResponse.StatusCode == HttpStatusCode.Redirect && queueResponse.Headers != null)
            {
                var locationHeader = queueResponse.Headers.First(h => h.Name == "Location");

                var docRequest = new RestRequest(locationHeader.Value);

                _logger?.LogInformation("HTTP GET to {DocRequestResource} ...", _baseUrl + docRequest.Resource);
                var docResponse = await _httpClient.ExecuteGetAsync(docRequest, ct);
                return ResponseHandler<AsyncPredictResponse<TModel>>(docResponse);
            }

            var handledResponse = ResponseHandler<AsyncPredictResponse<TModel>>(queueResponse);
            if (handledResponse.Job?.Error?.Code != null)
            {
                Int32.TryParse(handledResponse.Job.Error.Code, out var statusCode);
                throw new MindeeHttpExceptionV1(
                    "MindeeHttpError",
                    handledResponse.Job.Error.Message,
                    handledResponse.Job.Error.Details,
                    statusCode
                );
            }

            return handledResponse;
        }

        public async Task<WorkflowResponse<TModel>> PostWorkflowExecution<TModel>(
            string workflowId,
            WorkflowParameter workflowParameter)
            where TModel : class, new()
        {
            var request = new RestRequest(
                $"v1/workflows/{workflowId}/executions"
                , Method.Post);

            AddWorkflowRequestParameters(workflowParameter, request);

            _logger?.LogInformation("HTTP POST to {RequestResource} ...", _baseUrl + request.Resource);

            var response = await _httpClient.ExecutePostAsync(request);
            return ResponseHandler<WorkflowResponse<TModel>>(response);
        }

        private static void AddWorkflowRequestParameters(WorkflowParameter workflowParameter, RestRequest request)
        {
            if (workflowParameter.LocalSource != null)
            {
                request.AddFile(
                    "document",
                    workflowParameter.LocalSource.FileBytes,
                    workflowParameter.LocalSource.Filename);
            }
            else if (workflowParameter.UrlSource != null)
            {
                request.AddParameter(
                    "document",
                    workflowParameter.UrlSource.FileUrl.ToString());
            }

            if (workflowParameter.FullText)
            {
                request.AddQueryParameter("full_text_ocr", "true");
            }

            if (workflowParameter.Alias != null)
            {
                request.AddParameter("alias", workflowParameter.Alias);
            }

            if (workflowParameter.PublicUrl != null)
            {
                request.AddParameter("public_url", workflowParameter.PublicUrl);
            }

            if (workflowParameter.Priority != null)
            {
                request.AddParameter(
                    "priority",
                    workflowParameter.Priority?.ToString().ToLower());
            }

            if (workflowParameter.Rag)
            {
                request.AddQueryParameter("rag", "true");
            }
        }

        private static void AddPredictRequestParameters(PredictParameter predictParameter, RestRequest request)
        {
            if (predictParameter.LocalSource != null)
            {
                request.AddFile(
                    "document",
                    predictParameter.LocalSource.FileBytes,
                    predictParameter.LocalSource.Filename);
            }
            else if (predictParameter.UrlSource != null)
            {
                request.AddParameter(
                    "document",
                    predictParameter.UrlSource.FileUrl.ToString());
            }

            if (predictParameter.AllWords)
            {
                request.AddParameter("include_mvision", "true");
            }

            if (predictParameter.FullText)
            {
                request.AddQueryParameter("full_text_ocr", "true");
            }

            if (predictParameter.Cropper)
            {
                request.AddQueryParameter("cropper", "true");
            }
        }

        private TModel ResponseHandler<TModel>(RestResponse restResponse)
            where TModel : CommonResponse, new()
        {
            _logger?.LogDebug("HTTP response: {RestResponseContent}", restResponse.Content);

            var errorMessage = "Mindee API client: ";
            TModel model = null;

            if (!string.IsNullOrWhiteSpace(restResponse.Content))
            {
                _logger?.LogInformation("Parsing response {Name} ...", typeof(TModel).Name);
                try
                {
                    model = JsonSerializer.Deserialize<TModel>(restResponse.Content);
                    model.RawResponse = restResponse.Content;
                }
                catch (Exception ex)
                {
                    _logger?.LogCritical(ex, "Mindee API client: {ErrorMessage}", ex.Message);
                    throw new MindeeException(errorMessage);
                }

                if (restResponse.IsSuccessful)
                {
                    return model;
                }
            }

            var statusCode = (int)restResponse.StatusCode;
            errorMessage += $"HTTP code {statusCode}: ";

            var apiErrorMessage = model?.ApiRequest.Error.ToString();
            string errorName;
            ErrorDetails errorDetails;

            if (!string.IsNullOrEmpty(apiErrorMessage))
            {
                // JSON error body parsed successfully
                errorMessage += apiErrorMessage;
                errorName = model.ApiRequest.Error.Code ?? "MindeeHttpError";
                errorDetails = model.ApiRequest.Error.Details;
            }
            else
            {
                // Non-JSON (likely HTML) — apply substring heuristics
                var body = restResponse.Content ?? string.Empty;
                errorName = ClassifyHtmlError(body);
                errorMessage += string.IsNullOrEmpty(body) ? "Empty response from server." : body;
                errorDetails = null;
            }

            _logger?.LogError("{ErrorMessage}", errorMessage);

            throw BuildHttpException(errorName, errorMessage, errorDetails, statusCode);
        }

        /// <summary>
        ///     Classifies a non-JSON (HTML) error body into a meaningful error-name string,
        ///     matching the heuristics used by sibling SDKs (Python / Node).
        /// </summary>
        private static string ClassifyHtmlError(string body)
        {
            if (body.Contains("Maximum pdf pages", StringComparison.OrdinalIgnoreCase))
                return "TooManyPages";
            if (body.Contains("Max file size is", StringComparison.OrdinalIgnoreCase))
                return "FileTooLarge";
            if (body.Contains("Invalid file type", StringComparison.OrdinalIgnoreCase))
                return "InvalidFiletype";
            if (body.Contains("Gateway timeout", StringComparison.OrdinalIgnoreCase))
                return "RequestTimeout";
            if (body.Contains("Bad gateway", StringComparison.OrdinalIgnoreCase))
                return "BadRequest";
            if (body.Contains("Too Many Requests", StringComparison.OrdinalIgnoreCase))
                return "TooManyRequests";
            return "UnknownError";
        }

        /// <summary>
        ///     Constructs the most specific typed exception for the given HTTP status code.
        /// </summary>
        private static MindeeHttpExceptionV1 BuildHttpException(
            string name, string message, ErrorDetails details, int statusCode)
        {
            return statusCode switch
            {
                400 => new MindeeHttp400ExceptionV1(name, message, details),
                401 => new MindeeHttp401ExceptionV1(name, message, details),
                403 => new MindeeHttp403ExceptionV1(name, message, details),
                404 => new MindeeHttp404ExceptionV1(name, message, details),
                413 => new MindeeHttp413ExceptionV1(name, message, details),
                429 => new MindeeHttp429ExceptionV1(name, message, details),
                500 => new MindeeHttp500ExceptionV1(name, message, details),
                504 => new MindeeHttp504ExceptionV1(name, message, details),
                _ => new MindeeHttpExceptionV1(name, message, details, statusCode),
            };
        }
    }
}
