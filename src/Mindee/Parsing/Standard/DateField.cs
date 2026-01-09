using System;
using Microsoft.Extensions.Logging;
using Mindee.Geometry;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    ///     Represent a date.
    /// </summary>
    public class DateField : StringField
    {
        /// <summary>
        ///     Default constructor.
        /// </summary>
        /// <param name="value">
        ///     <see cref="StringField.Value" />
        /// </param>
        /// <param name="confidence">
        ///     <see cref="BaseField.Confidence" />
        /// </param>
        /// <param name="polygon">
        ///     <see cref="BaseField.Polygon" />
        /// </param>
        /// <param name="pageId">
        ///     <see cref="BaseField.PageId" />
        /// </param>
        /// <param name="isComputed">
        ///     <see cref="BaseField.PageId" />
        /// </param>
        public DateField(
            string value,
            double? confidence,
            Polygon polygon,
            int? pageId = null,
            bool? isComputed = null
        ) : base(value, null, confidence, polygon, pageId)
        {
            var logger = MindeeLogger.GetLogger();
            IsComputed = isComputed;

            if (string.IsNullOrEmpty(Value))
            {
                return;
            }

            try
            {
                DateObject = DateTime.Parse(Value);
            }
            catch (FormatException)
            {
                logger?.LogWarning("Unable to parse the date: {}", Value);
            }
        }

        /// <summary>
        ///     The value of the field as a <see cref="DateTime" /> object.
        /// </summary>
        public DateTime? DateObject { get; set; }

        /// <summary>
        ///     Whether the field was computed or retrieved directly from the document.
        /// </summary>
        public bool? IsComputed { get; set; }
    }
}
