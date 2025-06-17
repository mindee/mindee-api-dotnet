using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mindee.Exceptions;
using Mindee.Extensions.DependencyInjection;
using Mindee.Http;
using Mindee.Input;
using Mindee.Parsing.Common;
using Mindee.Pdf;

namespace Mindee
{
    /// <summary>
    /// The entry point to use the Mindee V2 API features.
    /// </summary>
    public sealed class MindeeClientV2
    {
        private readonly IPdfOperation _pdfOperation;
        private readonly IHttpApiV2 _mindeeApi;
        private readonly ILogger _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="apiKey">The required API key to use the Mindee V2 API.</param>
        /// <param name="logger"></param>
        public MindeeClientV2(string apiKey, ILoggerFactory logger = null)
        {
            var serviceCollection = new ServiceCollection();
            _pdfOperation = new DocNetApi();

            serviceCollection.AddMindeeApi(options =>
            {
                options.ApiKey = apiKey;
            });

            var serviceProvider = serviceCollection.BuildServiceProvider();

            if (logger != null)
            {
                serviceCollection.AddSingleton(logger);
                _logger = logger.CreateLogger<MindeeClientV2>();
            }

            _mindeeApi = serviceProvider.GetRequiredService<MindeeApiV2>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mindeeSettings"><see cref="MindeeSettings"/></param>
        /// <param name="logger"></param>
        public MindeeClientV2(MindeeSettings mindeeSettings, ILoggerFactory logger = null)
        {
            var serviceCollection = new ServiceCollection();
            _pdfOperation = new DocNetApi();
            serviceCollection.AddMindeeApi(options =>
            {
                options.ApiKey = mindeeSettings.ApiKey;
                options.MindeeBaseUrl = mindeeSettings.MindeeBaseUrl;
                options.RequestTimeoutSeconds = mindeeSettings.RequestTimeoutSeconds;
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            if (logger != null)
            {
                MindeeLogger.Assign(logger);
                _logger = MindeeLogger.GetLogger();
            }

            _mindeeApi = serviceProvider.GetRequiredService<MindeeApiV2>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pdfOperation"><see cref="IPdfOperation"/></param>
        /// <param name="httpApi"><see cref="IHttpApi"/></param>
        /// <param name="logger"></param>
        public MindeeClientV2(IPdfOperation pdfOperation, IHttpApiV2 httpApi, ILoggerFactory logger = null)
        {
            _pdfOperation = pdfOperation;
            _mindeeApi = httpApi;
            if (logger != null)
            {
                MindeeLogger.Assign(logger);
                _logger = MindeeLogger.GetLogger();
            }
        }


        /// <summary>
        /// Add a local input source to a Generated async queue.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="modelId"></param>
        /// <param name="predictOptions"><see cref="PageOptions"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <returns><see cref="AsyncPredictResponse{TInferenceModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponseV2> EnqueueAsync(
            LocalInputSource inputSource
            , string modelId
            , PredictOptionsV2 predictOptions = null
            , PageOptions pageOptions = null)
        {
            _logger?.LogInformation(message: "Enqueuing...");

            if (predictOptions == null)
            {
                predictOptions = new PredictOptionsV2();
            }

            if (pageOptions != null && inputSource.IsPdf())
            {
                inputSource.FileBytes = _pdfOperation.Split(
                    new SplitQuery(inputSource.FileBytes, pageOptions)).File;
            }

            return await _mindeeApi.EnqueuePostAsync(
                new PredictParameterV2(
                    localSource: inputSource,
                    urlSource: null,
                    fullText: predictOptions.FullText,
                    cropper: predictOptions.Cropper,
                    alias: predictOptions.Alias,
                    webhookIds: predictOptions.WebhookIds,
                    rag: predictOptions.Rag
                    )
                , modelId);
        }

        /// <summary>
        /// Add a URL input source to an async queue.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="modelId"></param>
        /// <param name="predictOptions"><see cref="PageOptions"/></param>
        /// <returns><see cref="AsyncPredictResponse{TInferenceModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponseV2> EnqueueAsync(
            UrlInputSource inputSource
            , string modelId
            , PredictOptionsV2 predictOptions = null)
        {
            _logger?.LogInformation(message: "Enqueuing...");

            if (predictOptions == null)
            {
                predictOptions = new PredictOptionsV2();
            }

            return await _mindeeApi.EnqueuePostAsync(
                new PredictParameterV2(
                    localSource: null,
                    urlSource: inputSource,
                    fullText: predictOptions.FullText,
                    cropper: predictOptions.Cropper,
                    rag: predictOptions.Rag,
                    alias: predictOptions.Alias,
                    webhookIds: predictOptions.WebhookIds
                    )
                , modelId);
        }

        /// <summary>
        /// Parse a document from a Generated async queue.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <returns><see cref="AsyncPredictResponseV2"/></returns>
        public async Task<AsyncPredictResponseV2> ParseQueuedAsync(string jobId)
        {
            _logger?.LogInformation(message: "Parse from queue...");

            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(jobId);
            }

            return await _mindeeApi.DocumentQueueGetAsync(jobId);
        }


        /// <summary>
        /// Add the document to an async queue, poll, and parse when complete.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="modelId"><see cref="string"/></param>
        /// <param name="predictOptions"><see cref="PageOptions"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <param name="pollingOptions"><see cref="AsyncPollingOptions"/></param>
        /// <returns><see cref="AsyncPredictResponseV2"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponseV2> EnqueueAndParseAsync(
            LocalInputSource inputSource
            , string modelId
            , PredictOptionsV2 predictOptions = null
            , PageOptions pageOptions = null
            , AsyncPollingOptions pollingOptions = null)
        {
            _logger?.LogInformation("Asynchronous parsing ...");

            if (pollingOptions == null)
            {
                pollingOptions = new AsyncPollingOptions();
            }

            var enqueueResponse = await EnqueueAsync(
                inputSource,
                modelId,
                predictOptions,
                pageOptions);

            return await PollForResultsAsync(enqueueResponse, pollingOptions);
        }



