using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Receipt
{
    /// <summary>
    ///     Document data for Receipt, API version 4.
    /// </summary>
    public sealed class ReceiptV4Document : IPrediction
    {
        /// <summary>
        ///     <see cref="ClassificationField" />
        /// </summary>
        [JsonPropertyName("document_type")]
        public ClassificationField DocumentType { get; set; }

        /// <summary>
        ///     The date.
        /// </summary>
        [JsonPropertyName("date")]
        public DateField Date { get; set; }

        /// <summary>
        ///     <see cref="Locale" />
        /// </summary>
        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }

        /// <summary>
        ///     <see cref="Tax" />
        /// </summary>
        [JsonPropertyName("taxes")]
        [JsonConverter(typeof(ObjectListJsonConverter<Taxes, Tax>))]
        public Taxes Taxes { get; set; }

        /// <summary>
        ///     Total amount including taxes.
        /// </summary>
        [JsonPropertyName("total_amount")]
        public AmountField TotalAmount { get; set; }

        /// <summary>
        ///     Total amount excluding taxes.
        /// </summary>
        [JsonPropertyName("total_net")]
        public AmountField TotalNet { get; set; }

        /// <summary>
        ///     The category of the receipt.
        /// </summary>
        [JsonPropertyName("category")]
        public StringField Category { get; set; }

        /// <summary>
        ///     The subcategory of the receipt.
        /// </summary>
        [JsonPropertyName("subcategory")]
        public StringField SubCategory { get; set; }

        /// <summary>
        ///     The supplier name.
        /// </summary>
        [JsonPropertyName("supplier")]
        public StringField Supplier { get; set; }

        /// <summary>
        ///     <see cref="Time" />
        /// </summary>
        [JsonPropertyName("time")]
        public TimeField Time { get; set; }

        /// <summary>
        ///     Total taxes.
        /// </summary>
        [JsonPropertyName("total_tax")]
        public AmountField TotalTax { get; set; }

        /// <summary>
        ///     The tip.
        /// </summary>
        [JsonPropertyName("tip")]
        public AmountField Tip { get; set; }

        /// <summary>
        ///     Generate a pretty to read summary of the model.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($":Locale: {Locale}\n");
            result.Append($":Date: {Date.Value}\n");
            result.Append($":Category: {Category}\n");
            result.Append($":Subcategory: {SubCategory}\n");
            result.Append($":Document type: {DocumentType}\n");
            result.Append($":Time: {Time.Value}\n");
            result.Append($":Supplier name: {Supplier}\n");
            result.Append($":Taxes:{Taxes}");
            result.Append($":Total net: {TotalNet}\n");
            result.Append($":Total tax: {TotalTax}\n");
            result.Append($":Tip: {Tip}\n");
            result.Append($":Total amount: {TotalAmount}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
