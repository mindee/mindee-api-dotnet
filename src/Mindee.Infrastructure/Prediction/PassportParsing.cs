using System.Threading.Tasks;
using Mapster;
using Mindee.Infrastructure.Api;
using Mindee.Domain.Parsing;
using Mindee.Domain.Parsing.Common;
using Mindee.Domain.Parsing.Passport;

namespace Mindee.Infrastructure.Prediction
{
    internal class PassportParsing : IPassportParsing
    {
        private readonly MindeeApi _mindeeApi;

        public PassportParsing(MindeeApi mindeeApi)
        {
            _mindeeApi = mindeeApi;
        }

        async Task<PassportInference> IPassportParsing.ExecuteAsync(ParseParameter parseParameter)
        {
            var response = await _mindeeApi.PredictPassportAsync(
                new PredictParameter(
                    parseParameter.DocumentClient.File,
                    parseParameter.DocumentClient.Filename));

            return new PassportInference()
            {
                Inference = response.Document.Inference.Adapt<Inference<PassportPrediction>>()
            };
        }
    }
}
