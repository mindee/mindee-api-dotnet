using System.Threading.Tasks;
using Mapster;
using Mindee.Infrastructure.Api;
using Mindee.Domain.Parsing;
using Mindee.Domain.Parsing.Common;
using Mindee.Domain.Parsing.Invoice;

namespace Mindee.Infrastructure.Prediction
{
    internal class InvoiceParsing : IInvoiceParsing
    {
        private readonly MindeeApi _mindeeApi;

        public InvoiceParsing(MindeeApi mindeeApi)
        {
            _mindeeApi = mindeeApi;
        }

        async Task<InvoiceInference> IInvoiceParsing.ExecuteAsync(ParseParameter parseParameter)
        {
            var response = await _mindeeApi.PredictInvoiceAsync(
                new PredictParameter(
                    parseParameter.DocumentClient.File,
                    parseParameter.DocumentClient.Filename));

            return new InvoiceInference()
            {
                Inference = response.Document.Inference.Adapt<Inference<InvoicePrediction>>()
            };
        }
    }
}
