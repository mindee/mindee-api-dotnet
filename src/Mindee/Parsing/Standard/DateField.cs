using System;
using Mindee.Geometry;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    /// Represent a date.
    /// </summary>
    public class DateField : StringField
    {
        /// <summary>
        /// The value of the field as a <see cref="DateTime"/> object.
        /// </summary>
        public DateTime? DateObject { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"><see cref="StringField.Value"/></param>
        /// <param name="confidence"><see cref="BaseField.Confidence"/></param>
        /// <param name="polygon"><see cref="BaseField.Polygon"/></param>
        /// <param name="pageId"><see cref="BaseField.PageId"/></param>
        public DateField(
            string value,
            double confidence,
            Polygon polygon,
            int? pageId = null) : base(value, confidence, polygon, pageId)
        {
            if (!String.IsNullOrEmpty(Value))
            {
                try
                {
                    DateObject = DateTime.Parse(Value);
                }
                catch (FormatException)
                {
                    // This is fine. Everything is fine. How are you?
                }
            }
        }
    }
}
