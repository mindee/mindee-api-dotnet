using System;

namespace Mindee.Http
{
    /// <summary>
    /// Is used to parameterize the associated endpoint on a model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CustomEndpointAttributeV2 : EndpointAttribute
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="modelName">The name of the product associated to the expected model.</param>
        /// <param name="modelVersion">The version number of the API. Without the v (for example for the v1.2: 1.2).
        /// By default set to 1.0</param>
        public CustomEndpointAttributeV2(
            string modelName
            , string modelVersion = "1") : base(modelName, modelVersion)
        {
        }
    }
}
