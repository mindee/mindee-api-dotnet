using System.Threading.Tasks;
using Mindee.Parsing.Common;
using Mindee.Parsing.Passport;

namespace Mindee.Parsing
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
