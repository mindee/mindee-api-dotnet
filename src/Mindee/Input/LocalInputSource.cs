using System;
using System.IO;
using System.Linq;
using Mindee.Exceptions;

namespace Mindee.Input
{
    /// <summary>
    /// Represent a document to parse.
    /// </summary>
    public sealed class LocalInputSource
    {
        /// <summary>
        /// The file as a stream.
        /// </summary>
        public byte[] FileBytes { get; set; }

        /// <summary>
        /// The name of the file.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Extension file's.
        /// </summary>
        public string Extension { get; set; }

        private static readonly string[] _authorizedFileExtensions =
        {
            ".pdf",
            ".webp",
            ".jpg",
            ".jpga",
            ".jpeg",
            ".png",
            ".heic",
            ".tiff",
            ".tif",
        };

        /// <summary>
        /// Construct from bytes.
        /// </summary>
        /// <param name="file">The file contents as bytes.</param>
        /// <param name="filename">The name of the file.</param>
        /// <exception cref="MindeeInputException"></exception>
        public LocalInputSource(byte[] file, string filename)
        {
            FileBytes = file;
            SetFileName(filename);
        }

        /// <summary>
        /// Construct from string.
        /// </summary>
        /// <param name="filePath">The file's local path as a string.</param>
        /// <exception cref="MindeeInputException"></exception>
        public LocalInputSource(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(filePath);
            }
            var fileInfo = new FileInfo(filePath);
            FileBytes = File.ReadAllBytes(fileInfo.FullName);
            SetFileName(fileInfo.Name);
        }

        /// <summary>
        /// Construct from stream.
        /// </summary>
        /// <param name="fileStream">The file contents as a stream.</param>
        /// <param name="filename">The file name.</param>
        /// <exception cref="MindeeInputException"></exception>
        public LocalInputSource(Stream fileStream, string filename)
        {
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                FileBytes = memoryStream.ToArray();
            }
            SetFileName(filename);
        }

        /// <summary>
        /// Load the document to perform some checks.
        /// </summary>
        /// <param name="fileinfo">File information from the file to load from disk.</param>
        /// <exception cref="MindeeInputException"></exception>
        public LocalInputSource(FileInfo fileinfo)
        {
            FileBytes = File.ReadAllBytes(fileinfo.FullName);
            SetFileName(fileinfo.Name);
        }

        /// <param name="filename"></param>
        /// <exception cref="MindeeInputException"></exception>
        private void SetFileName(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new MindeeInputException($"The filename '{filename}' requires an extension");
            }
            Filename = filename;
            Extension = Path.GetExtension(Filename);
            if (!IsExtensionValid())
            {
                throw new MindeeInputException($"The file extension '{Extension}' is not supported.");
            }
        }

        /// <summary>
        /// Determine if the file extension is valid.
        /// </summary>
        public bool IsExtensionValid()
        {
            return _authorizedFileExtensions.Any(
                f => f.Equals(Extension, StringComparison.InvariantCultureIgnoreCase)
                );
        }

        /// <summary>
        /// Determine if the file is a PDF.
        /// </summary>
        public bool IsPdf()
        {
            return Extension.Equals(".pdf", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
