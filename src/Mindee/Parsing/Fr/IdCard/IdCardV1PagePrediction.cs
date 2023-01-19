using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Fr.IdCard
{
    /// <summary>
    /// The french id card model for the v1.
    /// </summary>
    public sealed class IdCardV1PagePrediction : IdCardV1DocumentPrediction
    {
        /// <summary>
        /// Indicates if it is the recto side, the verso side, or both.
        /// </summary>
        [JsonPropertyName("document_side")]
        public StringField DocumentSide { get; set; }

        /// <summary>
        /// A prettier reprensentation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Document side: {DocumentSide}\n");
            result.Append(base.ToString());

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
