using System;

namespace Mindee.V2.Parsing
{
    /// <summary>
    /// Attribute to specify the slug for a product endpoint.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class EndpointSlugAttribute : Attribute
    {
        /// <summary>
        /// Slug of th eendpoint
        /// </summary>
        public string Slug { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="slug"></param>
        public EndpointSlugAttribute(string slug)
        {
            Slug = slug;
        }
    }
}
