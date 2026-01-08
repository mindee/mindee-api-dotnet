using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.BillOfLading
{
    /// <summary>
    ///     Bill of Lading API version 1.1 document data.
    /// </summary>
    public class BillOfLadingV1Document : IPrediction
    {
        /// <summary>
        ///     A unique identifier assigned to a Bill of Lading document.
        /// </summary>
        [JsonPropertyName("bill_of_lading_number")]
        public StringField BillOfLadingNumber { get; set; }

        /// <summary>
        ///     The shipping company responsible for transporting the goods.
        /// </summary>
        [JsonPropertyName("carrier")]
        public BillOfLadingV1Carrier Carrier { get; set; }

        /// <summary>
        ///     The goods being shipped.
        /// </summary>
        [JsonPropertyName("carrier_items")]
        [JsonConverter(typeof(ObjectListJsonConverter<BillOfLadingV1CarrierItems, BillOfLadingV1CarrierItem>))]
        public BillOfLadingV1CarrierItems CarrierItems { get; set; }

        /// <summary>
        ///     The party to whom the goods are being shipped.
        /// </summary>
        [JsonPropertyName("consignee")]
        public BillOfLadingV1Consignee Consignee { get; set; }

        /// <summary>
        ///     The date when the bill of lading is issued.
        /// </summary>
        [JsonPropertyName("date_of_issue")]
        public DateField DateOfIssue { get; set; }

        /// <summary>
        ///     The date when the vessel departs from the port of loading.
        /// </summary>
        [JsonPropertyName("departure_date")]
        public DateField DepartureDate { get; set; }

        /// <summary>
        ///     The party to be notified of the arrival of the goods.
        /// </summary>
        [JsonPropertyName("notify_party")]
        public BillOfLadingV1NotifyParty NotifyParty { get; set; }

        /// <summary>
        ///     The place where the goods are to be delivered.
        /// </summary>
        [JsonPropertyName("place_of_delivery")]
        public StringField PlaceOfDelivery { get; set; }

        /// <summary>
        ///     The port where the goods are unloaded from the vessel.
        /// </summary>
        [JsonPropertyName("port_of_discharge")]
        public StringField PortOfDischarge { get; set; }

        /// <summary>
        ///     The port where the goods are loaded onto the vessel.
        /// </summary>
        [JsonPropertyName("port_of_loading")]
        public StringField PortOfLoading { get; set; }

        /// <summary>
        ///     The party responsible for shipping the goods.
        /// </summary>
        [JsonPropertyName("shipper")]
        public BillOfLadingV1Shipper Shipper { get; set; }

        /// <summary>
        ///     A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($":Bill of Lading Number: {BillOfLadingNumber}\n");
            result.Append($":Shipper:{Shipper.ToFieldList()}");
            result.Append($":Consignee:{Consignee.ToFieldList()}");
            result.Append($":Notify Party:{NotifyParty.ToFieldList()}");
            result.Append($":Carrier:{Carrier.ToFieldList()}");
            result.Append($":Items:{CarrierItems}");
            result.Append($":Port of Loading: {PortOfLoading}\n");
            result.Append($":Port of Discharge: {PortOfDischarge}\n");
            result.Append($":Place of Delivery: {PlaceOfDelivery}\n");
            result.Append($":Date of issue: {DateOfIssue}\n");
            result.Append($":Departure Date: {DepartureDate}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
