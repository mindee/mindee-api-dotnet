using System.IO;

namespace Mindee
{
    /// <summary>
    /// Represent a document to parse.
    /// </summary>
    public sealed class DocumentClient
    {
        /// <summary>
        /// The file as a stream.
        /// </summary>
        public byte[] File { get; }

        /// <summary>
        /// The name of the file.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// Extension file's.
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="filename">The name of the file.</param>
        public DocumentClient(byte[] file, string filename)
        {
            File = file;
            Filename = filename;
            Extension = Path.GetExtension(Filename);
        }
    }
}
