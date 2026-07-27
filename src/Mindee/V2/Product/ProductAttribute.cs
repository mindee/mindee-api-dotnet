using System;

namespace Mindee.V2.Product
{
    /// <summary>
    /// Attribute to specify various product metadata.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ProductAttribute : Attribute
    {
        /// <summary>
        /// URL slug of the product.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// Attribute to specify various product metadata.
        /// </summary>
        public ProductAttribute(string slug)
        {
            Slug = slug;
        }
    }
}
