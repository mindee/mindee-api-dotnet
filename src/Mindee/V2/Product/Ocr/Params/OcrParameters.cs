using System.Collections.Generic;
using Mindee.V2.ClientOptions;

namespace Mindee.V2.Product.Ocr.Params
{
    /// <summary>
    ///     Parameters for sending a file to a Raw Text (OCR) product.
    /// </summary>
    [ProductAttributes("ocr")]
    public class OcrParameters : BaseProductParameters
    {

        /// <summary>
        /// OCR parameters to set when sending a file.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="alias"></param>
        /// <param name="webhookIds"></param>
        public OcrParameters(
            string modelId,
            string alias = null,
            List<string> webhookIds = null) : base(modelId, alias, webhookIds) { }
    }
}
