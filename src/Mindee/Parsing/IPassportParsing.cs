using System.Threading.Tasks;
using Mindee.Parsing.Common;
using Mindee.Parsing.Passport;

namespace Mindee.Parsing
{
    /// <summary>
    /// Parse a passport file. 
    /// </summary>
    public interface IPassportParsing
    {
        /// <summary>
        /// Execute the parsing.
        /// </summary>
        /// <param name="parseParameter"><see cref="ParseParameter"/></param>
        /// <returns><see cref="Document{PassportPrediction}"/></returns>
        Task<Document<PassportPrediction>> ExecuteAsync(ParseParameter parseParameter);
    }
}
