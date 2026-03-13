using System;

namespace Mindee.V2.Product
{
    /// <summary>
    /// Attribute to specify the slug for a product endpoint.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ProductSlugAttribute : Attribute
    {
        /// <summary>
        /// Slug of the endpoint
        /// </summary>
        public string Slug;

        /// <summary>
        /// Attribute to specify the slug for a product endpoint.
        /// </summary>
        public ProductSlugAttribute(string slug)
        {
            Slug = slug;
        }
    }
}
