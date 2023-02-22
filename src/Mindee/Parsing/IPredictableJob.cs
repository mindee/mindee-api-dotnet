using System.Threading.Tasks;
using Mindee.Parsing.Common;

namespace Mindee.Parsing
{
    /// <summary>
    /// Job informations.
    /// </summary>
    public interface IPredictableJob
    {
        /// <summary>
        /// Get a document which was predicted.
        /// </summary>
        Task<AsyncPredictResponse<TModel>> DocumentQueueGetAsync<TModel>(string jobId)
            where TModel : class, new();
    }
}
