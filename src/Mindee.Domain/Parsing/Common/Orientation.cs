namespace Mindee.Domain.Parsing.Common
{
    /// <summary>
    /// The orientation which was applied from the original page.
    /// </summary>
    public class Orientation
    {
        /// <summary>
        /// The confidence about the zone of the value extracted.
        /// A value from 0 to 1.
        /// </summary>
        /// <example>0.9</example>
        public double Confidence { get; set; }

        /// <summary>
        /// Degrees of the rotation
        /// </summary>
        /// <example>90</example>
        public int Degrees { get; set; }
    }
}
