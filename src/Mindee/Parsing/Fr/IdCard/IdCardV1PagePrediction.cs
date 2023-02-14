using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Fr.IdCard
{
    /// <summary>
    /// Page data for Carte Nationale d'Identité, API version 1.
    /// </summary>
    public sealed class IdCardV1PagePrediction : IdCardV1DocumentPrediction
    {
        /// <summary>
        /// The side of the document which is visible.
        /// </summary>
        [JsonPropertyName("document_side")]
        public StringField DocumentSide { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Document Side: {DocumentSide}\n");
            result.Append(base.ToString());

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
