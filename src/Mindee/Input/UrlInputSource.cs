using System;
using System.IO;
using System.Linq;
using Mindee.Exceptions;

namespace Mindee.Input
{
    /// <summary>
    /// Represent a document to parse.
    /// </summary>
    public sealed class UrlInputSource
    {
        /// <summary>
        /// The URI of the file.
        /// </summary>
        public Uri FileUrl { get; }

        /// <summary>
        /// Construct from string.
        /// </summary>
        /// <param name="fileUrl"><see cref="FileUrl"/></param>
        public UrlInputSource(string fileUrl)
        {
            FileUrl = new Uri(fileUrl);
            IsUriValid();
        }

        /// <summary>
        /// Construct from a URI object.
        /// </summary>
        /// <param name="fileUrl"><see cref="FileUrl"/></param>
        public UrlInputSource(Uri fileUrl)
        {
            FileUrl = fileUrl;
            IsUriValid();
        }

        /// <summary>
        /// Determine if the URI is valid.
        /// </summary>
        private void IsUriValid()
        {
            if (FileUrl.IsLoopback)
            {
                throw new MindeeInputException("Local files are not supported, use `LocalInputSource` instead.");
            }
            if (!FileUrl.IsAbsoluteUri)
            {
                throw new MindeeInputException("The URI must be absolute.");
            }
            if (FileUrl.Scheme != "https")
            {
                throw new MindeeInputException("Only the HTTPS scheme is supported.");
            }
        }
    }
}
