namespace Mindee.V2.Parsing.Inference.Field
{
    /// <summary>
    ///     Comparison helpers for <see cref="FieldConfidence" /> values.
    /// </summary>
    public static class FieldConfidenceExtensions
    {
        /// <summary>
        ///     Returns <c>true</c> when <paramref name="value" /> is strictly greater than <paramref name="other" />.
        /// </summary>
        public static bool GreaterThan(this FieldConfidence value, FieldConfidence other)
        {
            return (int)value > (int)other;
        }

        /// <summary>
        ///     Returns <c>true</c> when <paramref name="value" /> is greater than or equal to <paramref name="other" />.
        /// </summary>
        public static bool GreaterThanOrEqual(this FieldConfidence value, FieldConfidence other)
        {
            return (int)value >= (int)other;
        }

        /// <summary>
        ///     Returns <c>true</c> when <paramref name="value" /> is strictly less than <paramref name="other" />.
        /// </summary>
        public static bool LessThan(this FieldConfidence value, FieldConfidence other)
        {
            return (int)value < (int)other;
        }

        /// <summary>
        ///     Returns <c>true</c> when <paramref name="value" /> is less than or equal to <paramref name="other" />.
        /// </summary>
        public static bool LessThanOrEqual(this FieldConfidence value, FieldConfidence other)
        {
            return (int)value <= (int)other;
        }
    }
}
