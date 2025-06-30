using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Parsing.V2;
using RestSharp;

namespace Mindee.Http
{
    internal sealed class MindeeApiV2 : IHttpApiV2
    {
        private readonly string _baseUrl;
        private readonly RestClient _httpClient;
        private readonly ILogger<MindeeApiV2> _logger;

        public MindeeApiV2(IOptions<MindeeSettings> mindeeSettings, RestClient httpClient,
            ILogger<MindeeApiV2> logger = null)
        {
            _logger = logger ?? NullLogger<MindeeApiV2>.Instance;
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

        public async Task<AsyncJobResponse> EnqueuePostAsync(
            PredictParameterV2 predictParameter
        )
        {
            var request = new RestRequest("v2/inferences/enqueue", Method.Post);

            request.AddParameter("model_id", predictParameter.ModelId);
            AddPredictRequestParameters(predictParameter, request);

            _logger?.LogInformation($"HTTP POST to {_baseUrl + request.Resource} ...");

            var response = await _httpClient.ExecutePostAsync(request);
            return ResponseHandler<AsyncJobResponse>(response);
        }

        public async Task<AsyncInferenceResponse> DocumentQueueGetAsync(string jobId)
        {
            var queueRequest = new RestRequest(
                $"v2/inferences/{jobId}");

            _logger?.LogInformation($"HTTP GET to {_baseUrl + queueRequest.Resource}...");

            var queueResponse = await _httpClient.ExecuteGetAsync(queueRequest);

            _logger?.LogDebug($"HTTP response: {queueResponse.Content}");

            if (queueResponse.StatusCode == HttpStatusCode.Redirect && queueResponse.Headers != null)
            {
                var locationHeader = queueResponse.Headers.First(h => h.Name == "Location");

                var docRequest = new RestRequest(locationHeader.Value);

                _logger?.LogInformation($"HTTP GET to {_baseUrl + docRequest.Resource}...");
                var docResponse = await _httpClient.ExecuteGetAsync(docRequest);
                return ResponseHandler<AsyncInferenceResponse>(docResponse);
            }

            var handledResponse = ResponseHandler<AsyncInferenceResponse>(queueResponse);

            return handledResponse;
        }

        private static void AddPredictRequestParameters(PredictParameterV2 predictParameter, RestRequest request)
        {
            if (predictParameter.LocalSource != null)
            {
                request.AddFile(
                    "file",
                    predictParameter.LocalSource.FileBytes,
                    predictParameter.LocalSource.Filename);
            }
            if (!string.IsNullOrWhiteSpace(predictParameter.Alias))
            {
                request.AddParameter(name: "alias", value: predictParameter.Alias);
            }

            if (predictParameter.Rag)
            {
                request.AddQueryParameter("rag", "true");
            }

            if (predictParameter.WebhookIds is { Count: > 0 })
            {
                request.AddParameter(name: "webhook_ids", value: string.Join(",", predictParameter.WebhookIds));
            }
        }

        private TResponse ResponseHandler<TResponse>(RestResponse restResponse)
            where TResponse : CommonResponse, new()
        {
            _logger?.LogDebug($"HTTP response: {restResponse.Content}");

            string errorMessage = "Mindee API client: ";

            if (!string.IsNullOrWhiteSpace(restResponse.Content))
            {
                _logger?.LogInformation($"Parsing response ...");
                try
                {
                    var model = JsonSerializer.Deserialize<TResponse>(restResponse.Content);
                    model.RawResponse = restResponse.Content;

                    if (restResponse.IsSuccessful)
                    {
                        return model;
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        var pollingErrorResponse =
                            JsonSerializer.Deserialize<AsyncJobResponse>(restResponse.Content);
                        if (pollingErrorResponse.Job?.Error != null)
                        {
                            throw new Mindee500Exception(pollingErrorResponse.Job.Error.Detail);
                        }
                    }
                    catch (Exception)
                    {
                        throw new MindeeException(
                            "Couldn't deserialize response either as a polling error or a valid model response.");
                    }

                    errorMessage += ex.Message;
                    _logger?.LogCritical(errorMessage);
                    throw new MindeeException(errorMessage);
                }
            }


            // -----------------------------------------------------------------
            // [TEMP] Fallback for placeholder error format:
            // {
            //    "status": 400,
            //    "details": "Some explanation"
            // }
            // -----------------------------------------------------------------
            try
            {
                using var doc = JsonDocument.Parse(restResponse.Content ?? "{}");
                if (doc.RootElement.TryGetProperty("status", out var statusProp) &&
                    doc.RootElement.TryGetProperty("detail", out var detailsProp))
                {
                    var fallbackError = new ErrorResponse
                    {
                        Status = statusProp.GetInt32(),
                        Detail = detailsProp.GetString()
                    };

                    throw new MindeeHttpExceptionV2(fallbackError);
                }
                var error = new ErrorResponse { Detail = "Unknown error", Status = 500 };
                throw new MindeeHttpExceptionV2(error);
            }
            catch (JsonException)
            {
                var error = new ErrorResponse { Detail = "Unknown error", Status = 500 };
                throw new MindeeHttpExceptionV2(error);
            }
        }
    }
}
