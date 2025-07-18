using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Exceptions;
using Mindee.Extensions.DependencyInjection;
using Mindee.Http;
using Mindee.Input;
using Mindee.Parsing.V2;
using Mindee.Pdf;

namespace Mindee
{
    /// <summary>
    /// The entry point to use the Mindee V2 API features.
    /// </summary>
    public sealed class MindeeClientV2
    {
        private readonly IPdfOperation _pdfOperation;
        private readonly HttpApiV2 _mindeeApi;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="apiKey">The required API key to use the Mindee V2 API.</param>
        /// <param name="loggerFactory">Factory for the logger.</param>
        public MindeeClientV2(string apiKey, ILoggerFactory loggerFactory = null)
        {
            var serviceCollection = new ServiceCollection();
            _loggerFactory = loggerFactory ?? LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug);
            });
            _logger = _loggerFactory.CreateLogger<MindeeClientV2>();

            _pdfOperation = new DocNetApi();
            serviceCollection.AddSingleton(_pdfOperation);
            serviceCollection.AddMindeeApiV2(options =>
            {
                options.ApiKey = apiKey;
            }, _loggerFactory);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _mindeeApi = serviceProvider.GetRequiredService<MindeeApiV2>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mindeeSettings"><see cref="MindeeSettings"/></param>
        /// <param name="logger"></param>
        public MindeeClientV2(MindeeSettings mindeeSettings, ILoggerFactory logger = null)
        {
            _loggerFactory = logger ?? NullLoggerFactory.Instance;
            var serviceCollection = new ServiceCollection();
            _pdfOperation = new DocNetApi();
            serviceCollection.AddMindeeApiV2(options =>
            {
                options.ApiKey = mindeeSettings.ApiKey;
                options.MindeeBaseUrl = mindeeSettings.MindeeBaseUrl;
                options.RequestTimeoutSeconds = mindeeSettings.RequestTimeoutSeconds;
            }, _loggerFactory);
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
        public MindeeClientV2(IPdfOperation pdfOperation, HttpApiV2 httpApi, ILoggerFactory logger = null)
        {
            _pdfOperation = pdfOperation;
            _mindeeApi = httpApi;
            _loggerFactory = logger ?? NullLoggerFactory.Instance;
            _logger = _loggerFactory.CreateLogger<MindeeClientV2>();
        }


        /// <summary>
        /// Add a local input source to a Generated async queue.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="inferencePredictOptions"><see cref="InferencePredictOptions"/></param>
        /// <returns><see cref="AsyncJobResponse"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncJobResponse> EnqueueAsync(
            LocalInputSource inputSource
            , InferencePredictOptions inferencePredictOptions)
        {
            _logger?.LogInformation(message: "Enqueuing...");

            if (inferencePredictOptions.PageOptions != null && inputSource.IsPdf())
            {
                inputSource.FileBytes = _pdfOperation.Split(
                    new SplitQuery(inputSource.FileBytes, inferencePredictOptions.PageOptions)).File;
            }

            return await _mindeeApi.EnqueuePostAsync(
                new PredictParameterV2(
                    localSource: inputSource,
                    modelId: inferencePredictOptions.ModelId,
                    alias: inferencePredictOptions.Alias,
                    webhookIds: inferencePredictOptions.WebhookIds,
                    rag: inferencePredictOptions.Rag
                ));
        }

        /// <summary>
        /// Parse a document from a Generated async queue.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <returns><see cref="AsyncInferenceResponse"/></returns>
        public async Task<AsyncInferenceResponse> ParseQueuedAsync(string jobId)
        {
            _logger?.LogInformation(message: "Parse from queue...");

            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(jobId);
            }
            return await _mindeeApi.GetInferenceFromQueueAsync(jobId);
        }


        /// <summary>
        /// Add the document to an async queue, poll, and parse when complete.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="inferencePredictOptions"><see cref="InferencePredictOptions"/></param>
        /// <returns><see cref="AsyncInferenceResponse"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncInferenceResponse> EnqueueAndParseAsync(
            LocalInputSource inputSource
            , InferencePredictOptions inferencePredictOptions)
        {
            _logger?.LogInformation("Asynchronous parsing ...");

            if (inferencePredictOptions.PollingOptions == null)
            {
                inferencePredictOptions.PollingOptions = new AsyncPollingOptions();
            }

            var enqueueResponse = await EnqueueAsync(
                inputSource,
                inferencePredictOptions);
            return await PollForResultsAsync(enqueueResponse, inferencePredictOptions.PollingOptions);
        }

        /// <summary>
        /// Poll for results until the prediction is retrieved or the max amount of attempts is reached.
        /// </summary>
        /// <param name="enqueueResponse"><see cref="AsyncJobResponse"/></param>
        /// <param name="pollingOptions"><see cref="AsyncPollingOptions"/></param>
        /// <returns><see cref="AsyncInferenceResponse"/></returns>
        /// <exception cref="MindeeException">Thrown when maxRetries is reached and the result isn't ready.</exception>
        private async Task<AsyncInferenceResponse> PollForResultsAsync(
            AsyncJobResponse enqueueResponse,
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
            AsyncInferenceResponse response;
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
        /// Load a local inference.
        /// Typically used when wanting to load from a webhook callback.
        /// However, any kind of Mindee response may be loaded.
        /// </summary>
        /// <returns></returns>
        public AsyncInferenceResponse LoadInference(
            LocalResponse localResponse)
        {
            var model = JsonSerializer.Deserialize<AsyncInferenceResponse>(localResponse.FileBytes);
            model.RawResponse = ToString();

            return model;
        }
    }
}
