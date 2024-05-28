using System;
using System.Collections.Generic;
using System.Linq;
using Mindee.Geometry;

namespace Mindee.Parsing.Custom.LineItem
{
    /// <summary>
    /// Line items generator.
    /// </summary>
    public static class LineItemsGenerator
    {
        /// <summary>
        /// Prepare the line of a table.
        /// </summary>
        /// <param name="fields">The list of fields that will be transform to line items.</param>
        /// <param name="anchor">
        /// The line items generation is based on the fact that
        /// this field is most present that the others one, on all lines that we want.
        /// </param>
        /// <exception cref="InvalidOperationException"></exception>
        internal static IEnumerable<Line> GetPreparedLines(
            Dictionary<string, ListField> fields,
            Anchor anchor
            )
        {
            var anchorField = fields[anchor.Name];
            var lines = new Dictionary<int, Line>();

            if (anchorField == null)
            {
                throw new InvalidOperationException("The field selected for the anchor was not found.");
            }

            if (!anchorField.Values.Any()) return lines.Values;

            // If anchor words are not sorted based on their Y value, strange things will happen.
            anchorField.Values.Sort(delegate (ListFieldValue a, ListFieldValue b)
                {
                    return a.Polygon.GetMinY().CompareTo(b.Polygon.GetMinY());
                });

            var lineNumber = 1;
            Line currentLine = new Line(
                rowNumber: lineNumber,
                heightTolerance: anchor.Tolerance,
                anchorBoundingBox: anchorField.Values.First().Polygon);

            for (int i = 0; i < anchorField.Values.Count; i++)
            {
                Polygon currentPolygon = anchorField.Values[i].Polygon;
                Point currentCentroid = currentPolygon.GetCentroid();

                // The last word in the list obviously won't have a word after it
                Polygon nextPolygon = (i >= anchorField.Values.Count - 1)
                    ? null : anchorField.Values[i + 1].Polygon;

                // If we don't do this first, the last word never gets added
                // There's no harm in adding the same polygon multiple times
                currentLine.AddToAnchorBoundingBox(currentPolygon);

                // Create a new line if the next word is not on the same line.
                bool isPointInY = nextPolygon != null && nextPolygon.IsPointInY(currentCentroid);
                if (!isPointInY)
                {
                    lines.Add(lineNumber, currentLine);
                    if (nextPolygon != null)
                    {
                        lineNumber++;
                        currentLine = new Line(
                            rowNumber: lineNumber,
                            heightTolerance: anchor.Tolerance,
                            anchorBoundingBox: nextPolygon);
                    }
                }
            }
            return lines.Values;
        }

        /// <summary>
        /// Generate lines items from a list of names and the fields from API Builder response.
        /// </summary>
        /// <param name="fieldNames">The names of the fields that need to be transformed into lines.</param>
        /// <param name="fields">The list of the fields from the API Builder.</param>
        /// <param name="anchor"><see cref="Anchor"/></param>
        public static LineItems Generate(
            Anchor anchor,
            List<string> fieldNames,
            Dictionary<string, ListField> fields
            )
        {
            Dictionary<string, ListField> fieldsDict = fields
                  .Where(field => fieldNames.Contains(field.Key))
                  .ToDictionary(field => field.Key, field => field.Value);

            IEnumerable<Line> preparedLines = GetPreparedLines(fieldsDict, anchor);
            IEnumerable<Line> lines = PopulateLines(fieldsDict, preparedLines);

            return new LineItems(lines, fieldNames);
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
                        if (currentLine.AnchorBoundingBox.IsPointInY(
                            point: listFieldValue.Polygon.GetCentroid())
                        )
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
