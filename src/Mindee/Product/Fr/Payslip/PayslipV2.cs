using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    /// Payslip API version 2 inference prediction.
    /// </summary>
    [Endpoint("payslip_fra", "2")]
    public sealed class PayslipV2 : Inference<PayslipV2Document, PayslipV2Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<PayslipV2Document>))]
        public override Pages<PayslipV2Document> Pages { get; set; }
    }
}
