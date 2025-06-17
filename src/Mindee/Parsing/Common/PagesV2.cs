using System.Collections.Generic;
using System.Linq;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// The pages and the associated values which were detected on the document on the API V2.
    /// </summary>
    public class PagesV2 : List<PageV2>
    {
        /// <summary>
        /// A prettier representation.
        /// </summary>
        public override string ToString()
        {
            return string.Join("\n", this.Select(p => p.ToString()));
        }

        /// <summary>
        /// Returns whether or not the page list has any predictions set.
        /// </summary>
        /// <returns></returns>
        public bool HasPredictions()
        {
            return (this.Count > 0 && this.First().Fields != null);
        }
    }
}
