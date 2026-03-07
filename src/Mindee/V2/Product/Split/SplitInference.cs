using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Split
{
    /// <summary>
    ///     The inference result for a split utility request.
    /// </summary>
    public class SplitInference : BaseInference
    {
        /// <summary>
        /// Result of a split utility inference.
        /// </summary>
        [JsonPropertyName("result")]
        public SplitResult Result { get; set; }

        /// <summary>
        /// String representation of the split inference.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + Result + "\n";
        }
    }
}
