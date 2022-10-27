using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Regroup common data between receipt and invoice.
    /// </summary>
    public abstract class FinancialPredictionBase : PredictionBase
    {
        /// <summary>
        /// <see cref="Common.DocumentType"/>
        /// </summary>
        [JsonPropertyName("document_type")]
        public DocumentType DocumentType { get; set; }

        /// <summary>
        /// The date.
        /// </summary>
        [JsonPropertyName("date")]
        public Date Date { get; set; }

        /// <summary>
        /// The supplier name.
        /// </summary>
        [JsonPropertyName("supplier")]
        public StringField Supplier { get; set; }

        /// <summary>
        /// <see cref="Tax"/>
        /// </summary>
        [JsonPropertyName("taxes")]
        public List<Tax> Taxes { get; set; }
    }
}
