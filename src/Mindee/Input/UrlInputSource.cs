using System;
using System.IO;
using System.Threading.Tasks;
using Mindee.Exceptions;
using RestSharp;
using RestSharp.Authenticators;

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

        /// <summary>
        /// Downloads the file from the url, and returns a LocalInputSource wrapper object for it.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MindeeInputException"></exception>
        public async Task<LocalInputSource> AsLocalInputSource(
            string filename = null,
            string username = null,
            string password = null,
            string token = null,
            int maxRedirects = 3,
            IRestClient restClient = null)
        {
            filename ??= Path.GetFileName(FileUrl.LocalPath);
            if (filename == "" || !Path.HasExtension(filename))
            {
                throw new MindeeInputException("Filename must end with an extension.");
            }

            var options = new RestClientOptions(FileUrl) { FollowRedirects = true, MaxRedirects = maxRedirects };

            if (!string.IsNullOrEmpty(token))
            {
                options.Authenticator = new JwtAuthenticator(token);
            }
            else if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                options.Authenticator = new HttpBasicAuthenticator(username, password);
            }

            restClient ??= new RestClient(options);
            var request = new RestRequest(FileUrl);
            var response = await restClient.ExecuteAsync(request);

            // Note: response.IsSuccessful can't be mocked as easily, so this is a better solution at the moment.
            if (response.IsSuccessStatusCode)
            {
                return new LocalInputSource(fileBytes: response.RawBytes, filename: filename);
            }
            throw new MindeeInputException($"Failed to download file: {response.ErrorMessage}");
        }
    }
}
