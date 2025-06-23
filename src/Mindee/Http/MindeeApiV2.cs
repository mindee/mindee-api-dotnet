using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Parsing.Common;
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
            _logger = logger;
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

        public async Task<AsyncPollingResponseV2> EnqueuePostAsync(
            PredictParameterV2 predictParameter
        )
        {
            var request = new RestRequest("v2/inferences/enqueue", Method.Post);

            request.AddParameter("model_id", predictParameter.ModelId);
            AddPredictRequestParameters(predictParameter, request);

            _logger?.LogInformation($"HTTP POST to {_baseUrl + request.Resource} ...");

            var response = await _httpClient.ExecutePostAsync(request);
            return ResponseHandler<AsyncPollingResponseV2>(response);
        }

        public async Task<AsyncPredictResponseV2> DocumentQueueGetAsync(string jobId)
        {
            var queueRequest = new RestRequest(
                $"v2/inferences/{jobId}");

            _logger?.LogInformation($"HTTP GET to {_baseUrl + queueRequest.Resource} ...");

            var queueResponse = await _httpClient.ExecuteGetAsync(queueRequest);

            _logger?.LogDebug($"HTTP response: {queueResponse.Content}");

            if (queueResponse.StatusCode == HttpStatusCode.Redirect && queueResponse.Headers != null)
            {
                var locationHeader = queueResponse.Headers.First(h => h.Name == "Location");

                var docRequest = new RestRequest(locationHeader.Value);

                _logger?.LogInformation($"HTTP GET to {_baseUrl + docRequest.Resource} ...");
                var docResponse = await _httpClient.ExecuteGetAsync(docRequest);
                return ResponseHandler<AsyncPredictResponseV2>(docResponse);
            }

            var handledResponse = ResponseHandler<AsyncPredictResponseV2>(queueResponse);

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
            else if (predictParameter.UrlSource != null)
            {
                request.AddParameter(
                    "file",
                    predictParameter.UrlSource.FileUrl.ToString());
            }

            if (predictParameter.FullText)
            {
                request.AddQueryParameter(name: "full_text_ocr", value: "true");
            }

            if (predictParameter.Cropper)
            {
                request.AddQueryParameter(name: "cropper", value: "true");
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
            where TResponse : CommonResponseV2, new()
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
                            JsonSerializer.Deserialize<AsyncPollingResponseV2>(restResponse.Content);
                        if (pollingErrorResponse.Job?.Error?.Code != null)
                        {
                            throw new Mindee500Exception(pollingErrorResponse.Job.Error.Message);
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
                    var fallbackError = new ErrorV2
                    {
                        Status = statusProp.GetInt32(),
                        Detail = detailsProp.GetString()
                    };

                    throw new MindeeHttpExceptionV2(fallbackError);
                }

                var error = new ErrorV2 { Detail = "Unknown error", Code = "Unknown error" };
                throw new MindeeHttpExceptionV2(error);
            }
            catch (JsonException)
            {
                var error = new ErrorV2 { Detail = "Unknown error", Code = "Unknown error" };
                throw new MindeeHttpExceptionV2(error);
            }
        }
    }
}
