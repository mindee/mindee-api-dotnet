using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.V2.Parsing.Inference.Field
{
    /// <summary>
    ///     Base class for V2 fields.
    /// </summary>
    public class BaseField
    {
        /// <summary>
        ///     Base field.
        /// </summary>
        /// <param name="confidence">
        ///     <see cref="Confidence" />
        /// </param>
        /// <param name="locations">
        ///     <see cref="Locations" />
        /// </param>
        public BaseField(FieldConfidence? confidence, List<FieldLocation> locations)
        {
            Confidence = confidence;
            Locations = locations;
        }

        /// <summary>
        ///     Confidence associated with the field.
        /// </summary>
        [JsonPropertyName("confidence")]
        public FieldConfidence? Confidence { get; set; }

        /// <summary>
        ///     List of the location candidates for the value.
        /// </summary>
        [JsonPropertyName("locations")]
        public List<FieldLocation> Locations { get; set; }
    }
}
