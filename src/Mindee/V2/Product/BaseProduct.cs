using System;

namespace Mindee.V2.Product
{
    /// <summary>
    /// Base class for all V2 products.
    /// </summary>
    public abstract class BaseProduct
    {
        /// <summary>
        /// Type of the product's response.
        /// </summary>
        public static Type ResponseType;

        /// <summary>
        ///     Retrieves the parameters class for the product.
        /// </summary>
        public static Type ParametersType;

        /// <summary>
        ///     Retrieves the slug of the product.
        /// </summary>
        public static string Slug;
    }
}
