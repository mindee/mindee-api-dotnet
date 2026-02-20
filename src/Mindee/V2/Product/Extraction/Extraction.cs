using System;
using Mindee.V2.Product.Extraction.Params;

namespace Mindee.V2.Product.Extraction
{
    /// <summary>
    /// Automatically extract structured data from any image or scanned document.
    /// </summary>
    public class Extraction : BaseProduct
    {
        /// <summary>
        /// Type of the product's response.
        /// </summary>
        public static new Type ResponseType => typeof(ExtractionResponse);

        /// <summary>
        ///     Retrieves the parameters class for the product.
        /// </summary>
        public static new Type ParametersType => typeof(ExtractionParameters);

        /// <summary>
        ///     Retrieves the slug of the product.
        /// </summary>
        public static new string Slug => "extraction";
    }
}
