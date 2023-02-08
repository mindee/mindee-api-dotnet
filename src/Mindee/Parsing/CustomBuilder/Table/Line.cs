using System.Collections.Generic;
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
        /// The Bbox of the line.
        /// </summary>
        public Bbox Bbox { get; }

        /// <summary>
        /// Height tolerance which define a line.
        /// </summary>
        public double HeightTolerance { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rowNumber"><see cref="RowNumber"/></param>
        /// <param name="heightTolerance"><see cref="HeightTolerance"/></param>
        public Line(int rowNumber, double heightTolerance)
        {
            RowNumber = rowNumber;
            Fields = new Dictionary<string, StringField>();
            Bbox = new Bbox(1, 0, 1, 0);
            HeightTolerance = heightTolerance;
        }

        /// <summary>
        /// Update the current Bbox value.
        /// </summary>
        /// <param name="bbox">New Bbox to merge with.</param>
        public void UpdateBbox(Bbox bbox)
        {
            this.Bbox.Merge(bbox);
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

                var mergedBoundingBox = BoundingBoxCreation.Create(
                    new List<Polygon>()
                    {
                        BoundingBoxCreation.Create(existingField.Polygon),
                        BoundingBoxCreation.Create(fieldValue.Polygon)
                    });

                string content = existingField.Value == null ?
                  fieldValue.Content :
                  string.Join(" ", existingField.Value, fieldValue.Content
                  );

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
    }
}
