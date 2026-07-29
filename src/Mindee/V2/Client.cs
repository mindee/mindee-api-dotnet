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
using Mindee.V2.Product.Extraction.RagDocuments;
using Mindee.V2.Product.Extraction.RagDocuments.Params;
using Mindee.V2.Search.Model;
using Mindee.V2.Search.Models;
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
            , BaseProductParameters parameters
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
            _logger?.LogInformation("Getting Job at: {}", pollingUrl);

            if (string.IsNullOrWhiteSpace(pollingUrl))
            {
                throw new ArgumentNullException(pollingUrl);
            }

            return await _mindeeApi.ReqGetJobFromUrlAsync(pollingUrl, ct);
        }

        /// <summary>
        ///     Get a result directly from a polling URL.
        /// </summary>
        /// <param name="resultUrl">The result's URL.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>
        ///     <see cref="ExtractionResponse" />
        /// </returns>
        public async Task<TResponse> GetResultFromUrlAsync<TResponse>(string resultUrl, CancellationToken ct = default)
            where TResponse : BaseResponse, new()
        {
            _logger?.LogInformation("Getting result at: {}", resultUrl);

            if (string.IsNullOrWhiteSpace(resultUrl))
            {
                throw new MindeeInputException(nameof(resultUrl));
            }
            return await _mindeeApi.ReqGetResultFromUrlAsync<TResponse>(resultUrl, ct);
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
            _logger?.LogInformation("Getting result with ID: {}", jobId);

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
        ///     <see cref="BaseProductParameters" />
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
            , BaseProductParameters parameters
            , PollingOptions pollingOptions = null
            , CancellationToken ct = default)
            where TResponse : BaseResponse, new()
        {
            pollingOptions ??= new PollingOptions();

            var enqueueResponse = await EnqueueAsync(
                inputSource,
                parameters,
                ct);
            return await PollForProductResultsAsync<TResponse>(enqueueResponse, pollingOptions, ct);
        }

        /// <summary>
        /// Returns a list of models matching the given criteria.
        /// </summary>
        /// <param name="searchParameters"><see cref="ModelSearchParameters"/></param>
        /// <param name="ct">Cancellation token.</param>
        public async Task<ModelSearchResponse> SearchModelsAsync(
            ModelSearchParameters searchParameters, CancellationToken ct = default)
        {
            _logger?.LogInformation("Searching for models");
            var parameters = searchParameters ?? new ModelSearchParameters();
            return await _mindeeApi.ReqGetSearchModelsAsync(parameters, ct);
        }

        /// <summary>
        /// Not recommended for general use, prefer <see cref="UploadAndGetExtractionRagDocumentAsync"/>.
        /// You will need to poll until the document is ready for use.
        /// Add a document to the RAG database.
        /// For extraction models only.
        /// </summary>
        /// <param name="parameters"><see cref="RagDocumentUploadParameters"/></param>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<RagAnnotationResponse> UploadExtractionRagDocumentAsync(
            LocalInputSource inputSource, RagDocumentUploadParameters parameters, CancellationToken ct = default)
        {
            _logger?.LogInformation("Adding a document to the RAG database");
            return await _mindeeApi.ReqPostExtractionRagDocumentAsync(parameters, inputSource, ct);
        }

        /// <summary>
        /// Add a document to the RAG database and return the initial annotation.
        /// For extraction models only.
        /// </summary>
        /// <param name="parameters"><see cref="RagDocumentUploadParameters"/></param>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="pollingOptions"><see cref="PollingOptions"/></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<RagAnnotationResponse> UploadAndGetExtractionRagDocumentAsync(
            LocalInputSource inputSource
            , RagDocumentUploadParameters parameters
            , PollingOptions pollingOptions = null
            , CancellationToken ct = default)
        {
            pollingOptions ??= new PollingOptions();

            var initialResponse = await UploadExtractionRagDocumentAsync(
                inputSource, parameters, ct);

            return await PollForExtractionRagDocumentAsync(initialResponse, pollingOptions, ct);
        }

        /// <summary>
        /// Not recommended for general use, prefer <see cref="GetReadyExtractionRagDocumentAsync"/>.
        /// You will need to poll until the document is ready for use.
        /// Get a document's info and annotations from the RAG database.
        /// For extraction models only.
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<RagAnnotationResponse> GetExtractionRagDocumentAsync(
            string documentId, CancellationToken ct = default)
        {
            _logger?.LogInformation("Getting RAG document ID: {}", documentId);
            return await _mindeeApi.ReqGetExtractionRagAnnotationAsync(documentId, ct);
        }

        /// <summary>
        /// Get a document's info and annotations from the RAG database.
        /// For extraction models only.
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="pollingOptions"/>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<RagAnnotationResponse> GetReadyExtractionRagDocumentAsync(
            string documentId
            , PollingOptions pollingOptions = null
            , CancellationToken ct = default)
        {
            var initialResponse = await GetExtractionRagDocumentAsync(documentId, ct);
            if (initialResponse.Status != "Processing")
                return initialResponse;

            pollingOptions ??= new PollingOptions();
            return await PollForExtractionRagDocumentAsync(initialResponse, pollingOptions, ct);
        }

        /// <summary>
        /// Update a document's annotations in the RAG database.
        /// For extraction models only.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<RagAnnotationResponse> UpdateExtractionRagAnnotationAsync(
            RagDocumentAnnotationParameters parameters, CancellationToken ct = default)
        {
            _logger?.LogInformation("Updating RAG document ID: {}", parameters.DocumentId);
            return await _mindeeApi.ReqPatchExtractionRagAnnotationAsync(parameters, ct);
        }

        /// <summary>
        /// Get a document's info and annotations from the RAG database.
        /// For extraction models only.
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> DeleteExtractionRagDocumentAsync(
            string documentId, CancellationToken ct = default)
        {
            _logger?.LogInformation("Deleting RAG document ID: {}", documentId);
            return await _mindeeApi.ReqDeleteExtractionRagDocumentAsync(documentId, ct);
        }

        /// <summary>
        /// Returns a list of RAG documents matching the given criteria.
        /// </summary>
        /// <param name="searchParameters"><see cref="RagDocumentSearchResponse"/></param>
        /// <param name="ct">Cancellation token.</param>
        public async Task<RagDocumentSearchResponse> SearchRagDocumentsAsync(
            RagDocumentSearchParameters searchParameters, CancellationToken ct = default)
        {
            _logger?.LogInformation("Searching for RAG documents");
            return await _mindeeApi.ReqGetSearchRagDocumentsAsync(searchParameters, ct);
        }

        /// <summary>
        /// Returns a list of models matching a criteria for the given API key.
        /// </summary>
        /// <param name="name">Name filter.</param>
        /// <param name="modelType">Model type filter.</param>
        /// <param name="ct">Cancellation token.</param>
        [Obsolete("Use SearchModelsAsync(ModelSearchParameters parameters)")]
        public async Task<SearchResponse> SearchModels(
            string name = null, string modelType = null, CancellationToken ct = default)
        {
            return await _mindeeApi.SearchModelsObsolete(
                new ModelSearchParameters(name, modelType), ct);
        }

        /// <summary>
        /// Poll until the document is finished processing or the max number of attempts is reached.
        /// </summary>
        /// <exception cref="MindeeException">Thrown when maxRetries is reached and the annotation isn't ready.</exception>
        private async Task<RagAnnotationResponse> PollForExtractionRagDocumentAsync(
            RagAnnotationResponse initialResponse
            , PollingOptions pollingOptions
            , CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("Polling for RAG document ID: {}", initialResponse.Id);
            var maxRetries = pollingOptions.MaxRetries + 1;

            _logger?.LogDebug(
                "Waiting {} seconds before attempting to retrieve the result...",
                pollingOptions.InitialDelaySec);
            await Task.Delay(pollingOptions.InitialDelayMilliSec, cancellationToken);
            var documentId = initialResponse.Id;

            var retryCount = 1;
            while (retryCount < maxRetries)
            {
                var retryDelayMilliSec = pollingOptions.GetRetryDelayMilliSec(retryCount);
                await Task.Delay(retryDelayMilliSec, cancellationToken);
                _logger?.LogInformation(
                    "Poll attempt {RetryCount} of {MaxRetries}",
                    retryCount,
                    maxRetries);

                var response = await GetExtractionRagDocumentAsync(documentId, cancellationToken);

                retryCount++;
                switch (response.Status)
                {
                    case "Processing":
                        continue;
                    case "Failed":
                        throw new MindeeException("Job failed without an error payload.");
                    default:
                        return response;
                }
            }
            throw new MindeeException($"RAG polling not complete after {retryCount} attempts.");
        }

        /// <summary>
        /// Poll until the inference results are retrieved or the max number of attempts is reached.
        /// </summary>
        /// <exception cref="MindeeException">Thrown when maxRetries is reached and the result isn't ready.</exception>
        private async Task<TResponse> PollForProductResultsAsync<TResponse>(
            JobResponse enqueueResponse,
            PollingOptions pollingOptions,
            CancellationToken cancellationToken = default)
            where TResponse : BaseResponse, new()
        {
            _logger?.LogInformation("Polling for results on job ID: {}", enqueueResponse.Job.Id);
            var maxRetries = pollingOptions.MaxRetries + 1;
            var pollingUrl = enqueueResponse.Job.PollingUrl;
            _logger?.LogDebug(
                "Waiting {} seconds before attempting to retrieve the result...",
                pollingOptions.InitialDelaySec);
            await Task.Delay(pollingOptions.InitialDelayMilliSec, cancellationToken);
            var retryCount = 1;
            var response = enqueueResponse; // First init is only for error handling purposes.
            while (retryCount < maxRetries)
            {
                var retryDelayMilliSec = pollingOptions.GetRetryDelayMilliSec(retryCount);
                await Task.Delay(retryDelayMilliSec, cancellationToken);
                _logger?.LogInformation(
                    "Poll attempt {RetryCount} of {MaxRetries}",
                    retryCount,
                    maxRetries);

                response = await GetJobFromUrlAsync(pollingUrl, cancellationToken);
                if (response.Job.Error != null)
                {
                    break;
                }

                switch (response.Job.Status)
                {
                    case "Processed":
                        {
                            var resultUrl = response.Job.ResultUrl;
                            return await GetResultFromUrlAsync<TResponse>(resultUrl, cancellationToken);
                        }
                    case "Failed":
                        throw new MindeeException("Job failed without an error payload.");
                }

                retryCount++;
            }

            var error = response.Job.Error;
            if (error != null)
            {
                throw new MindeeHttpExceptionV2(error);
            }

            throw new MindeeException($"Result polling not complete after {retryCount} attempts.");
        }
    }
}
