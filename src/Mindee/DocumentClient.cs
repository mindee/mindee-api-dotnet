using System.IO;

namespace Mindee
{
    public sealed class DocumentClient
    {
        public Stream File { get; }
        public string Filename { get; }

        public DocumentClient(Stream file, string filename)
        {
            File = file;
            Filename = filename;
        }
    }
}
