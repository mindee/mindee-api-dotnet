using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mindee.Parsing.CustomBuilder
{
    /// <summary>
    /// Define a simple prediction from a model built thanks to an API builder.
    /// </summary>
    [Serializable]
    public sealed class CustomPrediction : Dictionary<string, ListField>
    {
        public CustomPrediction()
        { 
        }

        private CustomPrediction(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
