using System.Threading.Tasks;
using Mindee.Domain.Parsing.Common;
using Mindee.Domain.Parsing.Passport;

namespace Mindee.Domain.Parsing
{
    public partial class MindeeApi
    {
        /// <summary>
        /// Call the Mindee Passport API.
        /// </summary>
        /// <param name="predictParameter"><see cref="PredictParameter"/></param>
        /// <returns><see cref="PredictResponse{PassportPrediction}"/></returns>
        /// <exception cref="MindeeApiException"></exception>
        public Task<Document<PassportPrediction>> PredictPassportAsync(
            PredictParameter predictParameter)
        {
            return PredictAsync<PassportPrediction>(
                new Endpoint("passport", "1"),
                predictParameter);
        }
    }
}
