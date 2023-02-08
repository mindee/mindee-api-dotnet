using System;
using System.Collections.Generic;
using System.Linq;
using Mindee.Geometry;
using Mindee.Math;

namespace Mindee.Parsing.CustomBuilder.Table
{
    /// <summary>
    /// Line items generator.
    /// </summary>
    public static class LineItemsGenerator
    {
        /// <summary>
        /// Prepare he line of a table.
        /// </summary>
        /// <param name="fields">The list of fields that will be transform to line items.</param>
        /// <param name="fieldForAnchor">
        /// The line items generation is based on the fact that
        /// this field is most present that the others one, on all lines that we want.
        /// </param>
        /// <exception cref="InvalidOperationException"></exception>
        internal static IEnumerable<Line> GetPreparedLines(
             Dictionary<string, ListField> fields,
            Anchor fieldForAnchor
            )
        {
            var anchor = fields[fieldForAnchor.Name];

            if (anchor == null)
            {
                throw new InvalidOperationException("The field selected for the anchor was not found.");
            }

            if (!anchor.Values.Any())
            {
                throw new InvalidOperationException("No lines have been detected.");
            }

            var table = new Dictionary<int, Line>();

            var lineNumber = 1;
            var currentLine = new Line(lineNumber, fieldForAnchor.Tolerance);
            ListFieldValue currentValue = anchor.Values.First();
            currentLine.UpdateBbox(BboxCreation.Create(currentValue.Polygon));

            for (int i = 1; i < anchor.Values.Count; i++)
            {
                currentValue = anchor.Values[i];
                Bbox currentFieldBbox = BboxCreation.Create(currentValue.Polygon);

                if (!Precision.Equals(
                    currentLine.Bbox.MinY,
                    currentFieldBbox.MinY,
                    fieldForAnchor.Tolerance))
                {
                    // when it is a new line
                    table.Add(lineNumber, currentLine);
                    lineNumber++;
                    currentLine = new Line(lineNumber, fieldForAnchor.Tolerance);
                }
                currentLine.UpdateBbox(currentFieldBbox);
            }

            if (!table.ContainsKey(lineNumber))
            {
                table.Add(lineNumber, currentLine);
            }

            return table.Values;
        }

        /// <summary>
        /// Generate lines items from a list of names and the fields from API Builder response.
        /// </summary>
        /// <param name="fieldNames">The names of the fields that need to be transformed into lines.</param>
        /// <param name="fields">The list of the fields from the API Builder.</param>
        /// <param name="anchor"><see cref="Anchor"/></param>
        public static LineItems Generate(
            List<string> fieldNames,
            Dictionary<string, ListField> fields,
            Anchor anchor
            )
        {
            Dictionary<string, ListField> fieldsToTransformIntoLines = fields
                  .Where(field => fieldNames.Contains(field.Key))
                  .ToDictionary(field => field.Key, field => field.Value);

            IEnumerable<Line> lines = PopulateLines(
              fieldsToTransformIntoLines,
              GetPreparedLines(fieldsToTransformIntoLines, anchor));

            return new LineItems(lines);
        }

        private static IEnumerable<Line> PopulateLines(
            Dictionary<string, ListField> fields,
            IEnumerable<Line> preparedLines
            )
        {
            var populatedLines = new List<Line>();

            foreach (var currentLine in preparedLines)
            {
                foreach (var field in fields)
                {
                    foreach (var listFieldValue in field.Value.Values)
                    {
                        double minYCurrentValue = listFieldValue.Polygon.GetMinYCoordinate();

                        if (minYCurrentValue < currentLine.Bbox.MaxY
                            && minYCurrentValue >= currentLine.Bbox.MinY)
                        {
                            currentLine.UpdateField(field.Key, listFieldValue);
                        }
                    }
                }

                populatedLines.Add(currentLine);
            }

            return populatedLines;
        }
    }
}
