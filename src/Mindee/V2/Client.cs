using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Exceptions;
using Mindee.Extensions.DependencyInjection;
using Mindee.Input;
using Mindee.V2.ClientOptions;
using Mindee.V2.Exceptions;
using Mindee.V2.Http;
using Mindee.V2.Parsing;
using Mindee.V2.Parsing.Search;
using Mindee.V2.Product.Extraction;
using Mindee.V2.Product.Extraction.Params;
using SettingsV2 = Mindee.V2.Http.Settings;
// ReSharper disable once RedundantUsingDirective

namespace Mindee.V2
{
    /// <summary>
    ///     The entry point to use the Mindee V2 API features.
    /// </summary>
    public sealed class Client
    {
        private readonly ILogger _logger;
        private readonly HttpApiV2 _mindeeApi;

        /// <summary>
        /// </summary>
        /// <param name="apiKey">The required API key to use the Mindee V2 API.</param>
        /// <param name="loggerFactory">Factory for the logger.</param>
        public Client(string apiKey, ILoggerFactory loggerFactory = null)
        {
            var loggerFactory1 = loggerFactory ?? LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug);
            });
            _logger = loggerFactory1.CreateLogger<Client>();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMindeeApiV2(options =>
            {
                options.ApiKey = apiKey;
            }, loggerFactory1);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _mindeeApi = serviceProvider.GetRequiredService<MindeeApiV2>();
        }

        /// <summary>
        /// </summary>
        /// <param name="settings">
        ///     <see cref="SettingsV2" />
        /// </param>
        /// <param name="logger"></param>
        public Client(SettingsV2 settings, ILoggerFactory logger = null)
        {
            var loggerFactory = logger ?? NullLoggerFactory.Instance;
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMindeeApiV2(options =>
            {
                options.ApiKey = settings.ApiKey;
                options.MindeeBaseUrl = settings.MindeeBaseUrl;
                options.RequestTimeoutSeconds = settings.RequestTimeoutSeconds;
            }, loggerFactory);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            if (logger != null)
            {
                MindeeLogger.Assign(logger);
                _logger = MindeeLogger.GetLogger();
            }

            _mindeeApi = serviceProvider.GetRequiredService<MindeeApiV2>();
        }

        /// <summary>
        /// </summary>
        /// <param name="httpApi">
        ///     <see cref="HttpApiV2" />
        /// </param>
        /// <param name="logger"></param>
        public Client(HttpApiV2 httpApi, ILoggerFactory logger = null)
        {
            _mindeeApi = httpApi;
            var loggerFactory = logger ?? NullLoggerFactory.Instance;
            _logger = loggerFactory.CreateLogger<Client>();
        }

        /// <summary>
        ///     Send a document to the Mindee API for inference.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        ///     <see cref="UrlInputSource" />
        /// </param>
        /// <param name="parameters">
        ///     <see cref="ExtractionParameters" />
        /// </param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>
        ///     <see cref="JobResponse" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<JobResponse> EnqueueAsync(
            InputSource inputSource
            , BaseParameters parameters
            , CancellationToken ct = default)
        {
            switch (inputSource)
            {
                case LocalInputSource:
                    _logger?.LogInformation("Enqueuing: local source");
                    break;
                case UrlInputSource:
                    _logger?.LogInformation("Enqueuing: URL source");
                    break;
                case null:
                    throw new ArgumentNullException(nameof(inputSource));
                default:
                    throw new MindeeInputException($"Unsupported input source {inputSource.GetType().Name}");
            }
            return await _mindeeApi.ReqPostEnqueueAsync(inputSource, parameters, ct);
        }

        /// <summary>
        ///     Get the status of an inference that was previously enqueued.
        ///     Can be used for polling.
        /// </summary>
        /// <param name="pollingUrl">The URL to poll to retrieve the job.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>
        ///     <see cref="JobResponse" />
        /// </returns>
        public async Task<JobResponse> GetJobFromUrlAsync(string pollingUrl, CancellationToken ct = default)
        {
            _logger?.LogInformation("Getting Job by URL: {}", pollingUrl);

            if (string.IsNullOrWhiteSpace(pollingUrl))
            {
                throw new ArgumentNullException(pollingUrl);
            }

            return await _mindeeApi.ReqGetJobFromUrlAsync(pollingUrl, ct);
        }

        /// <summary>
        ///     Get a result directly from a URL.
        /// </summary>
        /// <param name="pollingUrl">The result's URL.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>
        ///     <see cref="ExtractionResponse" />
        /// </returns>
        public async Task<TResponse> GetResultFromUrlAsync<TResponse>(string pollingUrl, CancellationToken ct = default)
            where TResponse : BaseResponse, new()
        {
            _logger?.LogInformation("Getting result by URL: {}", pollingUrl);

            if (string.IsNullOrWhiteSpace(pollingUrl))
            {
                throw new MindeeInputException(nameof(pollingUrl));
            }
            return await _mindeeApi.ReqGetResultFromUrlAsync<TResponse>(pollingUrl, ct);
        }

        /// <summary>
        ///     Get the status of an inference that was previously enqueued.
        ///     Can be used for polling.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>
        ///     <see cref="ExtractionResponse" />
        /// </returns>
        public async Task<TResponse> GetResultAsync<TResponse>(string jobId, CancellationToken ct = default)
            where TResponse : BaseResponse, new()
        {
            _logger?.LogInformation("Getting result by ID: {}", jobId);

            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(jobId);
            }
            return await _mindeeApi.ReqGetResultAsync<TResponse>(jobId, ct);
        }

        /// <summary>
        ///     Get the status of an inference that was previously enqueued.
        ///     Can be used for polling.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>
        ///     <see cref="ExtractionResponse" />
        /// </returns>
        public async Task<JobResponse> GetJobAsync(string jobId, CancellationToken ct = default)
        {
            _logger?.LogInformation("Getting job ID: {}", jobId);

            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(jobId);
            }
            return await _mindeeApi.ReqGetJobAsync(jobId, ct);
        }

        /// <summary>
        ///     Add the document to an async queue, poll, and parse when complete.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        ///     <see cref="UrlInputSource" />
        /// </param>
        /// <param name="parameters">
        ///     <see cref="BaseParameters" />
        /// </param>
        /// <param name="pollingOptions">
        ///     <see cref="PollingOptions" />
        /// </param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>
        ///     <see cref="ExtractionResponse" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<TResponse> EnqueueAndGetResultAsync<TResponse>(
            InputSource inputSource
            , BaseParameters parameters
            , PollingOptions pollingOptions = null
            , CancellationToken ct = default)
            where TResponse : BaseResponse, new()
        {
            switch (inputSource)
            {
                case LocalInputSource:
                    _logger?.LogInformation("Enqueuing: local source");
                    break;
                case UrlInputSource:
                    _logger?.LogInformation("Enqueuing: URL source");
                    break;
                case null:
                    throw new ArgumentNullException(nameof(inputSource));
                default:
                    throw new MindeeInputException($"Unsupported input source {inputSource.GetType().Name}");
            }

            pollingOptions ??= new PollingOptions();

            var enqueueResponse = await EnqueueAsync(
                inputSource,
                parameters,
                ct);
            return await PollForResultsAsync<TResponse>(enqueueResponse, pollingOptions, ct);
        }

        /// <summary>
        /// Returns a list of models matching a criteria for the given API key.
        /// </summary>
        /// <param name="name">Name filter.</param>
        /// <param name="modelType">Model type filter.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns></returns>
        public async Task<SearchResponse> SearchModels(
            string name = null, string modelType = null, CancellationToken ct = default)
        {
            return await _mindeeApi.SearchModels(name, modelType, ct);
        }

        /// <summary>
        ///     Poll for results until the prediction is retrieved or the max amount of attempts is reached.
        /// </summary>
        /// <param name="enqueueResponse">
        ///     <see cref="JobResponse" />
        /// </param>
        /// <param name="pollingOptions">
        ///     <see cref="PollingOptions" />
        /// </param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>
        ///     <see cref="ExtractionResponse" />
        /// </returns>
        /// <exception cref="MindeeException">Thrown when maxRetries is reached and the result isn't ready.</exception>
        private async Task<TResponse> PollForResultsAsync<TResponse>(
            JobResponse enqueueResponse,
            PollingOptions pollingOptions,
            CancellationToken ct = default) where TResponse : BaseResponse, new()
        {
            var maxRetries = pollingOptions.MaxRetries + 1;
            var pollingUrl = enqueueResponse.Job.PollingUrl;
            _logger?.LogInformation("Enqueued with job ID: {}", enqueueResponse.Job.Id);
            _logger?.LogInformation(
                "Waiting {} seconds before attempting to retrieve the document...",
                pollingOptions.InitialDelaySec);
            await Task.Delay(pollingOptions.InitialDelayMilliSec, ct);
            var retryCount = 1;
            var response = enqueueResponse; // First init is only for error handling purposes.
            while (retryCount < maxRetries)
            {
                var retryDelayMilliSec = pollingOptions.GetRetryDelayMilliSec(retryCount);
                await Task.Delay(retryDelayMilliSec, ct);
                _logger?.LogInformation(
                    "Attempting to retrieve: {RetryCount} of {MaxRetries}",
                    retryCount,
                    maxRetries);

                response = await GetJobFromUrlAsync(pollingUrl, ct);
                if (response.Job.Error != null)
                {
                    break;
                }

                switch (response.Job.ParsedStatus)
                {
                    case JobStatus.Processed:
                        {
                            var resultUrl = response.Job.ResultUrl;
                            return await GetResultFromUrlAsync<TResponse>(resultUrl, ct);
                        }
                    case JobStatus.Failed:
                        throw new MindeeException("Job failed without an error payload.");
                }

                retryCount++;
            }

            var error = response.Job.Error;
            if (error != null)
            {
                throw new MindeeHttpExceptionV2(error);
            }

            throw new MindeeException($"Could not complete after {retryCount} attempts.");
        }
    }
}
