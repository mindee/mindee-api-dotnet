using System.Collections.Generic;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Define the inference model of values.
    /// </summary>
    /// <typeparam name="TPredictionModel">The prediction model which defines values.</typeparam>
    public class Inference<TPredictionModel>
        where TPredictionModel : class, new()
    {
        /// <summary>
        /// The pages and the associated values which was detected on the document.
        /// </summary>
        public List<Page<TPredictionModel>> Pages { get; set; }
    }
}
