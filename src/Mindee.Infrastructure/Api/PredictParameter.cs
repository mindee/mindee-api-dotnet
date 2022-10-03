using System.IO;

namespace Mindee.Infrastructure.Api
{
    internal sealed class PredictParameter
    {
        public Stream File { get; }
        public string Filename { get; }

        public PredictParameter(Stream file, string filename)
        {
            File = file;
            Filename = filename;
        }
    }
}
