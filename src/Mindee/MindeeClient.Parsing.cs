using System;
using System.Threading.Tasks;
using Mindee.Exceptions;
using Mindee.Input;
using Mindee.Parsing;
using Mindee.Parsing.Common;
using Mindee.Parsing.CustomBuilder;
using Mindee.Pdf;

namespace Mindee
{
    /// <summary>
    /// The entry point to use the Mindee features.
    /// </summary>
    public sealed partial class MindeeClient
    {
        /// <summary>
        /// Try to parse the current document using custom builder API.
        /// </summary>
        /// <param name="endpoint"><see cref="CustomEndpoint"/></param>
        /// <returns><see cref="PredictResponse{CustomV1Inference}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<PredictResponse<CustomV1Inference>> ParseAsync(CustomEndpoint endpoint)
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _mindeeApi.PredictPostAsync<CustomV1Inference>(
                endpoint,
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename));
        }

        /// <summary>
        /// Try to parse the current document using custom builder API.
        /// </summary>
        /// <param name="endpoint"><see cref="CustomEndpoint"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <returns><see cref="PredictResponse{CustomV1Inference}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<PredictResponse<CustomV1Inference>> ParseAsync(
            CustomEndpoint endpoint
            , PageOptions pageOptions)
        {
            if (DocumentClient == null)
            {
                return null;
            }

            if (DocumentClient.Extension.Equals(
                ".pdf",
                StringComparison.InvariantCultureIgnoreCase))
            {
                DocumentClient.File = _pdfOperation.Split(new SplitQuery(DocumentClient.File, pageOptions)).File;
            }

            return await _mindeeApi.PredictPostAsync<CustomV1Inference>(
                endpoint,
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename));
        }

        /// <summary>
        /// Try to parse the current document.
        /// </summary>
        /// <param name="withFullText">Get all the words in the current document.By default, set to false.</param>
        /// <param name="withCropper">To get the cropper information about the current document.By default, set to false.</param>
        /// <typeparam name="TInferenceModel">Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.</typeparam>
        /// <returns><see cref="PredictResponse{TInferenceModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        /// <remarks>With full text doesn't work for all the types.</remarks>
        public async Task<PredictResponse<TInferenceModel>> ParseAsync<TInferenceModel>(
            bool withFullText = false
            , bool withCropper = false)
            where TInferenceModel : class, new()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _mindeeApi.PredictPostAsync<TInferenceModel>(
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename,
                    withFullText,
                    withCropper));
        }

        /// <summary>
        /// Try to parse the current document.
        /// </summary>
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <param name="withCropper">To get the cropping results about the current document.By default, set to false.</param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <typeparam name="TInferenceModel">Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.</typeparam>
        /// <returns><see cref="PredictResponse{TInferenceModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        /// <remarks>With full text doesn't work for all the types. Check the API documentation first.</remarks>
        public async Task<PredictResponse<TInferenceModel>> ParseAsync<TInferenceModel>(
            PageOptions pageOptions
            , bool withFullText = false
            , bool withCropper = false)
            where TInferenceModel : class, new()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            if (DocumentClient.Extension.Equals(
                ".pdf",
                StringComparison.InvariantCultureIgnoreCase))
            {
                DocumentClient.File = _pdfOperation.Split(new SplitQuery(DocumentClient.File, pageOptions)).File;
            }

            return await _mindeeApi.PredictPostAsync<TInferenceModel>(
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename,
                    withFullText,
                    withCropper));
        }

        /// <summary>
        /// Try to enqueue the parsing of the current document.
        /// </summary>
        /// <param name="withFullText">Get all the words in the current document.By default, set to false.</param>
        /// <param name="withCropper">To get the cropper information about the current document.By default, set to false.</param>
        /// <typeparam name="TInferenceModel">Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.</typeparam>
        /// <returns><see cref="AsyncPredictResponse{TInferenceModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        /// <remarks>With full text doesn't work for all the types.</remarks>
        public async Task<AsyncPredictResponse<TInferenceModel>> EnqueueAsync<TInferenceModel>(
            bool withFullText = false
            , bool withCropper = false)
            where TInferenceModel : class, new()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _mindeeApi.PredictAsyncPostAsync<TInferenceModel>(
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename,
                    withFullText,
                    withCropper));
        }

        /// <summary>
        /// Try to parse the current document.
        /// </summary>
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <param name="withCropper">To get the cropping results about the current document.By default, set to false.</param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <typeparam name="TInferenceModel">Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.</typeparam>
        /// <returns><see cref="AsyncPredictResponse{TInferenceModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        /// <remarks>With full text doesn't work for all the types. Check the API documentation first.</remarks>
        public async Task<AsyncPredictResponse<TInferenceModel>> EnqueueAsync<TInferenceModel>(
            PageOptions pageOptions
            , bool withFullText = false
            , bool withCropper = false)
            where TInferenceModel : class, new()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            if (DocumentClient.Extension.Equals(
                ".pdf",
                StringComparison.InvariantCultureIgnoreCase))
            {
                DocumentClient.File = _pdfOperation.Split(new SplitQuery(DocumentClient.File, pageOptions)).File;
            }

            return await _mindeeApi.PredictAsyncPostAsync<TInferenceModel>(
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename,
                    withFullText,
                    withCropper));
        }

        /// <summary>
        /// Get the full job response from the parsed enqueued document.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <typeparam name="TInferenceModel">Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.</typeparam>
        /// <returns><see cref="AsyncPredictResponse{TInferenceModel}"/></returns>
        public async Task<AsyncPredictResponse<TInferenceModel>> ParseQueuedAsync<TInferenceModel>(string jobId)
            where TInferenceModel : class, new()
        {
            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(jobId);
            }

            var jobResponse = await _mindeeApi.DocumentQueueGetAsync<TInferenceModel>(jobId);

            return jobResponse;
        }
    }
}
