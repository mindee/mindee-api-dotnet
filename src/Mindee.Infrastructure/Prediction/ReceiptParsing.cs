using System.IO;
using System.Threading.Tasks;
using Mapster;
using Mindee.Infrastructure.Api;
using Mindee.Prediction;
using Mindee.Prediction.Commun;
using Mindee.Prediction.Receipt;

namespace Mindee.Infrastructure.Prediction
{
    internal class ReceiptParsing : IReceiptParsing
    {
        private readonly MindeeApi _mindeeApi;

        public ReceiptParsing(MindeeApi mindeeApi)
        {
            _mindeeApi = mindeeApi;
        }

        async Task<ReceiptInference> IReceiptParsing.ExecuteAsync(Stream file, string filename)
        {
            var response = await _mindeeApi.PredictReceiptAsync(file, filename);

            return new ReceiptInference()
            {
                Inference = response.Document.Inference.Adapt<Inference<ReceiptPrediction>>()
            };
        }
    }
}
