using System;
using Mindee.V2.Product;

namespace Mindee.V2.Parsing
{
    /// <summary>
    ///     Common response information from Mindee API V2.
    /// </summary>
    public abstract class CommonInferenceResponse : BaseResponse
    {
        /// <summary>
        /// Slug of the product.
        /// </summary>
        public virtual string Slug { get; }
    }

}
