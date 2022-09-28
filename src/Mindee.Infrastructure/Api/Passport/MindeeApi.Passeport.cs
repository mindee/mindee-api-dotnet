using System.IO;
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
        /// <param name="file"></param>
        /// <param name="filename"></param>
        /// <returns><see cref="PredictResponse{PassportPrediction}"/></returns>
        /// <exception cref="MindeeApiException"></exception>
        public Task<PredictResponse<PassportPrediction>> PredictPassportAsync(
            Stream file,
            string filename)
        {
            return PredictAsync<PassportPrediction>(
                new Credential("passport", "1"),
                file,
                filename);
        }
    }
}
