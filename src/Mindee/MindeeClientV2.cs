using System;
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

namespace Mindee
{
    /// <summary>
    /// The entry point to use the Mindee V2 API features.
    /// </summary>
    public sealed class MindeeClientV2
    {
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
            _loggerFactory = loggerFactory ?? LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug);
            });
            _logger = _loggerFactory.CreateLogger<MindeeClientV2>();

            var serviceCollection = new ServiceCollection();
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
        /// <param name="mindeeSettings"><see cref="MindeeSettingsV2"/></param>
        /// <param name="logger"></param>
        public MindeeClientV2(MindeeSettingsV2 mindeeSettings, ILoggerFactory logger = null)
        {
            _loggerFactory = logger ?? NullLoggerFactory.Instance;
            var serviceCollection = new ServiceCollection();
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
        /// <param name="httpApi"><see cref="IHttpApi"/></param>
        /// <param name="logger"></param>
        public MindeeClientV2(HttpApiV2 httpApi, ILoggerFactory logger = null)
        {
            _mindeeApi = httpApi;
            _loggerFactory = logger ?? NullLoggerFactory.Instance;
            _logger = _loggerFactory.CreateLogger<MindeeClientV2>();
        }

        /// <summary>
        /// Add a local input source to a Generated async queue.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="inferenceParameters"><see cref="InferenceParameters"/></param>
        /// <returns><see cref="JobResponse"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<JobResponse> EnqueueInferenceAsync(
            LocalInputSource inputSource
            , InferenceParameters inferenceParameters)
        {
            _logger?.LogInformation(message: "Enqueuing: local source");

            return await _mindeeApi.ReqPostEnqueueInferenceAsync(
                new InferencePostParameters(
                    localSource: inputSource,
                    modelId: inferenceParameters.ModelId,
                    alias: inferenceParameters.Alias,
                    webhookIds: inferenceParameters.WebhookIds,
                    rag: inferenceParameters.Rag,
                    rawText: inferenceParameters.RawText,
                    polygon: inferenceParameters.Polygon,
                    confidence: inferenceParameters.Confidence,
                    textContext: inferenceParameters.TextContext
                ));
        }

        /// <summary>
        /// Add a remote input source to a Generated async queue.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="inferenceParameters"><see cref="InferenceParameters"/></param>
        /// <returns><see cref="JobResponse"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<JobResponse> EnqueueInferenceAsync(
            UrlInputSource inputSource
            , InferenceParameters inferenceParameters)
        {
            _logger?.LogInformation(message: "Enqueuing: URL source");

            return await _mindeeApi.ReqPostEnqueueInferenceAsync(
                new InferencePostParameters(
                    urlSource: inputSource,
                    modelId: inferenceParameters.ModelId,
                    alias: inferenceParameters.Alias,
                    webhookIds: inferenceParameters.WebhookIds,
                    rag: inferenceParameters.Rag,
                    rawText: inferenceParameters.RawText,
                    polygon: inferenceParameters.Polygon,
                    confidence: inferenceParameters.Confidence
                ));
        }

        /// <summary>
        /// Get the status of an inference that was previously enqueued.
        /// Can be used for polling.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <returns><see cref="JobResponse"/></returns>
        public async Task<JobResponse> GetJobAsync(string jobId)
        {
            _logger?.LogInformation(message: "Getting Job: {}", jobId);

            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(jobId);
            }
            return await _mindeeApi.ReqGetJobAsync(jobId);
        }

        /// <summary>
        /// Get the status of an inference that was previously enqueued.
        /// Can be used for polling.
        /// </summary>
        /// <param name="inferenceId">The job id.</param>
        /// <returns><see cref="InferenceResponse"/></returns>
        public async Task<InferenceResponse> GetInferenceAsync(string inferenceId)
        {
            _logger?.LogInformation(message: "Getting Inference: {}", inferenceId);

            if (string.IsNullOrWhiteSpace(inferenceId))
            {
                throw new ArgumentNullException(inferenceId);
            }
            return await _mindeeApi.ReqGetInferenceAsync(inferenceId);
        }


        /// <summary>
        /// Add the URL source to an async queue, poll, and parse when complete.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="inferenceParameters"><see cref="InferenceParameters"/></param>
        /// <returns><see cref="InferenceResponse"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<InferenceResponse> EnqueueAndGetInferenceAsync(
            UrlInputSource inputSource
            , InferenceParameters inferenceParameters)
        {
            _logger?.LogInformation(message: "Enqueue and poll: URL source");

            if (inferenceParameters.PollingOptions == null)
            {
                inferenceParameters.PollingOptions = new AsyncPollingOptions();
            }

            var enqueueResponse = await EnqueueInferenceAsync(
                inputSource,
                inferenceParameters);
            return await PollForResultsAsync(enqueueResponse, inferenceParameters.PollingOptions);
        }

        /// <summary>
        /// Add the document to an async queue, poll, and parse when complete.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="inferenceParameters"><see cref="InferenceParameters"/></param>
        /// <returns><see cref="InferenceResponse"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<InferenceResponse> EnqueueAndGetInferenceAsync(
            LocalInputSource inputSource
            , InferenceParameters inferenceParameters)
        {
            _logger?.LogInformation(message: "Enqueue and poll: local source");

            if (inferenceParameters.PollingOptions == null)
            {
                inferenceParameters.PollingOptions = new AsyncPollingOptions();
            }

            var enqueueResponse = await EnqueueInferenceAsync(
                inputSource,
                inferenceParameters);
            return await PollForResultsAsync(enqueueResponse, inferenceParameters.PollingOptions);
        }

        /// <summary>
        /// Poll for results until the prediction is retrieved or the max amount of attempts is reached.
        /// </summary>
        /// <param name="enqueueResponse"><see cref="JobResponse"/></param>
        /// <param name="pollingOptions"><see cref="AsyncPollingOptions"/></param>
        /// <returns><see cref="InferenceResponse"/></returns>
        /// <exception cref="MindeeException">Thrown when maxRetries is reached and the result isn't ready.</exception>
        private async Task<InferenceResponse> PollForResultsAsync(
            JobResponse enqueueResponse,
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
            JobResponse response = enqueueResponse; // First init is only for error handling purposes.
            while (retryCount < maxRetries)
            {
                Thread.Sleep(pollingOptions.IntervalMilliSec);
                _logger?.LogInformation("Attempting to retrieve: {} of {}", retryCount, maxRetries);
                response = await GetJobAsync(jobId);
                if (response.Job.Error != null)
                {
                    break;
                }
                if (response.Job.Status.Equals("Processed"))
                {
                    return await GetInferenceAsync(response.Job.Id);
                }
                retryCount++;
            }
            ErrorResponse error = response.Job.Error;
            if (error != null)
            {
                throw new MindeeHttpExceptionV2(error);
            }
            throw new MindeeException($"Could not complete after {retryCount} attempts.");
        }
    }
}
