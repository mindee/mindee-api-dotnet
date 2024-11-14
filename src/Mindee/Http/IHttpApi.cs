using System.Threading.Tasks;
using Mindee.Parsing.Common;

namespace Mindee.Http
{
    /// <summary>
    /// Communicate with the Mindee API.
    /// <p>
    /// You may use this interface to make your own custom class.
    /// However, we may introduce breaking changes in minor versions as needed.
    /// </p>
    /// </summary>
    public interface IHttpApi
    {
        /// <summary>
        /// Do a prediction according parameters for custom model defined in the Studio.
        /// </summary>
        /// <typeparam name="TModel">Result expected type.</typeparam>
        /// <param name="endpoint"><see cref="CustomEndpoint"/></param>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        Task<PredictResponse<TModel>> PredictPostAsync<TModel>(
            PredictParameter predictParameter
            , CustomEndpoint endpoint = null)
            where TModel : class, new();

        /// <summary>
        /// Enqueue a prediction according parameters.
        /// </summary>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        /// <param name="endpoint"><see cref="CustomEndpoint"/></param>
        /// <typeparam name="TModel">Document type.</typeparam>
        Task<AsyncPredictResponse<TModel>> PredictAsyncPostAsync<TModel>(
            PredictParameter predictParameter
            , CustomEndpoint endpoint = null)
            where TModel : class, new();

        /// <summary>
        /// Get a document which was predicted.
        /// </summary>
        /// <param name="jobId">The job ID as returned by the predict_async route.</param>
        /// <param name="endpoint"><see cref="CustomEndpoint"/></param>
        Task<AsyncPredictResponse<TModel>> DocumentQueueGetAsync<TModel>(
            string jobId
            , CustomEndpoint endpoint = null)
            where TModel : class, new();

        /// <summary>
        /// Send a document to a workflow.
        /// </summary>
        /// <param name="workflowId">The ID of the workflow.</param>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        /// <typeparam name="TModel">Document type.</typeparam>
        Task<WorkflowResponse<TModel>> ExecutionQueuePost<TModel>(
            string workflowId,
            PredictParameter predictParameter)
            where TModel : class, new();
    }
}
