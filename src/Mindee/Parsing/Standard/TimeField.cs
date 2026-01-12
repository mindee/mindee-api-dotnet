using Mindee.Geometry;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    ///     Represent a time.
    /// </summary>
    public class TimeField : StringField
    {
        /// <summary>
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
        public TimeField(
            string value,
            double? confidence,
            Polygon polygon,
            int? pageId = null) : base(value, null, confidence, polygon, pageId)
        {
        }
    }
}
