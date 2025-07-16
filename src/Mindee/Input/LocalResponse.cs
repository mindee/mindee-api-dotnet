using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Mindee.Input
{
    /// <summary>
    /// A Mindee response saved locally.
    /// </summary>
    public class LocalResponse
    {
        /// <summary>
        /// ResultFile as UTF-8 bytes.
        /// </summary>
        public byte[] FileBytes { get; }


        /// <summary>
        /// Load from a string.
        /// </summary>
        /// <param name="input">Will be decoded as UTF-8.</param>
        public LocalResponse(string input)
        {
            FileBytes = Encoding.UTF8.GetBytes(input.Trim());
        }

        /// <summary>
        /// Load from a file.
        /// </summary>
        /// <param name="input">Will be decoded as UTF-8.</param>
        public LocalResponse(FileInfo input)
        {
            FileBytes = Encoding.UTF8.GetBytes(
                File.ReadAllText(input.FullName).Trim());
        }

        /// <summary>
        /// Get the HMAC signature of the payload.
        /// </summary>
        /// <param name="secretKey">Your secret key from the Mindee platform.</param>
        /// <returns>The generated HMAC signature.</returns>
        public string GetHmacSignature(string secretKey)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            using HMACSHA256 hmac = new HMACSHA256(keyBytes);
            string hexString = BitConverter.ToString(hmac.ComputeHash(FileBytes));
            return hexString.Replace("-", "").ToLower();
        }

        /// <summary>
        /// Verify that the payload's signature matches the one received from the server.
        /// </summary>
        /// <param name="secretKey">Your secret key from the Mindee platform.</param>
        /// <param name="signature">The signature from the "X-Mindee-Hmac-Signature" HTTP header.</param>
        /// <returns></returns>
        public bool IsValidHmacSignature(string secretKey, string signature)
        {
            return GetHmacSignature(secretKey) == signature.ToLower();
        }

        /// <summary>
        /// Print the file as a UTF-8 string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Encoding.UTF8.GetString(FileBytes);
        }
    }
}
