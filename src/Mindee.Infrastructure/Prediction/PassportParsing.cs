using System.IO;
using System.Threading.Tasks;
using Mapster;
using Mindee.Infrastructure.Api;
using Mindee.Prediction;
using Mindee.Prediction.Commun;
using Mindee.Prediction.Passport;

namespace Mindee.Infrastructure.Prediction
{
    internal class PassportParsing : IPassportParsing
    {
        private readonly MindeeApi _mindeeApi;

        public PassportParsing(MindeeApi mindeeApi)
        {
            _mindeeApi = mindeeApi;
        }

        async Task<PassportInference> IPassportParsing.ExecuteAsync(Stream file, string filename)
        {
            var response = await _mindeeApi.PredictPassportAsync(new PredictParameter(file, filename));

            return new PassportInference()
            {
                Inference = response.Document.Inference.Adapt<Inference<PassportPrediction>>()
            };
        }
    }
}
