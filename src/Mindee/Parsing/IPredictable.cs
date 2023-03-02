using System.Threading.Tasks;
using Mindee.Parsing.Common;
using Mindee.Parsing.Common.Jobs;

namespace Mindee.Parsing
{
    /// <summary>
    /// Make predictions and returned a result of it.
    /// </summary>
    public interface IPredictable : IPredictableJob
    {
        /// <summary>
        /// Do a prediction according parameters.
        /// </summary>
        /// <typeparam name="TModel">Result expected type.</typeparam>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        Task<Document<TModel>> PredictAsync<TModel>(
            PredictParameter predictParameter)
            where TModel : class, new();

        /// <summary>
        /// Do a prediction according parameters for custom model define in the Studio.
        /// </summary>
        /// <typeparam name="TModel">Result expected type.</typeparam>
        /// <param name="endpoint"><see cref="CustomEndpoint"/></param>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        Task<Document<TModel>> PredictAsync<TModel>(
            CustomEndpoint endpoint,
            PredictParameter predictParameter)
            where TModel : class, new();

        /// <summary>
        /// Enqueue a prediction according parameters.
        /// </summary>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        /// <typeparam name="TModel">Document type.</typeparam>
        Task<PredictEnqueuedResponse> EnqueuePredictAsync<TModel>(PredictParameter predictParameter)
            where TModel : class, new();
    }
}
