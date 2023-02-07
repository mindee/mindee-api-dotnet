﻿using Mindee.Geometry;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent a date.
    /// </summary>
    public class DateField : StringField
    {
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
        }
    }
}
