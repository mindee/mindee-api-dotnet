using System;

namespace Mindee.Http
{
    /// <summary>
    ///     Define a Mindee V1 API endpoint.
    /// </summary>
    public sealed class CustomEndpoint
    {
        /// <summary>
        ///     Default constructor.
        /// </summary>
        /// <param name="endpointName">The name of the product associated to the expected model.</param>
        /// <param name="accountName">The name of the account which owns the API. Useful when using custom builder.</param>
        /// <param name="version">The version number of the API. Without the v (for example for v1.2: 1.2).</param>
        public CustomEndpoint(
            string endpointName
            , string accountName
            , string version = "1")
        {
            EndpointName = endpointName;
            AccountName = accountName;
            Version = version;
        }

        /// <summary>
        ///     The name of the product associated to the expected model.
        /// </summary>
        public string EndpointName { get; }

        /// <summary>
        ///     The version number of the API. Without the v (for example for the v1.2: 1.2). By default set to 1.0.
        /// </summary>
        public string Version { get; }

        /// <summary>
        ///     The name of the organization which owns the API. Useful when using custom builder.
        ///     Defaults to "mindee".
        /// </summary>
        public string AccountName { get; }

        /// <summary>
        ///     Get the base URL for the endpoint.
        /// </summary>
        /// <returns></returns>
        public string GetBaseUrl()
        {
            return $"{AccountName}/{EndpointName}/v{Version}";
        }

        /// <summary>
        ///     Get an endpoint for a given prediction model.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static CustomEndpoint GetEndpoint<TModel>()
        {
            if (!Attribute.IsDefined(typeof(TModel), typeof(EndpointAttribute)))
            {
                throw new NotSupportedException(
                    $"The type {typeof(TModel)} is not supported as a prediction model. " +
                    "The endpoint attribute is missing. " +
                    "Please refer to the documentation or contact support.");
            }

            var endpointAttribute = (EndpointAttribute)Attribute.GetCustomAttribute(
                typeof(TModel), typeof(EndpointAttribute));

            return new CustomEndpoint(
                endpointAttribute.ModelName,
                endpointAttribute.AccountName,
                endpointAttribute.ModelVersion);
        }
    }
}
