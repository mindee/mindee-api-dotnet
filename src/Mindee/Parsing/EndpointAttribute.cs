using System;

namespace Mindee.Parsing
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EndpointAttribute : Attribute
    {
        private readonly string _productName;
        private readonly string _version;

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
