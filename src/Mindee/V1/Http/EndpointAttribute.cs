using System;

namespace Mindee.V1.Http
{
    /// <summary>
    ///     Is used to parameterize the associated endpoint on a model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class EndpointAttribute : Attribute
    {
        private readonly string _accountName;
        private readonly string _modelName;
        private readonly string _modelVersion;

        /// <summary>
        /// Endpoint attribute to target model info.
        /// </summary>
        /// <param name="modelName">The name of the product associated with the expected model.</param>
        /// <param name="modelVersion">The version number of the API. Without the v (for example, for the v1.2: 1.2).</param>
        /// <param name="accountName">
        ///     The name of the organization that holds the API. Useful when using custom builder.
        /// Defaults to `mindee`.
        /// </param>
        public EndpointAttribute(
            string modelName
            , string modelVersion
            , string accountName = "mindee")
        {
            _modelName = modelName;
            _modelVersion = modelVersion;
            _accountName = accountName;
        }

        /// <summary>
        ///     The name of the product associated to the expected model.
        /// </summary>
        public virtual string ModelName => _modelName;

        /// <summary>
        ///     The version number of the API. Without the v (for example for the v1.2: 1.2).
        /// </summary>
        public virtual string ModelVersion => _modelVersion;

        /// <summary>
        ///     The name of the account that owns the API. Useful when using custom builder. Default to mindee.
        /// </summary>
        public virtual string AccountName => _accountName;
    }
}
