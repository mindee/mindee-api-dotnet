using System;

namespace Mindee.V1.Http
{
    /// <summary>
    ///     Is used to parameterize the associated endpoint on a model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomEndpointAttribute : EndpointAttribute
    {
        /// <summary>
        /// Custom endpoint attribute for models.
        /// </summary>
        /// <param name="modelName">The name of the product associated with the expected model.</param>
        /// <param name="accountName">The name of the account wich hold the API. Useful when using custom builder.</param>
        /// <param name="modelVersion">
        ///     The version number of the API. Without the v (for example, the v1.2: 1.2). Default to `1`.
        /// </param>
        public CustomEndpointAttribute(
            string modelName
            , string accountName
            , string modelVersion = "1") : base(modelName, modelVersion, accountName)
        {
        }
    }
}
