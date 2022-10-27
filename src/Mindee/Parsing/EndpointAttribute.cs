using System;

namespace Mindee.Parsing
{
    /// <summary>
    /// Is used to parameterize the associated endpoint on a model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EndpointAttribute : Attribute
    {
        private readonly string _productName;
        private readonly string _version;
        private readonly string _organizationName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productName">The name of the product associated to the expected model</param>
        /// <param name="version">The version number of the API. Without the v (for example for the v1.2: 1.2)</param>
        /// <param name="organizationName">The name of the organization wich hold the API. Usefull when using custom builder. By default to mindee.</param>
        public EndpointAttribute(
            string productName
            , string version
            , string organizationName = "mindee")
        {
            _productName = productName;
            _version = version;
            _organizationName = organizationName;
        }

        public virtual string ProductName
        {
            get { return _productName; }
        }

        public virtual string Version
        {
            get { return _version; }
        }

        public virtual string OrganizationName
        {
            get { return _organizationName; }
        }
    }
}
