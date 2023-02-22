using System.Threading.Tasks;
using Mindee.Parsing.Common;

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
        Task<PredictResponse<TModel>> PredictPostAsync<TModel>(
            PredictParameter predictParameter)
            where TModel : class, new();

        /// <summary>
        /// Do a prediction according parameters for custom model define in the Studio.
        /// </summary>
        /// <typeparam name="TModel">Result expected type.</typeparam>
        /// <param name="endpoint"><see cref="CustomEndpoint"/></param>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        Task<PredictResponse<TModel>> PredictPostAsync<TModel>(
            CustomEndpoint endpoint,
            PredictParameter predictParameter)
            where TModel : class, new();

        /// <summary>
        /// Enqueue a prediction according parameters.
        /// </summary>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        /// <typeparam name="TModel">Document type.</typeparam>
        Task<AsyncPredictResponse<TModel>> PredictAsyncPostAsync<TModel>(PredictParameter predictParameter)
            where TModel : class, new();
    }
}
