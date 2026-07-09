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
        ///     Load from a string.
        /// </summary>
        /// <param name="input">Will be decoded as UTF-8.</param>
        public BaseLocalResponse(string input)
        {
            FileBytes = Encoding.UTF8.GetBytes(input.Replace("\r", "").Replace("\n", ""));
        }

        /// <summary>
        ///     Load from a file.
        /// </summary>
        /// <param name="input">Will be decoded as UTF-8.</param>
        public BaseLocalResponse(FileInfo input)
        {
            FileBytes = Encoding.UTF8.GetBytes(
                File.ReadAllText(input.FullName).Replace("\r", "").Replace("\n", ""));
        }

        /// <summary>
        ///     ResultFile as UTF-8 bytes.
        /// </summary>
        public byte[] FileBytes { get; }

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
        /// <param name="signature">The signature from the "X-Mindee-Hmac-Signature" HTTP header.</param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
        public override string ToString()
        {
            return Encoding.UTF8.GetString(FileBytes);
        }
    }
}
