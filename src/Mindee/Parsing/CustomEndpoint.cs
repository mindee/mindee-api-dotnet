namespace Mindee.Parsing
{
    /// <summary>
    /// Define a Mindee API endpoint.
    /// </summary>
    public sealed class CustomEndpoint
    {
        /// <summary>
        /// The name of the product associated to the expected model.
        /// </summary>
        public string EndpointName { get; }

        /// <summary>
        /// The version number of the API. Without the v (for example for the v1.2: 1.2).
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// The name of the organization wich hold the API. Usefull when using custom builder. By default to mindee.
        /// </summary>
        public string AccountName { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="endpointName">The name of the product associated to the expected model.</param>
        /// <param name="version">The version number of the API. Without the v (for example for the v1.2: 1.2).</param>
        /// <param name="accountName">The name of the account wich hold the API. Usefull when using custom builder.</param>
        public CustomEndpoint(
            string endpointName
            , string version
            , string accountName)
        {
            EndpointName = endpointName;
            Version = version;
            AccountName = accountName;
        }
    }
}
