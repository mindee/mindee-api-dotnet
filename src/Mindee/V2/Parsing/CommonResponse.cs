using System;
using Mindee.V2.Product;

namespace Mindee.V2.Parsing
{
    /// <summary>
    ///     Common response information from Mindee API V2.
    /// </summary>
    public abstract class CommonResponse<TProduct> : BaseResponse where TProduct : BaseProduct, new()
    {
        /// <summary>
        /// Type of product returned by this response
        /// </summary>
        public static Type ReturnType => typeof(TProduct);
    }
}
