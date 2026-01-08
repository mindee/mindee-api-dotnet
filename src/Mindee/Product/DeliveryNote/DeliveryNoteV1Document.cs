using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.DeliveryNote
{
    /// <summary>
    ///     Delivery note API version 1.2 document data.
    /// </summary>
    public class DeliveryNoteV1Document : IPrediction
    {
        /// <summary>
        ///     The address of the customer receiving the goods.
        /// </summary>
        [JsonPropertyName("customer_address")]
        public StringField CustomerAddress { get; set; }

        /// <summary>
        ///     The name of the customer receiving the goods.
        /// </summary>
        [JsonPropertyName("customer_name")]
        public StringField CustomerName { get; set; }

        /// <summary>
        ///     The date on which the delivery is scheduled to arrive.
        /// </summary>
        [JsonPropertyName("delivery_date")]
        public DateField DeliveryDate { get; set; }

        /// <summary>
        ///     A unique identifier for the delivery note.
        /// </summary>
        [JsonPropertyName("delivery_number")]
        public StringField DeliveryNumber { get; set; }

        /// <summary>
        ///     The address of the supplier providing the goods.
        /// </summary>
        [JsonPropertyName("supplier_address")]
        public StringField SupplierAddress { get; set; }

        /// <summary>
        ///     The name of the supplier providing the goods.
        /// </summary>
        [JsonPropertyName("supplier_name")]
        public StringField SupplierName { get; set; }

        /// <summary>
        ///     The total monetary value of the goods being delivered.
        /// </summary>
        [JsonPropertyName("total_amount")]
        public AmountField TotalAmount { get; set; }

        /// <summary>
        ///     A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($":Delivery Date: {DeliveryDate}\n");
            result.Append($":Delivery Number: {DeliveryNumber}\n");
            result.Append($":Supplier Name: {SupplierName}\n");
            result.Append($":Supplier Address: {SupplierAddress}\n");
            result.Append($":Customer Name: {CustomerName}\n");
            result.Append($":Customer Address: {CustomerAddress}\n");
            result.Append($":Total Amount: {TotalAmount}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
