using System.IO;
using System.Threading.Tasks;
using Mapster;
using Mindee.Infrastructure.Api;
using Mindee.Prediction;
using Mindee.Prediction.Commun;
using Mindee.Prediction.Invoice;

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
            var response = await _mindeeApi.PredictInvoiceAsync(file, filename);

            return new InvoiceInference()
            {
                Inference = response.Document.Inference.Adapt<Inference<InvoicePrediction>>()
            };
        }
    }
}
