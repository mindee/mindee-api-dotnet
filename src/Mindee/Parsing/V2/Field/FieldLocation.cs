using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    ///     Location for fields.
    /// </summary>
    public class FieldLocation
    {
        /// <summary>
        ///     Coordinates for the found value.
        /// </summary>
        [JsonPropertyName("polygon")]
        [JsonConverter(typeof(PolygonJsonConverter))]
        public Polygon Polygon { get; set; }

        /// <summary>
        ///     ID of the page the coordinates were found on.
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }


        /// <summary>
        ///     String representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Polygon.ToString();
        }
    }
}
