using Mindee.V2.ClientOptions;

namespace Mindee.V2.Search.Models
{
    /// <summary>
    /// Search parameters for models.
    /// </summary>
    public class ModelSearchParameters : BaseSearchParameters
    {
        /// <summary>
        /// Case-insensitive search term for the model name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Case-insensitive search term for the model type
        /// </summary>
        public string ModelType { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="modelType"><see cref="ModelType"/></param>
        /// <param name="page"><see cref="BaseSearchParameters.Page"/></param>
        /// <param name="perPage"><see cref="BaseSearchParameters.PerPage"/></param>
        public ModelSearchParameters(
            string name = null, string modelType = null, int? page = null, int? perPage = null)
            : base(page, perPage)
        {
            Name = name;
            ModelType = modelType;
        }
    }
}
