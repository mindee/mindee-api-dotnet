using System.IO;
using System.Threading.Tasks;
using Mapster;
using Mindee.Infrastructure.Api;
using Mindee.Prediction;
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

        async Task<PassportPrediction> IPassportParsing.ExecuteAsync(Stream file, string filename)
        {
            var response = await _mindeeApi.PredictPassportAsync(file, filename);

            return response.Document.Inference.Prediction.Adapt<PassportPrediction>();
        }
    }
}
