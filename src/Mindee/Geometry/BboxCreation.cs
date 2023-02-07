namespace Mindee.Geometry
{
    /// <summary>
    /// Bbox builder.
    /// </summary>
    public static class BboxCreation
    {
        /// <summary>
        /// Create from polygon.
        /// </summary>
        /// <param name="polygon"></param>
        public static Bbox Create(Polygon polygon)
        {
            if (polygon == null)
            {
                return null;
            }

            return new Bbox(
                polygon.GetMinXCoordinate(),
                polygon.GetMinXCoordinate(),
                polygon.GetMinYCoordinate(),
                polygon.GetMaxYCoordinate()
            );
        }
    }
}
