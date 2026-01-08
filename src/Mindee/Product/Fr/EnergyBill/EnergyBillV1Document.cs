using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.EnergyBill
{
    /// <summary>
    ///     Energy Bill API version 1.2 document data.
    /// </summary>
    public class EnergyBillV1Document : IPrediction
    {
        /// <summary>
        ///     The unique identifier associated with a specific contract.
        /// </summary>
        [JsonPropertyName("contract_id")]
        public StringField ContractId { get; set; }

        /// <summary>
        ///     The unique identifier assigned to each electricity or gas consumption point. It specifies the exact location where
        ///     the energy is delivered.
        /// </summary>
        [JsonPropertyName("delivery_point")]
        public StringField DeliveryPoint { get; set; }

        /// <summary>
        ///     The date by which the payment for the energy invoice is due.
        /// </summary>
        [JsonPropertyName("due_date")]
        public DateField DueDate { get; set; }

        /// <summary>
        ///     The entity that consumes the energy.
        /// </summary>
        [JsonPropertyName("energy_consumer")]
        public EnergyBillV1EnergyConsumer EnergyConsumer { get; set; }

        /// <summary>
        ///     The company that supplies the energy.
        /// </summary>
        [JsonPropertyName("energy_supplier")]
        public EnergyBillV1EnergySupplier EnergySupplier { get; set; }

        /// <summary>
        ///     Details of energy consumption.
        /// </summary>
        [JsonPropertyName("energy_usage")]
        [JsonConverter(typeof(ObjectListJsonConverter<EnergyBillV1EnergyUsages, EnergyBillV1EnergyUsage>))]
        public EnergyBillV1EnergyUsages EnergyUsage { get; set; }

        /// <summary>
        ///     The date when the energy invoice was issued.
        /// </summary>
        [JsonPropertyName("invoice_date")]
        public DateField InvoiceDate { get; set; }

        /// <summary>
        ///     The unique identifier of the energy invoice.
        /// </summary>
        [JsonPropertyName("invoice_number")]
        public StringField InvoiceNumber { get; set; }

        /// <summary>
        ///     Information about the energy meter.
        /// </summary>
        [JsonPropertyName("meter_details")]
        public EnergyBillV1MeterDetail MeterDetails { get; set; }

        /// <summary>
        ///     The subscription details fee for the energy service.
        /// </summary>
        [JsonPropertyName("subscription")]
        [JsonConverter(typeof(ObjectListJsonConverter<EnergyBillV1Subscriptions, EnergyBillV1Subscription>))]
        public EnergyBillV1Subscriptions Subscription { get; set; }

        /// <summary>
        ///     Details of Taxes and Contributions.
        /// </summary>
        [JsonPropertyName("taxes_and_contributions")]
        [JsonConverter(
            typeof(ObjectListJsonConverter<EnergyBillV1TaxesAndContributions, EnergyBillV1TaxesAndContribution>))]
        public EnergyBillV1TaxesAndContributions TaxesAndContributions { get; set; }

        /// <summary>
        ///     The total amount to be paid for the energy invoice.
        /// </summary>
        [JsonPropertyName("total_amount")]
        public AmountField TotalAmount { get; set; }

        /// <summary>
        ///     The total amount to be paid for the energy invoice before taxes.
        /// </summary>
        [JsonPropertyName("total_before_taxes")]
        public AmountField TotalBeforeTaxes { get; set; }

        /// <summary>
        ///     Total of taxes applied to the invoice.
        /// </summary>
        [JsonPropertyName("total_taxes")]
        public AmountField TotalTaxes { get; set; }

        /// <summary>
        ///     A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($":Invoice Number: {InvoiceNumber}\n");
            result.Append($":Contract ID: {ContractId}\n");
            result.Append($":Delivery Point: {DeliveryPoint}\n");
            result.Append($":Invoice Date: {InvoiceDate}\n");
            result.Append($":Due Date: {DueDate}\n");
            result.Append($":Total Before Taxes: {TotalBeforeTaxes}\n");
            result.Append($":Total Taxes: {TotalTaxes}\n");
            result.Append($":Total Amount: {TotalAmount}\n");
            result.Append($":Energy Supplier:{EnergySupplier.ToFieldList()}");
            result.Append($":Energy Consumer:{EnergyConsumer.ToFieldList()}");
            result.Append($":Subscription:{Subscription}");
            result.Append($":Energy Usage:{EnergyUsage}");
            result.Append($":Taxes and Contributions:{TaxesAndContributions}");
            result.Append($":Meter Details:{MeterDetails.ToFieldList()}");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