        /// <summary>
        /// Add the document to an async queue, poll, and parse when complete.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="modelId"><see cref="string"/></param>
        /// <param name="predictOptions"><see cref="PageOptions"/></param>
        /// <param name="pollingOptions"><see cref="AsyncPollingOptions"/></param>
        /// <returns><see cref="AsyncPredictResponseV2"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponseV2> EnqueueAndParseAsync(
            UrlInputSource inputSource
            , string modelId
            , PredictOptionsV2 predictOptions = null
            , AsyncPollingOptions pollingOptions = null)
        {
            _logger?.LogInformation("Asynchronous parsing ...");

            if (pollingOptions == null)
            {
                pollingOptions = new AsyncPollingOptions();
            }

            var enqueueResponse = await EnqueueAsync(
                inputSource,
                modelId,
                predictOptions);

            return await PollForResultsAsync(enqueueResponse, pollingOptions);
        }

        /// <summary>
        /// Poll for results until the prediction is retrieved or the max amount of attempts is reached.
        /// </summary>
        /// <param name="enqueueResponse"><see cref="AsyncPredictResponse{TInferenceModel}"/></param>
        /// <param name="pollingOptions"><see cref="AsyncPollingOptions"/></param>
        /// <returns><see cref="AsyncPredictResponse{TInferenceModel}"/></returns>
        /// <exception cref="MindeeException">Thrown when maxRetries is reached and the result isn't ready.</exception>
        private async Task<AsyncPredictResponseV2> PollForResultsAsync(
            AsyncPredictResponseV2 enqueueResponse,
            AsyncPollingOptions pollingOptions)
        {
            int maxRetries = pollingOptions.MaxRetries + 1;
            string jobId = enqueueResponse.Job.Id;
            _logger?.LogInformation("Enqueued with job ID: {}", jobId);
            _logger?.LogInformation(
                "Waiting {} seconds before attempting to retrieve the document...",
                pollingOptions.InitialDelaySec);
            Thread.Sleep(pollingOptions.InitialDelayMilliSec);
            int retryCount = 1;
            AsyncPredictResponseV2 response;
            while (retryCount < maxRetries)
            {
                Thread.Sleep(pollingOptions.IntervalMilliSec);
                _logger?.LogInformation("Attempting to retrieve: {} of {}", retryCount, maxRetries);
                response = await ParseQueuedAsync(jobId);
                if (response.Inference != null)
                {
                    return response;
                }
                retryCount++;
            }
            throw new MindeeException($"Could not complete after {retryCount} attempts.");
        }

        /// <summary>
        /// Load a local prediction.
        /// Typically used when wanting to load from a webhook callback.
        /// However, any kind of Mindee response may be loaded.
        /// </summary>
        /// <returns></returns>
        public AsyncPredictResponseV2 LoadPrediction(
            LocalResponse localResponse)
        {
            var model = JsonSerializer.Deserialize<AsyncPredictResponseV2>(localResponse.FileBytes);
            model.RawResponse = ToString();

            return model;
        }
    }
}
