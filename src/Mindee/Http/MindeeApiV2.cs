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
        private readonly Dictionary<string, string> _defaultHeaders;
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

            _defaultHeaders = new Dictionary<string, string>
            {
                { "Authorization", $"Token {mindeeSettings.Value.ApiKey}" }, { "Connection", "close" }
            };
            _httpClient.AddDefaultHeaders(_defaultHeaders);
        }

        public async Task<AsyncPredictResponseV2> EnqueuePostAsync(
            PredictParameterV2 predictParameter
            , string modelId
        )
        {
            var request = new RestRequest("v2/inferences/enqueue", Method.Post);

            request.AddQueryParameter("model_id", modelId);
            AddPredictRequestParameters(predictParameter, request);

            _logger?.LogInformation($"HTTP POST to {_baseUrl + request.Resource} ...");

            var response = await _httpClient.ExecutePostAsync(request);
            return ResponseHandler(response);
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
                return ResponseHandler(docResponse);
            }

            var handledResponse = ResponseHandler(queueResponse);
            if (handledResponse.Job?.Error?.Code != null)
            {
                throw new Mindee500Exception(handledResponse.Job.Error.Message);
            }

            return handledResponse;
        }

        private static void AddPredictRequestParameters(PredictParameterV2 predictParameter, RestRequest request)
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

            if (predictParameter.WebhookIds.Count > 0)
            {
                request.AddParameter(name: "webhook_ids", value: string.Join(",", predictParameter.WebhookIds));
            }
        }

        private AsyncPredictResponseV2 ResponseHandler(RestResponse restResponse)
        {
            _logger?.LogDebug($"HTTP response: {restResponse.Content}");

            string errorMessage = "Mindee API client: ";
            AsyncPredictResponseV2 model = null;

            if (!string.IsNullOrWhiteSpace(restResponse.Content))
            {
                _logger?.LogInformation($"Parsing response ...");
                try
                {
                    model = JsonSerializer.Deserialize<AsyncPredictResponseV2>(restResponse.Content);
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

            throw new MindeeHttpException(model?.ApiRequest.Error);

        }
    }
}
