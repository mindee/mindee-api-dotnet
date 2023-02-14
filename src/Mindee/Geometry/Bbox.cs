namespace Mindee.Geometry
{
    /// <summary>
    /// Define the max and min coordinates.
    /// </summary>
    /// <remarks>https://wiki.openstreetmap.org/wiki/Bounding_Box</remarks>
    public class Bbox
    {
        /// <summary>
        /// The x min coordinate.
        /// </summary>
        public double MinX { get; private set; }

        /// <summary>
        /// The x max coordinate.
        /// </summary>
        public double MaxX { get; private set; }

        /// <summary>
        /// The y min coordinate.
        /// </summary>
        public double MinY { get; private set; }

        /// <summary>
        /// The y max coordinate.
        /// </summary>
        public double MaxY { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="minX"><see cref="MinX"/></param>
        /// <param name="maxX"><see cref="MaxX"/></param>
        /// <param name="minY"><see cref="MinY"/></param>
        /// <param name="maxY"><see cref="MaxY"/></param>
        public Bbox(double minX, double maxX, double minY, double maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        /// <summary>
        /// Merge the current Bbox with another one.
        /// </summary>
        /// <param name="bbox">The bbox to merge with.</param>
        public void Merge(Bbox bbox)
        {
            if (MinX > bbox.MinX)
            {
                MinX = bbox.MinX;
            }

            if (MaxX < bbox.MaxX)
            {
                MaxX = bbox.MaxX;
            }

            if (MinY > bbox.MinY)
            {
                MinY = bbox.MinY;
            }

            if (MaxY < bbox.MaxY)
            {
                MaxY = bbox.MaxY;
            }
        }
    }
}
