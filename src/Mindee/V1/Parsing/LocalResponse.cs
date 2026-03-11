using System.IO;

namespace Mindee.V1.Parsing
{
    /// <summary>
    /// Implementation of BaseLocalResponse for V1.
    /// </summary>
    public class LocalResponse : Mindee.Parsing.BaseLocalResponse
    {
        /// <inheritdoc />
        public LocalResponse(string input) : base(input)
        {
        }

        /// <inheritdoc />
        public LocalResponse(FileInfo input) : base(input)
        {
        }
    }
}
