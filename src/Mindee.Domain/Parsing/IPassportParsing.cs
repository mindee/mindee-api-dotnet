using System.IO;
using System.Threading.Tasks;
using Mindee.Domain.Parsing.Passport;

namespace Mindee.Domain.Parsing
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
        /// <returns><see cref="PassportInference"/></returns>
        Task<PassportInference> ExecuteAsync(ParseParameter parseParameter);
    }
}
