using System.Threading.Tasks;
using Mindee.Parsing.V2;

namespace Mindee.Http
{
    /// <summary>
    /// Communicate with the Mindee API V2.
    /// <p>
    /// You may use this interface to make your own custom class.
    /// However, we may introduce breaking changes in minor versions as needed.
    /// </p>
    /// </summary>
    public interface IHttpApiV2
    {
        /// <summary>
        /// Do a prediction according parameters for custom model defined in the Studio.
        /// </summary>
        /// <param name="predictParameter"><see cref="PredictParameterV2"/></param>
        Task<AsyncJobResponse> EnqueuePostAsync(
            PredictParameterV2 predictParameter);


        /// <summary>
        /// Get a document which was predicted.
        /// </summary>
        /// <param name="jobId">The job ID as returned by the predict_async route.</param>
        Task<AsyncInferenceResponse> DocumentQueueGetAsync(
            string jobId);
    }
}
