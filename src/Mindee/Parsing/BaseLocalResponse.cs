using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Mindee.Parsing
{
    /// <summary>
    ///     A Mindee response saved locally.
    /// </summary>
    public abstract class BaseLocalResponse
    {
        /// <summary>
        ///     File as UTF-8 bytes.
        /// </summary>
        public byte[] FileBytes { get; }

        /// <summary>
        ///     Load from a string.
        /// </summary>
        /// <param name="input">Will be decoded as UTF-8.</param>
        protected BaseLocalResponse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using var reader = new StringReader(input);
            FileBytes = ReadToCleanUtf8Bytes(reader);
        }

        /// <summary>
        ///    Load from a byte buffer.
        /// </summary>
        /// <param name="input">Assumes UTF-8 encoding.</param>
        protected BaseLocalResponse(byte[] input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using var stream = new MemoryStream(input, writable: false);
            using var reader = new StreamReader(
                stream,
                Encoding.UTF8);
            FileBytes = ReadToCleanUtf8Bytes(reader);
        }

        /// <summary>
        ///    Load from a Stream.
        ///    This method will not close the provided stream.
        /// </summary>
        /// <param name="input">
        ///     Assumes UTF-8 encoding.
        /// </param>
        protected BaseLocalResponse(Stream input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (!input.CanRead)
                throw new ArgumentException("Input stream must be readable.", nameof(input));

            using var reader = new StreamReader(
                input,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: 1024,
                leaveOpen: true);
            FileBytes = ReadToCleanUtf8Bytes(reader);
        }

        /// <summary>
        ///     Load from a file.
        /// </summary>
        /// <param name="input">Will be decoded as UTF-8.</param>
        protected BaseLocalResponse(FileInfo input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using var reader = new StreamReader(
                input.FullName,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true);

            FileBytes = ReadToCleanUtf8Bytes(reader);
        }

        /// <summary>
        ///     Read, remove line endings, transform to UTF-8 bytes.
        ///     Does not close the provided Reader.
        /// </summary>
        private static byte[] ReadToCleanUtf8Bytes(TextReader reader)
        {
            var stringBuilder = new StringBuilder();
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                stringBuilder.Append(line);
            }

            string cleanJson = stringBuilder.ToString();

            if (string.IsNullOrWhiteSpace(cleanJson))
                throw new ArgumentException("Input cannot be empty or contain only whitespace.");

            return Encoding.UTF8.GetBytes(cleanJson);
        }

        /// <summary>
        ///     Get the HMAC signature of the payload.
        /// </summary>
        /// <param name="secretKey">Your secret key from the Mindee platform.</param>
        /// <returns>The generated HMAC signature.</returns>
        public string GetHmacSignature(string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            using var hmac = new HMACSHA256(keyBytes);
            var hexString = BitConverter.ToString(hmac.ComputeHash(FileBytes));
            return hexString.Replace("-", "").ToLower();
        }

        /// <summary>
        ///     Verify that the payload's signature matches the one received from the server.
        /// </summary>
        /// <param name="secretKey">Your secret key from the Mindee platform.</param>
        /// <param name="signature">The signature from the "X-Signature" HTTP header.</param>
        public bool IsValidHmacSignature(string secretKey, string signature)
        {
            if (string.IsNullOrEmpty(signature))
            {
                return false;
            }

            string expectedSignature = GetHmacSignature(secretKey);

            byte[] expectedBytes = Encoding.UTF8.GetBytes(expectedSignature);
            byte[] actualBytes = Encoding.UTF8.GetBytes(signature.ToLower());

            return FixedTimeEquals(expectedBytes, actualBytes);
        }

        /// <summary>
        /// Custom constant-time comparison method, since it doesn't exist in .NET472/48
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;

            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            return diff == 0;
        }

        /// <summary>
        ///     Print the file as a UTF-8 string.
        /// </summary>
        public override string ToString()
        {
            return Encoding.UTF8.GetString(FileBytes);
        }
    }
}
