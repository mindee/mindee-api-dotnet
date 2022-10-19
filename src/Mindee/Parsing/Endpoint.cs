namespace Mindee.Parsing
{
    /// <summary>
    /// Define a Mindee API endpoint.
    /// </summary>
    public sealed class Endpoint
    {
        /// <summary>
        /// The name of the product associated to the expected model.
        /// </summary>
        public string ProductName { get; }

        /// <summary>
        /// The version number of the API. Without the v (for example for the v1.2: 1.2).
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// The name of the organization wich hold the API. Usefull when using custom builder. By default to mindee.
        /// </summary>
        public string OrganizationName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productName">The name of the product associated to the expected model.</param>
        /// <param name="version">The version number of the API. Without the v (for example for the v1.2: 1.2).</param>
        /// <param name="organizationName">The name of the organization wich hold the API. Usefull when using custom builder. By default to mindee.</param>
        public Endpoint(
            string productName
            , string version
            , string organizationName = "mindee")
        {
            ProductName = productName;
            Version = version;
            OrganizationName = organizationName;
        }
    }
}
