using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Parsing.Common;
using RestSharp;

namespace Mindee.Http
{
    internal sealed class MindeeApi : IHttpApi
    {
        private readonly string _baseUrl;
        private readonly Dictionary<string, string> _defaultHeaders;
        private readonly RestClient _httpClient;
        private readonly ILogger<MindeeApi> _logger;

        public MindeeApi(
            IOptions<MindeeSettings> mindeeSettings,
            RestClient httpClient,
            ILogger<MindeeApi> logger = null)
        {
            _logger = logger;
            _httpClient = httpClient;

            if (!string.IsNullOrWhiteSpace(mindeeSettings.Value.MindeeBaseUrl))
            {
                _baseUrl = mindeeSettings.Value.MindeeBaseUrl;
            }

            _defaultHeaders = new Dictionary<string, string>
            {
                { "Authorization", $"Token {mindeeSettings.Value.ApiKey}" }, { "Connection", "close" },
                { "Accept", "application/json" }
            };
            _httpClient.AddDefaultHeaders(_defaultHeaders);
        }

        public async Task<AsyncPredictResponse<TModel>> PredictAsyncPostAsync<TModel>(
            PredictParameter predictParameter
            , CustomEndpoint endpoint = null
        )
            where TModel : class, new()
        {
            if (endpoint is null)
                endpoint = CustomEndpoint.GetEndpoint<TModel>();

            String url;
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

            _logger?.LogInformation($"HTTP POST to {_baseUrl + request.Resource} ...");

            var response = await _httpClient.ExecutePostAsync(request);
            return ResponseHandler<AsyncPredictResponse<TModel>>(response);
        }

        public async Task<PredictResponse<TModel>> PredictPostAsync<TModel>(
            PredictParameter predictParameter
            , CustomEndpoint endpoint = null)
            where TModel : class, new()
        {
            if (endpoint is null)
                endpoint = CustomEndpoint.GetEndpoint<TModel>();

            var request = new RestRequest(
                $"v1/products/{endpoint.GetBaseUrl()}/predict"
                , Method.Post);

            AddPredictRequestParameters(predictParameter, request);

            _logger?.LogInformation($"HTTP POST to {_baseUrl + request.Resource} ...");

            var response = await _httpClient.ExecutePostAsync(request);
            return ResponseHandler<PredictResponse<TModel>>(response);
        }

        public async Task<AsyncPredictResponse<TModel>> DocumentQueueGetAsync<TModel>(
            string jobId
            , CustomEndpoint endpoint = null)
            where TModel : class, new()
        {
            if (endpoint is null)
                endpoint = CustomEndpoint.GetEndpoint<TModel>();

            var queueRequest = new RestRequest(
                $"v1/products/{endpoint.GetBaseUrl()}/documents/queue/{jobId}");

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

            var handledResponse = ResponseHandler<AsyncPredictResponse<TModel>>(queueResponse);
            if (handledResponse.Job?.Error?.Code != null)
            {
                throw new Mindee500Exception(handledResponse.Job.Error.Message);
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

            _logger?.LogInformation($"HTTP POST to {_baseUrl + request.Resource} ...");

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
                request.AddQueryParameter(name: "full_text_ocr", value: "true");
            }

            if (workflowParameter.Alias != null)
            {
                request.AddParameter(name: "alias", value: workflowParameter.Alias);
            }

            if (workflowParameter.PublicUrl != null)
            {
                request.AddParameter(name: "public_url", value: workflowParameter.PublicUrl);
            }

            if (workflowParameter.Priority != null)
            {
                request.AddParameter(
                    name: "priority",
                    value: workflowParameter.Priority != null ?
                        workflowParameter.Priority.ToString()?.ToLower() : null);
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
                request.AddParameter(name: "include_mvision", value: "true");
            }

            if (predictParameter.FullText)
            {
                request.AddQueryParameter(name: "full_text_ocr", value: "true");
            }

            if (predictParameter.Cropper)
            {
                request.AddQueryParameter(name: "cropper", value: "true");
            }
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

            // Always add the status code, this REALLY should be made a property of our HTTP error classes.
            errorMessage += $"HTTP code {restResponse.StatusCode}: ";

            // If the server JSON return is empty, try to dump the raw contents.
            // If that STILL doesn't work, notify the user that the server response is empty.
            var apiErrorMessage = model?.ApiRequest.Error.ToString();
            if (!String.IsNullOrEmpty(apiErrorMessage))
                errorMessage += apiErrorMessage;
            else if (!String.IsNullOrEmpty(restResponse.Content))
                errorMessage += restResponse.Content;
            else
                errorMessage += "Empty response from server.";

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
                    throw new MindeeException(restResponse.ErrorMessage);
            }
        }
    }
}
