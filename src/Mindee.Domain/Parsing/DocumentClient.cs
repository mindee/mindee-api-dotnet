using System.IO;

namespace Mindee.Domain
{
    /// <summary>
    /// Represent a document to parse.
    /// </summary>
    public sealed class DocumentClient
    {
        /// <summary>
        /// The file as a stream.
        /// </summary>
        public Stream File { get; }

        /// <summary>
        /// The name of the file.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// Extension file's.
        /// </summary>
        public string Extension { get; }

        public DocumentClient(Stream file, string filename)
        {
            File = file;
            Filename = filename;
            Extension = Path.GetExtension(Filename);
        }
    }
}
