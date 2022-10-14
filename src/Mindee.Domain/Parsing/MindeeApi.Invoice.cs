﻿using System.Threading.Tasks;
using Mindee.Domain.Parsing.Common;
using Mindee.Domain.Parsing.Invoice;

namespace Mindee.Domain.Parsing
{
    public partial class MindeeApi
    {
        /// <summary>
        /// Call the Mindee Invoice API.
        /// </summary>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        /// <returns><see cref="PredictResponse{InvoicePrediction}"/></returns>
        /// <exception cref="MindeeApiException"></exception>
        public Task<Document<InvoicePrediction>> PredictInvoiceAsync(
            PredictParameter predictParameter)
        {
            return PredictAsync<InvoicePrediction>(
                new Endpoint("invoices", "3"),
                predictParameter);
        }
    }
}
