using System.IO;
using System.Threading.Tasks;
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
        /// <param name="file">The file data.</param>
        /// <param name="filename">The filename.</param>
        /// <returns><see cref="PassportInference"/></returns>
        Task<PassportInference> ExecuteAsync(Stream file, string filename);
    }
}
