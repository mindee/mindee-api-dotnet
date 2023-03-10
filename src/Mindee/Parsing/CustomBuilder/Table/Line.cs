using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindee.Geometry;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.CustomBuilder.Table
{
    /// <summary>
    /// Define a line.
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Number of the current line.
        /// </summary>
        public int RowNumber { get; }

        /// <summary>
        /// All the fields identify by their name.
        /// </summary>
        public Dictionary<string, StringField> Fields { get; }

        /// <summary>
        /// The BoundingBox of the anchor, which is used to determine if a word is on this line.
        /// </summary>
        public Polygon AnchorBoundingBox { get; set; }

        /// <summary>
        /// Height tolerance which define a line.
        /// </summary>
        public double HeightTolerance { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rowNumber"><see cref="RowNumber"/></param>
        /// <param name="heightTolerance"><see cref="HeightTolerance"/></param>
        /// /// <param name="anchorBoundingBox"><see cref="AnchorBoundingBox"/></param>
        public Line(int rowNumber, double heightTolerance, Polygon anchorBoundingBox)
        {
            RowNumber = rowNumber;
            Fields = new Dictionary<string, StringField>();
            AnchorBoundingBox = anchorBoundingBox;
            HeightTolerance = heightTolerance;
        }

        /// <summary>
        /// Add a polygon to the anchor bounding box.
        /// </summary>
        /// <param name="boundingBox">New Polygon to merge with.</param>
        public void AddToAnchorBoundingBox(Polygon boundingBox)
        {
            List<Polygon> polygons = new List<Polygon> { AnchorBoundingBox, boundingBox };
            AnchorBoundingBox = Utils.BoundingBoxFromPolygons(polygons);
        }

        /// <summary>
        /// Get the bounding box of the entire line, encompassing all words.
        /// </summary>
        /// <returns></returns>
        public Polygon GetBoundingBox()
        {
            List<Polygon> polygons = Fields
                .Select(field => field.Value.Polygon)
                .ToList();
            return Utils.BoundingBoxFromPolygons(polygons);
        }

        /// <summary>
        /// Add or update the given field to the current line.
        /// </summary>
        /// <param name="name">Name of the field.</param>
        /// <param name="fieldValue">Values to add.</param>
        public void UpdateField(string name, ListFieldValue fieldValue)
        {
            if (Fields.ContainsKey(name))
            {
                StringField existingField = Fields[name];

                var mergedBoundingBox = Utils.BoundingBoxFromPolygons(
                    new List<Polygon>()
                    {
                        Utils.BoundingBoxFromPolygon(existingField.Polygon),
                        Utils.BoundingBoxFromPolygon(fieldValue.Polygon)
                    });

                string content = existingField.Value == null ?
                  fieldValue.Content :
                  string.Join(" ", existingField.Value, fieldValue.Content);

                Fields.Remove(name);
                Fields.Add(
                  name,
                  new StringField(
                    content,
                    existingField.Confidence * fieldValue.Confidence,
                    mergedBoundingBox));
            }
            else
            {
                Fields.Add(name,
                    new StringField(
                        fieldValue.Content,
                        fieldValue.Confidence,
                        fieldValue.Polygon)
                    );
            }
        }

        /// <summary>
        /// The default string representation.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("");

            foreach (var field in Fields)
            {
                result.Append($"{field.Key}: {field.Value}\n");
            }
            return result.ToString();
        }
    }
}
