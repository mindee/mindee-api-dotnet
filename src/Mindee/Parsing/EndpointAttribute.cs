using System;

namespace Mindee.Parsing
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EndpointAttribute : Attribute
    {
        private readonly string _productName;
        private readonly string _version;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productName">The name of the product associated to the expected model</param>
        /// <param name="version">The version number of the API. Without the v (for example for the v1.2: 1.2)</param>
        public EndpointAttribute(string productName, string version)
        {
            _productName = productName;
            _version = version;
        }

        public virtual string ProductName
        {
            get { return _productName; }
        }

        public virtual string Version
        {
            get { return _version; }
        }
    }
}
