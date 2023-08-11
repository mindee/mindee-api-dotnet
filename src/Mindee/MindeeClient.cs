using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Http;
using Mindee.Input;
using Mindee.Parsing.Common;
using Mindee.Pdf;
using Mindee.Product.Custom;

namespace Mindee
{
    /// <summary>
    /// The entry point to use the Mindee features.
    /// </summary>
    public sealed class MindeeClient
    {
        private readonly IPdfOperation _pdfOperation;
        private readonly IHttpApi _mindeeApi;
        private readonly ILogger<MindeeClient> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="apiKey">The required API key to use Mindee.</param>
        /// <param name="logger"></param>
        public MindeeClient(string apiKey, ILogger<MindeeClient> logger = null)
        {
            var mindeeSettings = new MindeeSettings
            {
                ApiKey = apiKey
            };
            _pdfOperation = new DocNetApi();
            _mindeeApi = new MindeeApi(Options.Create(mindeeSettings));
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mindeeSettings"><see cref="MindeeSettings"/></param>
        /// <param name="logger"></param>
        public MindeeClient(MindeeSettings mindeeSettings, ILogger<MindeeClient> logger = null)
        {
            _pdfOperation = new DocNetApi();
            _mindeeApi = new MindeeApi(Options.Create(mindeeSettings));
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pdfOperation"><see cref="IPdfOperation"/></param>
        /// <param name="httpApi"><see cref="IHttpApi"/></param>
        /// <param name="logger"></param>
        public MindeeClient(IPdfOperation pdfOperation, IHttpApi httpApi, ILogger<MindeeClient> logger = null)
        {
            _pdfOperation = pdfOperation;
            _mindeeApi = httpApi;
            _logger = logger;
        }

        /// <summary>
        /// Call Custom prediction API on the document and parse the results.
        /// </summary>
        /// <param name="endpoint"><see cref="CustomEndpoint"/></param>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <param name="predictOptions"><see cref="PageOptions"/></param>
        /// <returns><see cref="PredictResponse{CustomV1Inference}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<PredictResponse<CustomV1>> ParseAsync(
            LocalInputSource inputSource
            , CustomEndpoint endpoint
            , PredictOptions predictOptions = null
            , PageOptions pageOptions = null)
        {
            _logger?.LogInformation(message: "Synchronous parsing of {} ...", nameof(CustomV1));

            if (predictOptions == null)
            {
                predictOptions = new PredictOptions();
            }
            if (pageOptions != null && inputSource.IsPdf())
            {
                inputSource.FileBytes = _pdfOperation.Split(
                    new SplitQuery(inputSource.FileBytes, pageOptions)).File;
            }

            return await _mindeeApi.PredictPostAsync<CustomV1>(
                endpoint,
                new PredictParameter(
                    inputSource.FileBytes,
                    inputSource.Filename,
                    predictOptions.AllWords,
                    predictOptions.Cropper));
        }

        /// <summary>
        /// Call Standard prediction API on the document and parse the results.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="predictOptions"><see cref="PageOptions"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <typeparam name="TInferenceModel">Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.</typeparam>
        /// <returns><see cref="PredictResponse{TInferenceModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        /// <remarks>With full text doesn't work for all the types.</remarks>
        public async Task<PredictResponse<TInferenceModel>> ParseAsync<TInferenceModel>(
            LocalInputSource inputSource
            , PredictOptions predictOptions = null
            , PageOptions pageOptions = null)
            where TInferenceModel : class, new()
        {
            _logger?.LogInformation(message: "Synchronous parsing of {} ...", typeof(TInferenceModel).Name);

            if (predictOptions == null)
            {
                predictOptions = new PredictOptions();
            }
            if (pageOptions != null && inputSource.IsPdf())
            {
                inputSource.FileBytes = _pdfOperation.Split(
                    new SplitQuery(inputSource.FileBytes, pageOptions)).File;
            }
            return await _mindeeApi.PredictPostAsync<TInferenceModel>(
                new PredictParameter(
                    inputSource.FileBytes,
                    inputSource.Filename,
                    predictOptions.AllWords,
                    predictOptions.Cropper));
        }

        /// <summary>
        /// Add the document to an async queue.
        /// </summary>
        /// <param name="inputSource"><see cref="LocalInputSource"/></param>
        /// <param name="predictOptions"><see cref="PageOptions"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <typeparam name="TInferenceModel">Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.</typeparam>
        /// <returns><see cref="AsyncPredictResponse{TInferenceModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        /// <remarks>With full text doesn't work for all the types.</remarks>
        public async Task<AsyncPredictResponse<TInferenceModel>> EnqueueAsync<TInferenceModel>(
            LocalInputSource inputSource
            , PredictOptions predictOptions = null
            , PageOptions pageOptions = null)
            where TInferenceModel : class, new()
        {
            _logger?.LogInformation(message: "Enqueuing of {} ...", typeof(TInferenceModel).Name);

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
                    inputSource.FileBytes,
                    inputSource.Filename,
                    predictOptions.AllWords,
                    predictOptions.Cropper));
        }

        /// <summary>
        /// Parse a document from an async queue.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <typeparam name="TInferenceModel">Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.</typeparam>
        /// <returns><see cref="AsyncPredictResponse{TInferenceModel}"/></returns>
        public async Task<AsyncPredictResponse<TInferenceModel>> ParseQueuedAsync<TInferenceModel>(string jobId)
            where TInferenceModel : class, new()
        {
            _logger?.LogInformation(message: "Parse from queue of {} ...", typeof(TInferenceModel).Name);

            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(jobId);
            }
            return await _mindeeApi.DocumentQueueGetAsync<TInferenceModel>(jobId);
        }
    }
}
