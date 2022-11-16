﻿using System;

namespace Mindee.Parsing
{
    /// <summary>
    /// Is used to parameterize the associated endpoint on a model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CustomEndpointAttribute : EndpointAttribute
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="endpointName">The name of the product associated to the expected model.</param>
        /// <param name="accountName">The name of the account wich hold the API. Usefull when using custom builder.</param>
        /// <param name="version">The version number of the API. Without the v (for example for the v1.2: 1.2). By default set to 1.0</param>
        public CustomEndpointAttribute(
            string endpointName
            , string accountName
            , string version = "1.0") : base(endpointName, version, accountName)
        {
        }
    }
}
