namespace Mindee.Geometry
{
    /// <summary>
    /// A field with position data.
    /// </summary>
    public interface IPositionDataField
    {
        /// <summary>
        ///     Coordinates for the found value.
        /// </summary>
        public Polygon Polygon { get; set; }
    }
}
