using System.IO;
using System.Threading.Tasks;
using Mapster;
using Mindee.Infrastructure.Api;
using Mindee.Parsing;
using Mindee.Parsing.Common;
using Mindee.Parsing.Invoice;

namespace Mindee.Infrastructure.Prediction
{
    internal class InvoiceParsing : IInvoiceParsing
    {
        private readonly MindeeApi _mindeeApi;

        public InvoiceParsing(MindeeApi mindeeApi)
        {
            _mindeeApi = mindeeApi;
        }

        async Task<InvoiceInference> IInvoiceParsing.ExecuteAsync(Stream file, string filename)
        {
            var response = await _mindeeApi.PredictInvoiceAsync(new PredictParameter(file, filename));

            return new InvoiceInference()
            {
                Inference = response.Document.Inference.Adapt<Inference<InvoicePrediction>>()
            };
        }
    }
}
