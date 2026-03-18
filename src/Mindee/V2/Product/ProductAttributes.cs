using System;

namespace Mindee.V2.Product
{
    /// <summary>
    /// Attribute to specify various product metadata.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ProductAttributes : Attribute
    {
        /// <summary>
        /// URL slug of the product.
        /// </summary>
        public string Slug;

        /// <summary>
        /// Attribute to specify various product metadata.
        /// </summary>
        public ProductAttributes(string slug)
        {
            Slug = slug;
        }
    }
}
