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
        public DateField Date { get; set; }

        /// <summary>
        /// <see cref="Tax"/>
        /// </summary>
        [JsonPropertyName("taxes")]
        public List<Tax> Taxes { get; set; }

        /// <summary>
        /// Total amount including taxes.
        /// </summary>
        [JsonPropertyName("total_amount")]
        public AmountField TotalAmount { get; set; }

        /// <summary>
        /// Total amount excluding taxes.
        /// </summary>
        [JsonPropertyName("total_net")]
        public AmountField TotalNet { get; set; }
    }
}
