using System;

namespace Mindee.Http
{
    /// <summary>
    /// Define a Mindee V2 API endpoint.
    /// </summary>
    public sealed class CustomEndpointV2
    {
        /// <summary>
        /// The name of the target model.
        /// </summary>
        public string ModelName { get; }

        /// <summary>
        /// The version number of the product. Without the v (for example for the v1.2: 1.2). By default set to 1.0.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="modelName">The name of the product associated to the expected model.</param>
        /// <param name="version">The version number of the API. Without the v (for example for v1.2: 1.2).</param>
        public CustomEndpointV2(
            string modelName
            , string version = "1")
        {
            ModelName = modelName;
            Version = version;
        }
    }
}
