using System.Runtime.Serialization;
using Mindee.Input;

namespace Mindee.Parsing.V2.Field
{

    /// <summary>
    /// Confidence level of a field as returned by the V2 API.
    /// </summary>
    /// <remarks>
    /// The backend returns the values as strings (e.g. <c>"High"</c>).
    /// The <see cref="StringEnumConverter{T}"/> together with the
    /// <see cref="EnumMemberAttribute"/> annotations makes sure the
    /// values are correctly (de)serialised.
    /// </remarks>
    public enum FieldConfidence
    {
        /// <summary>100 % confidence.</summary>
        [EnumMember(Value = "Certain")]
        Certain,

        /// <summary>Very high confidence.</summary>
        [EnumMember(Value = "High")]
        High,

        /// <summary>Medium confidence.</summary>
        [EnumMember(Value = "Medium")]
        Medium,

        /// <summary>Low confidence.</summary>
        [EnumMember(Value = "Low")]
        Low
    }

}
