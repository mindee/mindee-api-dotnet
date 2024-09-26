using System;
using System.Runtime.InteropServices;
using SkiaSharp;

namespace Mindee.Input
{
    /// <summary>
    /// Utility class.
    /// </summary>
    public static class MindeeInputUtils
    {
        /// <summary>
        /// Converts a raw sequence of bytes representing an image into a 3D pixel array.
        /// </summary>
        /// <param name="input">Input byte sequence.</param>
        /// <param name="width">Width of the image.</param>
        /// <param name="height">Height of the image</param>
        /// <returns>A 3D array of pixels.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[,,] ConvertTo3DArray(byte[] input, int width, int height)
        {
            if (input.Length != width * height * 4)
            {
                throw new ArgumentException("Input array size does not match the given width and height.");
            }

            byte[,,] output = new byte[height, width, 4];

            for (int i = 0; i < input.Length; i += 4)
            {
                int pixelIndex = i / 4;
                int x = pixelIndex % width;
                int y = pixelIndex / width;

                output[y, x, 2] = input[i];
                output[y, x, 1] = input[i + 1];
                output[y, x, 0] = input[i + 2];
                output[y, x, 3] = input[i + 3];
            }

            return output;
        }

        /// <summary>
        /// Convert a matrix of pixels into a compatible SKBitmap.
        /// </summary>
        /// <param name="pixelArray">Raw array produce by DocLib's .GetImage() function.</param>
        /// <returns>A valid SKBitmap.</returns>
        public static SKBitmap ArrayToImage(byte[,,] pixelArray)
        {
            int width = pixelArray.GetLength(1);
            int height = pixelArray.GetLength(0);

            uint[] pixelValues = new uint[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte alpha = 255;
                    byte red = pixelArray[y, x, 0];
                    byte green = pixelArray[y, x, 1];
                    byte blue = pixelArray[y, x, 2];
                    uint pixelValue = (uint)red + (uint)(green << 8) + (uint)(blue << 16) + (uint)(alpha << 24);
                    pixelValues[y * width + x] = pixelValue;
                }
            }

            SKBitmap bitmap = new();
            GCHandle gcHandle = GCHandle.Alloc(pixelValues, GCHandleType.Pinned);
            SKImageInfo info = new(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);

            IntPtr ptr = gcHandle.AddrOfPinnedObject();
            int rowBytes = info.RowBytes;
            bitmap.InstallPixels(info, ptr, rowBytes, delegate { gcHandle.Free(); });

            return bitmap;
        }
    }
}
