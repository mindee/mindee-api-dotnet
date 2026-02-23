using System;

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
    }
}
