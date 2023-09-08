using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.W9
{
    /// <summary>
    /// Document data for US W9, API version 1.
    /// </summary>
    public class W9V1Document : IPrediction
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
