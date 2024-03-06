using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Mindee.Parsing;
using Mindee.Parsing.Custom;

namespace Mindee.Product.Custom
{
    /// <summary>
    /// Page data for Custom Documents, API version 1.
    /// </summary>
    [Serializable]
    public sealed class CustomV1Page : Dictionary<string, ListField>, IPrediction
    {
        /// <summary>
        /// Default empty constructor.
        /// </summary>
        public CustomV1Page()
        {
        }

        /// <summary>
        /// A prettier reprensentation.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var listField in this)
            {
                result.Append($":{listField.Key}: {listField.Value}\n");
            }

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
