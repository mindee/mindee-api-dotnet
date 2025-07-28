using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    /// Inference fields dict.
    /// </summary>
    public class InferenceFields : Dictionary<string, DynamicField>
    {
        /// <summary>
        /// Pretty-prints the dictionary with an optional indentation level.
        /// </summary>
        /// <param name="indent">Indent level (each level equals two spaces).</param>
        /// <returns>String representation similar to the Java implementation.</returns>
        public string ToString(int indent)
        {
            if (Count == 0)
            {
                return string.Empty;
            }

            string padding = string.Concat(Enumerable.Repeat("  ", Math.Max(0, indent)));
            StringBuilder joiner = new();

            foreach (KeyValuePair<string, DynamicField> fieldPair in this)
            {
                DynamicField fieldValue = fieldPair.Value;
                StringBuilder line = new();

                line.Append(padding)
                    .Append(':')
                    .Append(fieldPair.Key)
                    .Append(": ");

                if (fieldValue.ListField is { Items.Count: > 0 } listField)
                {
                    line.Append(listField);
                }
                else if (fieldValue.ObjectField != null)
                {
                    line.Append(fieldValue.ObjectField);
                }
                else if (fieldValue.SimpleField != null)
                {
                    string simpleText = fieldValue.SimpleField.ToString();
                    if (!string.IsNullOrEmpty(simpleText))
                    {
                        line.Append(simpleText);
                    }
                }

                joiner.AppendLine(line.ToString());
            }
            string summary = joiner.ToString().TrimEnd('\n', '\r');
            return SummaryHelper.Clean(summary);
        }

        /// <summary>
        /// Default string representation.
        /// </summary>
        public override string ToString() => ToString(0);
    }
}
