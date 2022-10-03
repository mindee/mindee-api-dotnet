using System.Threading.Tasks;
using Mindee.Infrastructure.Api.Commun;
using Mindee.Infrastructure.Api.Passport;

namespace Mindee.Infrastructure.Api
{
    internal partial class MindeeApi
    {
        /// <summary>
        /// Call the Mindee Passport API.
        /// </summary>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        /// <returns><see cref="PredictResponse{PassportPrediction}"/></returns>
        /// <exception cref="MindeeApiException"></exception>
        public Task<PredictResponse<PassportPrediction>> PredictPassportAsync(
            PredictParameter predictParameter)
        {
            return PredictAsync<PassportPrediction>(
                new Endpoint("passport", "1"),
                predictParameter);
        }
    }
}
