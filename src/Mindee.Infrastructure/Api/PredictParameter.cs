using System.IO;

namespace Mindee.Infrastructure.Api
{
    internal sealed class PredictParameter
    {
        public Stream File { get; }
        public string Filename { get; }
        public bool WithFullText { get; }

        public PredictParameter(
            Stream file, 
            string filename)
            : this (file, filename, false)
        {
        }

        public PredictParameter(
            Stream file,
            string filename,
            bool withFullText) 
        {
            File = file;
            Filename = filename;
            WithFullText = withFullText;
        }
    }
}
