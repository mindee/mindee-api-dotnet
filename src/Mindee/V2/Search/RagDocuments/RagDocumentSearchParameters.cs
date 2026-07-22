using System.Collections.Generic;
using Mindee.V2.ClientOptions;

namespace Mindee.V2.Search.Model
{
    /// <summary>
    /// Search parameters for models.
    /// </summary>
    public class RagDocumentSearchParameters : BaseSearchParameters
    {
        /// <summary>
        /// Model identifier to search in.
        /// </summary>
        public string ModelId { get; }

        /// <summary>
        /// Case-insensitive substring search on filename.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="modelId"><see cref="ModelId"/></param>
        /// <param name="filename"><see cref="Filename"/></param>
        /// <param name="page"><see cref="BaseSearchParameters.Page"/></param>
        /// <param name="perPage"><see cref="BaseSearchParameters.PerPage"/></param>
        public RagDocumentSearchParameters(
            string modelId = null, string filename = null, int? page = null, int? perPage = null)
            : base(page, perPage)
        {
            ModelId = modelId;
            Filename = filename;
        }

        /// <inheritdoc />
        public override Dictionary<string, string> GetRequestParameters()
        {
            var parameters = base.GetRequestParameters();
            if (!string.IsNullOrEmpty(ModelId))
            {
                parameters.Add("model_id", ModelId);
            }
            else
            {
                throw new System.ArgumentException("ModelId is required in RagDocumentSearchParameters");
            }
            if (!string.IsNullOrEmpty(Filename))
                parameters.Add("filename", Filename);
            return parameters;
        }
    }
}
