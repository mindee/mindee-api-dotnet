using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Cropper
{
    /// <summary>
    /// Document data for Cropper, API version 1.
    /// </summary>
    public class CropperV1Document : IPrediction
    {
        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            return "";
        }
    }
}
