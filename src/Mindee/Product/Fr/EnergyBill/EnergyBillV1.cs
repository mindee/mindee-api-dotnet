using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.EnergyBill
{
    /// <summary>
    /// Energy Bill API version 1 inference prediction.
    /// </summary>
    [Endpoint("energy_bill_fra", "1")]
    public sealed class EnergyBillV1 : Inference<EnergyBillV1Document, EnergyBillV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<EnergyBillV1Document>))]
        public override Pages<EnergyBillV1Document> Pages { get; set; }
    }
}
