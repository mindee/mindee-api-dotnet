using System.Collections.Generic;
using System.Linq;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// The pages and the associated values which were detected on the document.
    /// </summary>
    /// <typeparam name="TPagePrediction"></typeparam>
    public class Pages<TPagePrediction> : List<Page<TPagePrediction>>
        where TPagePrediction : IPrediction, new()
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
            return (this.Count > 0 && this.First().Prediction != null);
        }
    }
}
