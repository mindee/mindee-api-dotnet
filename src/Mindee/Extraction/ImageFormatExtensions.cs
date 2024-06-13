#nullable enable
using System;
using FreeImageAPI;
using Microsoft.Extensions.Logging;
using Mindee.Exceptions;

namespace Mindee.Extraction
{
    /// <summary>
    /// FreeImage interface utilities.
    /// </summary>
    public static class ImageFormatExtensions
    {
    /// <summary>
    /// Enum representing supported image formats.
    /// </summary>
    public enum ImageFormat
    {
        /// <summary>
        /// Joint Photographic Experts Group format (JPG).
        /// </summary>
        JPG = FREE_IMAGE_FORMAT.FIF_JPEG,

        /// <summary>
        /// Joint Photographic Experts Group format (JPEG).
        /// </summary>
        JPEG = FREE_IMAGE_FORMAT.FIF_JPEG,

        /// <summary>
        /// Portable Network Graphics format (PNG).
        /// </summary>
        PNG = FREE_IMAGE_FORMAT.FIF_PNG,

        /// <summary>
        /// Tagged Image File Format (TIFF).
        /// </summary>
        TIFF = FREE_IMAGE_FORMAT.FIF_TIFF
    }
        /// <summary>
        /// Converts a string to the corresponding ImageFormat enum value.
        /// </summary>
        /// <param name="formatStr">The string representing the image format.</param>
        /// <returns>The corresponding ImageFormat enum value, or ImageFormat.JPG if not found.</returns>
        public static ImageFormat? FromString(string? formatStr=null)
        {
            if (formatStr == null)
            {
                ILogger logger = MindeeLogger.GetLogger();
                logger.LogWarning("No format provided, file will be saved as JPEG by default.");
                return ImageFormat.JPG;
            }
            if (Enum.TryParse(formatStr, true, out ImageFormat format))
            {
                return format;
            }

            throw new MindeeException($"Unable to process format '{formatStr}', reverting to JPEG by default.");
        }
    }
}
