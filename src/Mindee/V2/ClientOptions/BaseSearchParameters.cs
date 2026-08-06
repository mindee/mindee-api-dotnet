using System.Collections.Generic;

namespace Mindee.V2.ClientOptions
{
    /// <summary>
    ///     Base parameters for searches.
    /// </summary>
    public abstract class BaseSearchParameters
    {
        /// <summary>
        /// 1-based page index.
        /// </summary>
        public int? Page { get; }

        /// <summary>
        /// Number of items per page.
        /// </summary>
        public int? PerPage { get; }

        /// <summary>
        /// Base constructor.
        /// </summary>
        /// <param name="page"><see cref="Page"/></param>
        /// <param name="perPage"><see cref="PerPage"/></param>
        protected BaseSearchParameters(int? page, int? perPage)
        {
            Page = page;
            PerPage = perPage;
        }

        /// <summary>
        /// Gets the request parameters for the search request.
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetRequestParameters()
        {
            var parameters = new Dictionary<string, string>();

            if (Page != null && Page > 0)
                parameters.Add("page", Page.ToString());

            if (PerPage != null && PerPage > 0)
                parameters.Add("per_page", PerPage.ToString());

            return parameters;
        }
    }
}
