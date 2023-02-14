using Mindee.Parsing.Common;

namespace Mindee.Parsing.Passport
{
    /// <summary>
    /// The invoice v4 definition.
    /// </summary>
    [Endpoint("passport", "1")]
    public class PassportV1Inference : Inference<PassportV1DocumentPrediction, PassportV1DocumentPrediction>
    {
    }
}
