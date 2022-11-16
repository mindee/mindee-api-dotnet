using System;

namespace Mindee.Parsing
{
    /// <summary>
    /// Is used to parameterize the associated endpoint on a model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EndpointAttribute : Attribute
    {
        private readonly string _endpointName;
        private readonly string _version;
        private readonly string _accountName;

        /// <summary>
        ///
        /// </summary>
        /// <param name="endpointName">The name of the product associated to the expected model.</param>
        /// <param name="version">The version number of the API. Without the v (for example for the v1.2: 1.2).</param>
        /// <param name="accountName">The name of the organization wich hold the API. Usefull when using custom builder. By default to mindee.</param>
        public EndpointAttribute(
            string endpointName
            , string version
            , string accountName = "mindee")
        {
            _endpointName = endpointName;
            _version = version;
            _accountName = accountName;
        }

        /// <summary>
        /// The name of the product associated to the expected model.
        /// </summary>
        public virtual string EndpointName
        {
            get { return _endpointName; }
        }

        /// <summary>
        /// The version number of the API. Without the v (for example for the v1.2: 1.2).
        /// </summary>
        public virtual string Version
        {
            get { return _version; }
        }

        /// <summary>
        /// The name of the account that owns the API. Useful when using custom builder. Default to mindee.
        /// </summary>
        public virtual string AccountName
        {
            get { return _accountName; }
        }
    }
}
