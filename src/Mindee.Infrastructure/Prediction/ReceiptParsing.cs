using System.IO;
using System.Threading.Tasks;
using Mapster;
using Mindee.Infrastructure.Api;
using Mindee.Prediction;
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

        async Task<ReceiptPrediction> IReceiptParsing.ExecuteAsync(Stream file, string filename)
        {
            var response = await _mindeeApi.PredictReceiptAsync(file, filename);

            return response.Document.Inference.Prediction.Adapt<ReceiptPrediction>();
        }
    }
}
