using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.CustomBuilder
{
    /// <summary>
    /// Define a page prediction from a model built thanks to an API builder.
    /// </summary>
    [Serializable]
    public sealed class CustomV1PagePrediction : Dictionary<string, ListField>
    {
        /// <summary>
        /// Default empty constructor.
        /// </summary>
        public CustomV1PagePrediction()
        {
        }

        private CustomV1PagePrediction(SerializationInfo info, StreamingContext context) : base(info, context)
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
