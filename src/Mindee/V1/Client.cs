using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mindee.ClientOptions;
using Mindee.Exceptions;
using Mindee.Extensions.DependencyInjection;
using Mindee.Input;
using Mindee.Pdf;
using Mindee.V1.ClientOptions;
using Mindee.V1.Http;
using Mindee.V1.Parsing;
using Mindee.V1.Parsing.Common;
using Mindee.V1.Product.Generated;
// ReSharper disable once RedundantUsingDirective

namespace Mindee.V1
{
    /// <summary>
    ///     The entry point to use the Mindee API legacy features.
    /// </summary>
    public sealed class Client : BaseClient
    {
        private readonly IHttpApi _mindeeApi;
        private readonly IPdfOperation _pdfOperation;

        /// <summary>
        /// </summary>
        /// <param name="apiKey">The required API key to use Mindee.</param>
        /// <param name="logger"></param>
        public Client(string apiKey, ILoggerFactory logger = null) : base(logger)
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
                // legacy static logging only used in v1
                MindeeLogger.Assign(logger);
            }

            _mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();
        }

        /// <summary>
        /// </summary>
        /// <param name="settings">
        ///     <see cref="Settings" />
        /// </param>
        /// <param name="logger"></param>
        public Client(Settings settings, ILoggerFactory logger = null) : base(logger)
        {
            var serviceCollection = new ServiceCollection();
            _pdfOperation = new DocNetApi();

            serviceCollection.AddMindeeApi(options =>
            {
                options.ApiKey = settings.ApiKey;
                options.MindeeBaseUrl = settings.MindeeBaseUrl;
                options.RequestTimeoutSeconds = settings.RequestTimeoutSeconds;
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            if (logger != null)
            {
                // legacy static logging only used in v1
                MindeeLogger.Assign(logger);
            }

            _mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();
        }

        /// <summary>
        /// </summary>
        /// <param name="pdfOperation">
        ///     <see cref="IPdfOperation" />
        /// </param>
        /// <param name="httpApi">
        ///     <see cref="IHttpApi" />
        /// </param>
        /// <param name="logger"></param>
        public Client(IPdfOperation pdfOperation, IHttpApi httpApi, ILoggerFactory logger = null) : base(logger)
        {
            _pdfOperation = pdfOperation;
            _mindeeApi = httpApi;
            if (logger != null)
            {
                // legacy static logging only used in v1
                MindeeLogger.Assign(logger);
            }
        }

        /// <summary>
        ///     Call Generated prediction API on a local input source and parse the results.
        /// </summary>
        /// <param name="endpoint">
        ///     <see cref="CustomEndpoint" />
        /// </param>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <param name="pageOptions">
        ///     <see cref="PageOptions" />
        /// </param>
        /// <returns>
        ///     <see cref="PredictResponse{GeneratedV1}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<PredictResponse<GeneratedV1>> ParseAsync<TInferenceModel>(
            LocalInputSource inputSource
            , CustomEndpoint endpoint
            , PredictOptions predictOptions = null
            , PageOptions pageOptions = null)
            where TInferenceModel : GeneratedV1, new()
        {
            Logger?.LogInformation("Synchronous parsing of {} ...", nameof(TInferenceModel));

            if (predictOptions == null)
            {
                predictOptions = new PredictOptions();
            }

            if (pageOptions != null && inputSource.IsPdf())
            {
                inputSource.FileBytes = _pdfOperation.Split(
                    new SplitQuery(inputSource.FileBytes, pageOptions)).File;
            }

            return await _mindeeApi.PredictPostAsync<GeneratedV1>(
                new PredictParameter(
                    inputSource,
                    null,
                    predictOptions.AllWords,
                    predictOptions.FullText,
                    predictOptions.Cropper,
                    predictOptions.WorkflowId,
                    predictOptions.Rag)
                , endpoint);
        }

        /// <summary>
        ///     Call Generated prediction API on a URL input source and parse the results.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="UrlInputSource" />
        /// </param>
        /// <param name="endpoint">
        ///     <see cref="CustomEndpoint" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="PredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<PredictResponse<TInferenceModel>> ParseAsync<TInferenceModel>(
            UrlInputSource inputSource
            , CustomEndpoint endpoint
            , PredictOptions predictOptions = null)
            where TInferenceModel : GeneratedV1, new()
        {
            Logger?.LogInformation("Synchronous parsing of {} ...", typeof(TInferenceModel).Name);

            if (predictOptions == null)
            {
                predictOptions = new PredictOptions();
            }

            return await _mindeeApi.PredictPostAsync<TInferenceModel>(
                new PredictParameter(
                    null,
                    inputSource,
                    predictOptions.AllWords,
                    predictOptions.FullText,
                    predictOptions.Cropper,
                    predictOptions.WorkflowId,
                    predictOptions.Rag)
                , endpoint);
        }

        /// <summary>
        ///     Add a local input source to a Generated async queue.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="endpoint">
        ///     <see cref="CustomEndpoint" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <param name="pageOptions">
        ///     <see cref="PageOptions" />
        /// </param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponse<TInferenceModel>> EnqueueAsync<TInferenceModel>(
            LocalInputSource inputSource
            , CustomEndpoint endpoint
            , PredictOptions predictOptions = null
            , PageOptions pageOptions = null)
            where TInferenceModel : GeneratedV1, new()
        {
            Logger?.LogInformation("Enqueuing of {} ...", typeof(TInferenceModel).Name);

            if (predictOptions == null)
            {
                predictOptions = new PredictOptions();
            }

            if (pageOptions != null && inputSource.IsPdf())
            {
                inputSource.FileBytes = _pdfOperation.Split(
                    new SplitQuery(inputSource.FileBytes, pageOptions)).File;
            }

            return await _mindeeApi.PredictAsyncPostAsync<TInferenceModel>(
                new PredictParameter(
                    inputSource,
                    null,
                    predictOptions.AllWords,
                    predictOptions.FullText,
                    predictOptions.Cropper,
                    predictOptions.WorkflowId,
                    predictOptions.Rag
                )
                , endpoint);
        }

        /// <summary>
        ///     Add a URL input source to an async queue.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="endpoint">
        ///     <see cref="CustomEndpoint" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponse<TInferenceModel>> EnqueueAsync<TInferenceModel>(
            UrlInputSource inputSource
            , CustomEndpoint endpoint
            , PredictOptions predictOptions = null)
            where TInferenceModel : GeneratedV1, new()
        {
            Logger?.LogInformation("Enqueuing of {} ...", typeof(TInferenceModel).Name);

            if (predictOptions == null)
            {
                predictOptions = new PredictOptions();
            }

            return await _mindeeApi.PredictAsyncPostAsync<TInferenceModel>(
                new PredictParameter(
                    null,
                    inputSource,
                    predictOptions.AllWords,
                    predictOptions.FullText,
                    predictOptions.Cropper,
                    predictOptions.WorkflowId,
                    predictOptions.Rag)
                , endpoint);
        }

        /// <summary>
        ///     Parse a document from a Generated async queue.
        /// </summary>
        /// <param name="endpoint">
        ///     <see cref="CustomEndpoint" />
        /// </param>
        /// <param name="jobId">The job id.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        public async Task<AsyncPredictResponse<TInferenceModel>> ParseQueuedAsync<TInferenceModel>(
            CustomEndpoint endpoint
            , string jobId
            , CancellationToken ct = default)
            where TInferenceModel : GeneratedV1, new()
        {
            Logger?.LogInformation("Parse from queue of {} ...", typeof(TInferenceModel).Name);

            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(jobId);
            }

            return await _mindeeApi.DocumentQueueGetAsync<TInferenceModel>(jobId, endpoint, ct);
        }

        /// <summary>
        ///     Add the document to an async queue, poll, and parse when complete.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="endpoint">
        ///     <see cref="CustomEndpoint" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <param name="pageOptions">
        ///     <see cref="PageOptions" />
        /// </param>
        /// <param name="pollingOptions">
        ///     <see cref="AsyncPollingOptions" />
        /// </param>
        /// <param name="ct">Cancellation token.</param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponse<TInferenceModel>> EnqueueAndParseAsync<TInferenceModel>(
            LocalInputSource inputSource
            , CustomEndpoint endpoint
            , PredictOptions predictOptions = null
            , PageOptions pageOptions = null
            , AsyncPollingOptions pollingOptions = null
            , CancellationToken ct = default)
            where TInferenceModel : GeneratedV1, new()
        {
            Logger?.LogInformation("Asynchronous parsing of {} ...", typeof(TInferenceModel).Name);

            if (pollingOptions == null)
            {
                pollingOptions = new AsyncPollingOptions();
            }

            var enqueueResponse = await EnqueueAsync<TInferenceModel>(
                inputSource,
                endpoint,
                predictOptions,
                pageOptions);

            return await PollForResultsAsync(enqueueResponse, endpoint, pollingOptions, ct);
        }


        /// <summary>
        ///     Add the document to an async queue, poll, and parse when complete. URL input version.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="UrlInputSource" />
        /// </param>
        /// <param name="endpoint">
        ///     <see cref="CustomEndpoint" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <param name="pollingOptions">
        ///     <see cref="AsyncPollingOptions" />
        /// </param>
        /// <param name="ct">Cancellation token.</param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponse<TInferenceModel>> EnqueueAndParseAsync<TInferenceModel>(
            UrlInputSource inputSource
            , CustomEndpoint endpoint
            , PredictOptions predictOptions = null
            , AsyncPollingOptions pollingOptions = null
            , CancellationToken ct = default)
            where TInferenceModel : GeneratedV1, new()
        {
            Logger?.LogInformation("Asynchronous parsing of {} ...", typeof(TInferenceModel).Name);

            if (pollingOptions == null)
            {
                pollingOptions = new AsyncPollingOptions();
            }

            var enqueueResponse = await EnqueueAsync<TInferenceModel>(
                inputSource,
                endpoint,
                predictOptions);

            return await PollForResultsAsync(enqueueResponse, endpoint, pollingOptions, ct);
        }

        /// <summary>
        ///     Call Standard prediction API on a local input source and parse the results.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <param name="pageOptions">
        ///     <see cref="PageOptions" />
        /// </param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="PredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<PredictResponse<TInferenceModel>> ParseAsync<TInferenceModel>(
            LocalInputSource inputSource
            , PredictOptions predictOptions = null
            , PageOptions pageOptions = null)
            where TInferenceModel : class, new()
        {
            Logger?.LogInformation("Synchronous parsing of {} ...", typeof(TInferenceModel).Name);

            if (predictOptions == null)
            {
                predictOptions = new PredictOptions();
            }

            if (pageOptions != null && inputSource.IsPdf())
            {
                var splitPdf = _pdfOperation.Split(new SplitQuery(inputSource.FileBytes, pageOptions));
                inputSource.FileBytes = splitPdf.File;
            }

            return await _mindeeApi.PredictPostAsync<TInferenceModel>(
                new PredictParameter(
                    inputSource,
                    null,
                    predictOptions.AllWords,
                    predictOptions.FullText,
                    predictOptions.Cropper,
                    predictOptions.WorkflowId,
                    predictOptions.Rag));
        }

        /// <summary>
        ///     Call Standard prediction API on a URL input source and parse the results.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="PredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<PredictResponse<TInferenceModel>> ParseAsync<TInferenceModel>(
            UrlInputSource inputSource
            , PredictOptions predictOptions = null)
            where TInferenceModel : class, new()
        {
            Logger?.LogInformation("Synchronous parsing of {} ...", typeof(TInferenceModel).Name);

            if (predictOptions == null)
            {
                predictOptions = new PredictOptions();
            }

            return await _mindeeApi.PredictPostAsync<TInferenceModel>(
                new PredictParameter(
                    null,
                    inputSource,
                    predictOptions.AllWords,
                    predictOptions.FullText,
                    predictOptions.Cropper,
                    predictOptions.WorkflowId,
                    predictOptions.Rag));
        }

        /// <summary>
        ///     Add a local input source to a Standard async queue.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <param name="pageOptions">
        ///     <see cref="PageOptions" />
        /// </param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponse<TInferenceModel>> EnqueueAsync<TInferenceModel>(
            LocalInputSource inputSource
            , PredictOptions predictOptions = null
            , PageOptions pageOptions = null)
            where TInferenceModel : class, new()
        {
            Logger?.LogInformation("Enqueuing of {} ...", typeof(TInferenceModel).Name);

            if (predictOptions == null)
            {
                predictOptions = new PredictOptions();
            }

            if (pageOptions != null && inputSource.IsPdf())
            {
                inputSource.FileBytes = _pdfOperation.Split(
                    new SplitQuery(inputSource.FileBytes, pageOptions)).File;
            }

            return await _mindeeApi.PredictAsyncPostAsync<TInferenceModel>(
                new PredictParameter(
                    inputSource,
                    null,
                    predictOptions.AllWords,
                    predictOptions.FullText,
                    predictOptions.Cropper,
                    predictOptions.WorkflowId,
                    predictOptions.Rag));
        }

        /// <summary>
        ///     Add a URL input source to an async queue.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponse<TInferenceModel>> EnqueueAsync<TInferenceModel>(
            UrlInputSource inputSource
            , PredictOptions predictOptions = null)
            where TInferenceModel : class, new()
        {
            Logger?.LogInformation("Enqueuing of {} ...", typeof(TInferenceModel).Name);

            if (predictOptions == null)
            {
                predictOptions = new PredictOptions();
            }

            return await _mindeeApi.PredictAsyncPostAsync<TInferenceModel>(
                new PredictParameter(
                    null,
                    inputSource,
                    predictOptions.AllWords,
                    predictOptions.FullText,
                    predictOptions.Cropper,
                    predictOptions.WorkflowId,
                    predictOptions.Rag));
        }

        /// <summary>
        ///     Parse a document from an async queue.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        public async Task<AsyncPredictResponse<TInferenceModel>> ParseQueuedAsync<TInferenceModel>(
            string jobId,
            CancellationToken ct = default)
            where TInferenceModel : class, new()
        {
            Logger?.LogInformation("Parse from queue of {} ...", typeof(TInferenceModel).Name);

            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(jobId);
            }

            return await _mindeeApi.DocumentQueueGetAsync<TInferenceModel>(jobId, null, ct);
        }

        /// <summary>
        ///     Add the document to an async queue, poll, and parse when complete.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <param name="pageOptions">
        ///     <see cref="PageOptions" />
        /// </param>
        /// <param name="pollingOptions">
        ///     <see cref="AsyncPollingOptions" />
        /// </param>
        /// <param name="ct">Cancellation token.</param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponse<TInferenceModel>> EnqueueAndParseAsync<TInferenceModel>(
            LocalInputSource inputSource
            , PredictOptions predictOptions = null
            , PageOptions pageOptions = null
            , AsyncPollingOptions pollingOptions = null
            , CancellationToken ct = default)
            where TInferenceModel : class, new()
        {
            Logger?.LogInformation("Asynchronous parsing of {} ...", typeof(TInferenceModel).Name);

            if (pollingOptions == null)
            {
                pollingOptions = new AsyncPollingOptions();
            }

            var enqueueResponse = await EnqueueAsync<TInferenceModel>(
                inputSource,
                predictOptions,
                pageOptions);

            return await PollForResultsAsync(enqueueResponse, pollingOptions, ct);
        }


        /// <summary>
        ///     Add the document to an async queue, poll, and parse when complete.
        /// </summary>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="predictOptions">
        ///     <see cref="PredictOptions" />
        /// </param>
        /// <param name="pollingOptions">
        ///     <see cref="AsyncPollingOptions" />
        /// </param>
        /// <param name="ct">Cancellation token.</param>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<AsyncPredictResponse<TInferenceModel>> EnqueueAndParseAsync<TInferenceModel>(
            UrlInputSource inputSource
            , PredictOptions predictOptions = null
            , AsyncPollingOptions pollingOptions = null
            , CancellationToken ct = default)
            where TInferenceModel : class, new()
        {
            Logger?.LogInformation("Asynchronous parsing of {} ...", typeof(TInferenceModel).Name);

            if (pollingOptions == null)
            {
                pollingOptions = new AsyncPollingOptions();
            }

            var enqueueResponse = await EnqueueAsync<TInferenceModel>(
                inputSource,
                predictOptions);

            return await PollForResultsAsync(enqueueResponse, pollingOptions, ct);
        }

        /// <summary>
        ///     Send a local file to a workflow execution.
        /// </summary>
        /// <param name="workflowId">The workflow id.</param>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="workflowOptions">
        ///     <see cref="WorkflowOptions" />
        /// </param>
        /// <param name="pageOptions">
        ///     <see cref="PageOptions" />
        /// </param>
        /// <returns>
        ///     <see cref="WorkflowResponse{TInferenceModel}" />
        /// </returns>
        public async Task<WorkflowResponse<GeneratedV1>> ExecuteWorkflowAsync(
            string workflowId,
            LocalInputSource inputSource,
            WorkflowOptions workflowOptions = null,
            PageOptions pageOptions = null)
        {
            Logger?.LogInformation("Sending '{Filename}' to workflow '{WorkflowId}'...", inputSource.Filename, workflowId);

            if (pageOptions != null && inputSource.IsPdf())
            {
                inputSource.FileBytes = _pdfOperation.Split(
                    new SplitQuery(inputSource.FileBytes, pageOptions)).File;
            }

            workflowOptions ??= new WorkflowOptions();

            return await _mindeeApi.PostWorkflowExecution<GeneratedV1>(
                workflowId,
                new WorkflowParameter(
                    inputSource,
                    null,
                    workflowOptions.FullText,
                    workflowOptions.Alias,
                    workflowOptions.Priority,
                    workflowOptions.PublicUrl,
                    workflowOptions.Rag
                ));
        }

        /// <summary>
        ///     Send a remote file to a workflow execution.
        /// </summary>
        /// <param name="workflowId">The workflow id.</param>
        /// <param name="inputSource">
        ///     <see cref="LocalInputSource" />
        /// </param>
        /// <param name="workflowOptions">
        ///     <see cref="WorkflowOptions" />
        /// </param>
        /// <returns>
        ///     <see cref="WorkflowResponse{TInferenceModel}" />
        /// </returns>
        public async Task<WorkflowResponse<GeneratedV1>> ExecuteWorkflowAsync(
            string workflowId,
            UrlInputSource inputSource,
            WorkflowOptions workflowOptions = null)
        {
            Logger?.LogInformation("Asynchronous parsing of {} ...", inputSource.FileUrl);

            if (workflowOptions == null)
            {
                workflowOptions = new WorkflowOptions();
            }

            return await _mindeeApi.PostWorkflowExecution<GeneratedV1>(
                workflowId,
                new WorkflowParameter(
                    null,
                    inputSource,
                    workflowOptions.FullText,
                    workflowOptions.Alias,
                    workflowOptions.Priority,
                    workflowOptions.PublicUrl,
                    workflowOptions.Rag
                ));
        }

        /// <summary>
        ///     Load a local prediction.
        ///     Typically used when wanting to load from a webhook callback.
        ///     However, any kind of Mindee response may be loaded.
        /// </summary>
        /// <typeparam name="TInferenceModel"></typeparam>
        /// <returns></returns>
        public AsyncPredictResponse<TInferenceModel> LoadPrediction<TInferenceModel>(
            LocalResponse localResponse)
            where TInferenceModel : class, new()
        {
            var model = JsonSerializer.Deserialize<AsyncPredictResponse<TInferenceModel>>(localResponse.FileBytes);
            model.RawResponse = ToString();

            return model;
        }

        /// <summary>
        ///     Poll for results until the prediction is retrieved or the max amount of attempts is reached.
        /// </summary>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <param name="enqueueResponse">
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </param>
        /// <param name="endpoint">
        ///     <see cref="CustomEndpoint" />
        /// </param>
        /// <param name="pollingOptions">
        ///     <see cref="AsyncPollingOptions" />
        /// </param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException">Thrown when maxRetries is reached and the result isn't ready.</exception>
        private async Task<AsyncPredictResponse<TInferenceModel>> PollForResultsAsync<TInferenceModel>(
            AsyncPredictResponse<TInferenceModel> enqueueResponse,
            CustomEndpoint endpoint,
            AsyncPollingOptions pollingOptions,
            CancellationToken cancellationToken = default)
            where TInferenceModel : GeneratedV1, new()
        {
            var maxRetries = pollingOptions.MaxRetries + 1;
            var jobId = enqueueResponse.Job.Id;
            Logger?.LogInformation("Enqueued with job ID: {}", jobId);
            Logger?.LogInformation(
                "Waiting {} seconds before attempting to retrieve the document...",
                pollingOptions.InitialDelaySec);
            await Task.Delay(pollingOptions.InitialDelayMilliSec, cancellationToken);
            var tryCounter = 0;
            AsyncPredictResponse<TInferenceModel> response;
            while (tryCounter < maxRetries)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Logger?.LogInformation(
                    "Attempting to retrieve: {RetryCount} of {MaxRetries}",
                    tryCounter + 1,
                    maxRetries);

                response = await ParseQueuedAsync<TInferenceModel>(endpoint, jobId, cancellationToken);
                if (response.Document != null)
                {
                    return response;
                }
                tryCounter++;
                await ThrottlePollingAttemptAsync(tryCounter, maxRetries, pollingOptions, cancellationToken);
            }

            throw new MindeeException($"Could not complete after {tryCounter} attempts.");
        }

        /// <summary>
        ///     Poll for results until the prediction is retrieved or the max amount of attempts is reached.
        /// </summary>
        /// <typeparam name="TInferenceModel">
        ///     Set the prediction model used to parse the document.
        ///     The response object will be instantiated based on this parameter.
        /// </typeparam>
        /// <param name="enqueueResponse">
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </param>
        /// <param name="pollingOptions">
        ///     <see cref="AsyncPollingOptions" />
        /// </param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        ///     <see cref="AsyncPredictResponse{TInferenceModel}" />
        /// </returns>
        /// <exception cref="MindeeException">Thrown when maxRetries is reached and the result isn't ready.</exception>
        private async Task<AsyncPredictResponse<TInferenceModel>> PollForResultsAsync<TInferenceModel>(
            AsyncPredictResponse<TInferenceModel> enqueueResponse,
            AsyncPollingOptions pollingOptions,
            CancellationToken cancellationToken = default)
            where TInferenceModel : class, new()
        {
            var maxRetries = pollingOptions.MaxRetries + 1;
            var jobId = enqueueResponse.Job.Id;
            Logger?.LogInformation("Enqueued with job ID: {}", jobId);
            Logger?.LogInformation(
                "Waiting {} seconds before attempting to retrieve the document...",
                pollingOptions.InitialDelaySec);
            await Task.Delay(pollingOptions.InitialDelayMilliSec, cancellationToken);
            var tryCounter = 1;
            AsyncPredictResponse<TInferenceModel> response;
            while (tryCounter < maxRetries)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Logger?.LogInformation(
                    "Attempting to retrieve: {RetryCount} of {MaxRetries}",
                    tryCounter + 1,
                    maxRetries);

                response = await ParseQueuedAsync<TInferenceModel>(jobId, cancellationToken);
                if (response.Document != null)
                {
                    return response;
                }
                tryCounter++;
                await ThrottlePollingAttemptAsync(tryCounter, maxRetries, pollingOptions, cancellationToken);
            }

            throw new MindeeException($"Could not complete after {tryCounter} attempts.");
        }
    }
}
