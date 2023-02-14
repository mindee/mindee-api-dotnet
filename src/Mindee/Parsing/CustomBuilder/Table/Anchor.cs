namespace Mindee.Parsing.CustomBuilder.Table
{
    /// <summary>
    /// An anchor used to detect each lines.
    /// </summary>
    public class Anchor
    {
        /// <summary>
        /// Name of the field which is an anchor.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The error tolerance when computing.
        /// </summary>
        /// <remarks>0.001d by default.</remarks>
        public double Tolerance { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="tolerance"><see cref="Tolerance"/></param>
        public Anchor(string name, double tolerance = 0.001d)
        {
            Name = name;
            Tolerance = tolerance;
        }
    }
}
