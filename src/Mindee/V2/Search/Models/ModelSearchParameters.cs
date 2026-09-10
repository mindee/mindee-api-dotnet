using System;
using System.Collections.Generic;
using Mindee.V2.ClientOptions;

namespace Mindee.V2.Search.Models
{
    /// <summary>
    /// Search for models within the organization linked to the API key.
    /// All search filters are optional.
    /// If no search filters are given, all models belonging to the organization are returned.
    /// Results are paginated.
    /// </summary>
    public class ModelSearchParameters : BaseSearchParameters<ModelSearchResponse>
    {
        /// <summary>
        /// Filter models by partial name match, case-insensitive.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Filter by an exact model type.
        /// </summary>
        public string ModelType { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="modelType"><see cref="ModelType"/></param>
        /// <param name="page">
        ///     <see cref="BaseSearchParameters{ModelSearchResponse}.Page"/>
        /// </param>
        /// <param name="perPage">
        ///     <see cref="BaseSearchParameters{ModelSearchResponse}.PerPage"/>
        /// </param>
        public ModelSearchParameters(
            string name = null, string modelType = null, int? page = null, int? perPage = null)
            : base(page, perPage)
        {
            if (name == "")
                throw new ArgumentException("name cannot be an empty string.", nameof(name));
            if (modelType != null && string.IsNullOrWhiteSpace(modelType))
                throw new ArgumentException("modelType cannot be whitespace.", nameof(modelType));

            Name = name;
            ModelType = modelType;
        }

        /// <inheritdoc />
        public override Dictionary<string, string> GetRequestParameters()
        {
            var parameters = base.GetRequestParameters();
            if (Name != null)
                parameters.Add("name", Name);
            if (ModelType != null)
                parameters.Add("model_type", ModelType);
            return parameters;
        }
    }
}
