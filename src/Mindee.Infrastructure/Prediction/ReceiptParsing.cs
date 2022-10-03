using System.Threading.Tasks;
using Mapster;
using Mindee.Infrastructure.Api;
using Mindee.Domain.Parsing;
using Mindee.Domain.Parsing.Common;
using Mindee.Domain.Parsing.Receipt;

namespace Mindee.Infrastructure.Prediction
{
    internal class ReceiptParsing : IReceiptParsing
    {
        private readonly MindeeApi _mindeeApi;

        public ReceiptParsing(MindeeApi mindeeApi)
        {
            _mindeeApi = mindeeApi;
        }

        async Task<ReceiptInference> IReceiptParsing.ExecuteAsync(ParseParameter parseParameter)
        {
            var response = await _mindeeApi.PredictReceiptAsync(
                new PredictParameter(
                    parseParameter.DocumentClient.File,
                    parseParameter.DocumentClient.Filename));

            return new ReceiptInference()
            {
                Inference = response.Document.Inference.Adapt<Inference<ReceiptPrediction>>()
            };
        }
    }
}
