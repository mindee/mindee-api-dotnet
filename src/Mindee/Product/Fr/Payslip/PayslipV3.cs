using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    /// Payslip API version 3 inference prediction.
    /// </summary>
    [Endpoint("payslip_fra", "3")]
    public sealed class PayslipV3 : Inference<PayslipV3Document, PayslipV3Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<PayslipV3Document>))]
        public override Pages<PayslipV3Document> Pages { get; set; }
    }
}
