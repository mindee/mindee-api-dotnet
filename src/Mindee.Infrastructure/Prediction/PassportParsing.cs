using System.Threading.Tasks;
using Mapster;
using Mindee.Domain.Parsing;
using Mindee.Domain.Parsing.Common;
using Mindee.Domain.Parsing.Passport;
using MindeeApi = Mindee.Infrastructure.Api.MindeeApi;
using PredictParameter = Mindee.Infrastructure.Api.PredictParameter;

namespace Mindee.Infrastructure.Prediction
{
    internal class PassportParsing : IPassportParsing
    {
        private readonly MindeeApi _mindeeApi;

        public PassportParsing(MindeeApi mindeeApi)
        {
            _mindeeApi = mindeeApi;
        }

        async Task<Document<PassportPrediction>> IPassportParsing.ExecuteAsync(ParseParameter parseParameter)
        {
            var response = await _mindeeApi.PredictPassportAsync(
                new PredictParameter(
                    parseParameter.DocumentClient.File,
                    parseParameter.DocumentClient.Filename,
                    parseParameter.WithFullText));

            return new Document<PassportPrediction>()
            {
                Inference = response.Document.Inference.Adapt<Inference<PassportPrediction>>(),
                Ocr = response.Document.Ocr.Adapt<Ocr>()
            };
        }
    }
}
