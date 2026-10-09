using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mindee.ClientOptions;
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
using Mindee.V2.Search.Models;
using SettingsV2 = Mindee.V2.Http.Settings;
// ReSharper disable once RedundantUsingDirective

namespace Mindee.V2
{
    /// <summary>
    ///     The entry point to use the Mindee V2 API features.
    /// </summary>
    public sealed class Client : BaseClient
    {
        private readonly HttpApiV2 _mindeeApi;

        /// <summary>
        /// </summary>
        /// <param name="apiKey">The required API key to use the Mindee V2 API.</param>
        /// <param name="loggerFactory">Factory for the logger.</param>
        public Client(string apiKey, ILoggerFactory loggerFactory = null) : base(loggerFactory)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMindeeApiV2(options =>
            {
                options.ApiKey = apiKey;
            }, LoggerFactory);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _mindeeApi = serviceProvider.GetRequiredService<MindeeApiV2>();
        }

        /// <summary>
        /// </summary>
        /// <param name="settings">
        ///     <see cref="SettingsV2" />
        /// </param>
        /// <param name="logger"></param>
        public Client(SettingsV2 settings, ILoggerFactory logger = null) : base(logger)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMindeeApiV2(options =>
            {
                options.ApiKey = settings.ApiKey;
                options.MindeeBaseUrl = settings.MindeeBaseUrl;
                options.RequestTimeoutSeconds = settings.RequestTimeoutSeconds;
            }, LoggerFactory);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _mindeeApi = serviceProvider.GetRequiredService<MindeeApiV2>();
        }

        /// <summary>
        /// </summary>
        /// <param name="httpApi">
        ///     <see cref="HttpApiV2" />
        /// </param>
        /// <param name="logger"></param>
        public Client(HttpApiV2 httpApi, ILoggerFactory logger = null) : base(logger)
        {
            _mindeeApi = httpApi;
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
                    Logger?.LogInformation("Enqueuing: local source");
                    break;
                case UrlInputSource:
                    Logger?.LogInformation("Enqueuing: URL source");
                    break;
                case null:
                    throw new ArgumentNullException(nameof(inputSource));
                default:
                    throw new MindeeInputException($"Unsupported input source {inputSource.GetType().Name}");
            }
            return await _mindeeApi.ReqPostProductEnqueueAsync(inputSource, parameters, ct);
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
            Logger?.LogInformation("Getting Job by URL: {JobURL}", pollingUrl);
            return await _mindeeApi.ReqGetJobByUrlAsync(pollingUrl, ct);
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
            Logger?.LogInformation("Getting result by URL: {ResultUrl}", resultUrl);
            return await _mindeeApi.ReqGetResultByUrlAsync<TResponse>(resultUrl, ct);
        }

        /// <summary>
        ///     Get the result of an inference that was previously enqueued by its ID.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>
        ///     <see cref="ExtractionResponse" />
        /// </returns>
        public async Task<TResponse> GetResultAsync<TResponse>(string jobId, CancellationToken ct = default)
            where TResponse : BaseResponse, new()
        {
            Logger?.LogInformation("Getting result with ID: {JobID}", jobId);

            if (string.IsNullOrWhiteSpace(jobId))
                throw new ArgumentNullException(jobId, "jobId must not be null or blank.");

            return await _mindeeApi.ReqGetResultByIdAsync<TResponse>(jobId, ct);
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
            Logger?.LogInformation("Getting job ID: {JobID}", jobId);

            if (string.IsNullOrWhiteSpace(jobId))
                throw new ArgumentNullException(jobId, "jobId must not be null or blank.");

            return await _mindeeApi.ReqGetJobByIdAsync(jobId, ct);
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
            Logger?.LogInformation(
                "Successfully enqueued document with job ID {JobID}", enqueueResponse.Job.Id);
            return await PollForProductResultsAsync<TResponse>(
                enqueueResponse, pollingOptions, false, ct);
        }

        /// <summary>
        /// Not recommended for general use, prefer <see cref="UploadAndGetRagDocumentPollAsync{TAnnotationResponse}"/>.
        /// You will need to poll until the document is ready for use.
        /// Add a document to the RAG database.
        /// </summary>
        /// <param name="parameters"><see cref="RagDocumentUploadParameters"/></param>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="ct"></param>
        public async Task<TAnnotationResponse> UploadRagDocumentAsync<TAnnotationResponse>(
            LocalInputSource inputSource
            , BaseRagDocumentUploadParameters<TAnnotationResponse> parameters
            , CancellationToken ct = default)
            where TAnnotationResponse : BaseRagAnnotationResponse, new()
        {
            Logger?.LogInformation("Adding a document to the RAG database");
            return await _mindeeApi.ReqPostRagDocumentAsync(parameters, inputSource, ct);
        }

        /// <summary>
        /// Add a document to the RAG database and return the initial annotation.
        /// </summary>
        /// <param name="parameters"><see cref="RagDocumentUploadParameters"/></param>
        /// <param name="inputSource"><see cref="LocalInputSource"/> The file to upload.</param>
        /// <param name="pollingOptions"><see cref="PollingOptions"/></param>
        /// <param name="ct"></param>
        public async Task<TAnnotationResponse> UploadAndGetRagDocumentPollAsync<TAnnotationResponse>(
            LocalInputSource inputSource
            , BaseRagDocumentUploadParameters<TAnnotationResponse> parameters
            , PollingOptions pollingOptions = null
            , CancellationToken ct = default)
            where TAnnotationResponse : BaseRagAnnotationResponse, new()
        {
            pollingOptions ??= new PollingOptions();

            var initialResponse = await UploadRagDocumentAsync(inputSource, parameters, ct);

            return await PollForRagDocumentAsync<TAnnotationResponse>(
                initialResponse, pollingOptions, ct);
        }

        /// <summary>
        /// Not recommended for general use, prefer <see cref="GetReadyRagDocumentPollAsync{TAnnotationResponse}"/>.
        /// You will need to poll until the document is ready for use.
        /// Get a document's info and annotations from the RAG database.
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="ct"></param>
        public async Task<TAnnotationResponse> GetRagDocumentAsync<TAnnotationResponse>(
            string documentId, CancellationToken ct = default)
            where TAnnotationResponse : BaseRagAnnotationResponse, new()
        {
            Logger?.LogInformation("Getting RAG document ID: {DocumentId}", documentId);
            return await _mindeeApi.ReqGetRagAnnotationAsync<TAnnotationResponse>(documentId, ct);
        }

        /// <summary>
        /// Get a document's info and annotations from the RAG database.
        /// </summary>
        /// <param name="documentId">The document's ID.</param>
        /// <param name="pollingOptions"/>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<TAnnotationResponse> GetReadyRagDocumentPollAsync<TAnnotationResponse>(
            string documentId
            , PollingOptions pollingOptions = null
            , CancellationToken ct = default)
            where TAnnotationResponse : BaseRagAnnotationResponse, new()
        {
            var initialResponse = await GetRagDocumentAsync<TAnnotationResponse>(documentId, ct);
            if (initialResponse.Status != "Processing")
                return initialResponse;

            pollingOptions ??= new PollingOptions();
            return await PollForRagDocumentAsync<TAnnotationResponse>(initialResponse, pollingOptions, ct);
        }

        /// <summary>
        /// Not recommended for general use, prefer <see cref="UpdateAndGetRagAnnotationPollAsync{TAnnotationResponse}"/>.
        /// You will need to poll until the document is ready for use.
        /// Update a document's annotations in the RAG database.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<TAnnotationResponse> UpdateRagAnnotationAsync<TAnnotationResponse>(
            BaseAnnotationParameters<TAnnotationResponse> parameters, CancellationToken ct = default)
            where TAnnotationResponse : BaseRagAnnotationResponse, new()
        {
            Logger?.LogInformation("Updating RAG document ID: {DocumentId}", parameters.DocumentId);
            return await _mindeeApi.ReqPatchRagAnnotationAsync(parameters, ct);
        }

        /// <summary>
        /// Update a document's annotations in the RAG database.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="pollingOptions"/>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<TAnnotationResponse> UpdateAndGetRagAnnotationPollAsync<TAnnotationResponse>(
            BaseAnnotationParameters<TAnnotationResponse> parameters
            , PollingOptions pollingOptions = null
            , CancellationToken ct = default)
            where TAnnotationResponse : ExtractionRagAnnotationResponse, new()
        {
            var initialResponse = await UpdateRagAnnotationAsync(parameters, ct);
            if (initialResponse.Status != "Processing")
                return initialResponse;

            pollingOptions ??= new PollingOptions();
            return await PollForRagDocumentAsync<TAnnotationResponse>(
                initialResponse, pollingOptions, ct);
        }

        /// <summary>
        /// Delete a document from the RAG database.
        /// For extraction models only.
        /// </summary>
        /// <param name="documentId">The document's ID.</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> DeleteExtractionRagDocumentAsync(
            string documentId, CancellationToken ct = default)
        {
            Logger?.LogInformation("Deleting RAG document ID: {DocumentId}", documentId);
            return await _mindeeApi.ReqDeleteExtractionRagDocumentAsync(documentId, ct);
        }

        /// <summary>
        /// Search for resources matching the given criteria.
        /// </summary>
        /// <param name="searchParameters">Search parameters</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A search response containing the matching resources.</returns>
        public async Task<TSearchResponse> SearchAsync<TSearchResponse>(
            BaseSearchParameters<TSearchResponse> searchParameters, CancellationToken ct = default)
            where TSearchResponse : BaseSearchResponse, new()
        {
            if (searchParameters == null)
                throw new ArgumentNullException(nameof(searchParameters));

            return await _mindeeApi.ReqGetSearchAsync(searchParameters, ct);
        }

        /// <summary>
        /// Returns a list of models matching a criteria for the given API key.
        /// </summary>
        /// <param name="name">Name filter.</param>
        /// <param name="modelType">Model type filter.</param>
        /// <param name="ct">Cancellation token.</param>
        [Obsolete("Use SearchAsync(ModelSearchParameters parameters)")]
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
        private async Task<TAnnotationResponse> PollForRagDocumentAsync<TAnnotationResponse>(
            BaseRagAnnotationResponse initialResponse
            , PollingOptions pollingOptions
            , CancellationToken cancellationToken = default)
        where TAnnotationResponse : BaseRagAnnotationResponse, new()
        {
            Logger?.LogInformation("Polling for RAG document ID: {DocumentId}", initialResponse.Id);

            Logger?.LogDebug(
                "Waiting {InitialDelaySec} seconds before attempting to retrieve the result...",
                pollingOptions.InitialDelaySec);

            await Task.Delay(pollingOptions.InitialDelayMilliSec, cancellationToken);

            var tryCounter = 0;
            var maxRetries = pollingOptions.MaxRetries;
            var documentId = initialResponse.Id;

            while (tryCounter < maxRetries)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Logger?.LogDebug(
                    "Poll attempt {RetryCount} of {MaxRetries}",
                    tryCounter + 1,
                    maxRetries);

                var response = await GetRagDocumentAsync<TAnnotationResponse>(documentId, cancellationToken);

                tryCounter++;
                switch (response.Status)
                {
                    case "Processing":
                        await ThrottlePollingAttemptAsync(tryCounter, maxRetries, pollingOptions, cancellationToken);
                        break;
                    case "Failed":
                        throw new MindeeException("RAG failed without an error payload.");
                    default:
                        return response;
                }
            }
            throw new MindeeException($"RAG polling not complete after {tryCounter} attempts.");
        }

        /// <summary>
        /// Checks if all webhooks associated with a job have finished processing.
        /// </summary>
        private bool CheckWebhooksDone(JobResponse jobResponse)
        {
            var areWebhooksDone = jobResponse.Job.Webhooks == null ||
                                  jobResponse.Job.Webhooks.All(w => w.Status != "Processing");

            if (areWebhooksDone)
            {
                Logger?.LogDebug("All webhooks are completed.");
                return true;
            }

            Logger?.LogDebug("Not all webhooks are completed.");
            return false;
        }

        /// <summary>
        /// Polls a job until it is processed or the maximum number of tries is reached.
        /// </summary>
        private async Task<JobResponse> PollOnJobAsync(
            JobResponse initialResponse,
            PollingOptions pollingOptions,
            bool waitForWebhooks = false,
            CancellationToken cancellationToken = default)
        {
            Logger?.LogDebug(
                "Waiting {InitialDelaySec} seconds before attempting to retrieve the result...",
                pollingOptions.InitialDelaySec);

            await Task.Delay(pollingOptions.InitialDelayMilliSec, cancellationToken);
            var tryCounter = 0;
            var maxRetries = pollingOptions.MaxRetries;
            var pollingUrl = initialResponse.Job.PollingUrl;

            while (tryCounter < maxRetries)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Logger?.LogDebug(
                    "Poll attempt {RetryCount} of {MaxRetries}",
                    tryCounter + 1,
                    maxRetries);

                var jobResponse = await GetJobFromUrlAsync(pollingUrl, cancellationToken);

                if (jobResponse.Job.Status == "Processed")
                {
                    Logger?.LogDebug(
                        "Job ID {JobID} completed processing at: {CompletedAt}",
                        jobResponse.Job.Id,
                        jobResponse.Job.CompletedAt);

                    if (!waitForWebhooks || CheckWebhooksDone(jobResponse))
                    {
                        return jobResponse;
                    }
                }

                // normally the API handler will throw an error, this is a fallback
                if (jobResponse.Job.Status == "Failed")
                {
                    if (jobResponse.Job.Error != null)
                    {
                        throw new MindeeHttpExceptionV2(jobResponse.Job.Error);
                    }
                    throw new MindeeException($"Parsing failed for job {jobResponse.Job.Id}: No error detail available.");
                }

                tryCounter++;
                await ThrottlePollingAttemptAsync(tryCounter, maxRetries, pollingOptions, cancellationToken);
            }

            throw new MindeeException($"Couldn't retrieve the result after {tryCounter} tries.");
        }

        /// <summary>
        /// Poll until the inference results are retrieved or the max number of attempts is reached.
        /// </summary>
        private async Task<TResponse> PollForProductResultsAsync<TResponse>(
            JobResponse enqueueResponse,
            PollingOptions pollingOptions,
            bool waitForWebhooks = false,
            CancellationToken cancellationToken = default)
            where TResponse : BaseResponse, new()
        {
            var jobResponse = await PollOnJobAsync(
                enqueueResponse,
                pollingOptions,
                waitForWebhooks,
                cancellationToken);

            if (string.IsNullOrWhiteSpace(jobResponse.Job.ResultUrl))
            {
                throw new MindeeException(
                    "The result URL is undefined. This is a server error, try again later or contact support.");
            }

            return await GetResultFromUrlAsync<TResponse>(jobResponse.Job.ResultUrl, cancellationToken);
        }
    }
}
