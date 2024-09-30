using System;
using System.Runtime.InteropServices;
using Docnet.Core.Models;
using Mindee.Exceptions;
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
                    uint pixelValue = red + (uint)(green << 8) + (uint)(blue << 16) + (uint)(alpha << 24);
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

        private static SKColor AdjustColor(SKColor originalColor)
        {
            ColorToHsl(originalColor, out var hue, out var saturation, out var lightness);

            saturation = Math.Min(1.0f, saturation * 1.5f);
            lightness = (float)(1.1 - 0.8 / (1 + Math.Exp(-10 * (lightness - 0.5))));

            return HslToColor(hue, saturation, lightness);
        }

        private static void ColorToHsl(SKColor color, out float hue, out float saturation, out float lightness)
        {
            float r = color.Red / 255f;
            float g = color.Green / 255f;
            float b = color.Blue / 255f;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));

            lightness = (max + min) / 2;

            const float tolerance = 0.01f;
            if (Math.Abs(max - min) < tolerance)
            {
                hue = saturation = 0;
            }
            else
            {
                float d = max - min;
                saturation = lightness > 0.5 ? d / (2 - max - min) : d / (max + min);

                if (Math.Abs(max - r) < tolerance)
                    hue = (g - b) / d + (g < b ? 6 : 0);
                else if (Math.Abs(max - g) < tolerance)
                    hue = (b - r) / d + 2;
                else
                    hue = (r - g) / d + 4;

                hue /= 6;
            }
        }

        private static SKColor HslToColor(float h, float s, float l)
        {
            float r, g, b;

            if (s == 0)
            {
                r = g = b = l;
            }
            else
            {
                float Hue2Rgb(float p, float q, float t)
                {
                    if (t < 0) t += 1;
                    if (t > 1) t -= 1;
                    return t switch
                    {
                        < 1 / 6f => p + (q - p) * 6 * t,
                        < 1 / 2f => q,
                        < 2 / 3f => p + (q - p) * (2 / 3f - t) * 6,
                        _ => p
                    };
                }

                float q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                float p = 2 * l - q;

                r = Hue2Rgb(p, q, h + 1 / 3f);
                g = Hue2Rgb(p, q, h);
                b = Hue2Rgb(p, q, h - 1 / 3f);
            }

            return new SKColor((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }


        /// <summary>
        /// Infers the color of a character in the source text of a PDF file.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        public static SKColor InferTextColor(SKBitmap bitmap, BoundBox box)
        {
            int left = Math.Max(0, box.Left);
            int top = Math.Max(0, box.Top);
            int right = Math.Min(bitmap.Width - 1, box.Right);
            int bottom = Math.Min(bitmap.Height - 1, box.Bottom);

            long totalR = 0, totalG = 0, totalB = 0;
            int pixelCount = 0;

            for (int y = top; y <= bottom; y++)
            {
                for (int x = left; x <= right; x++)
                {
                    SKColor pixel = bitmap.GetPixel(x, y);
                    totalR += pixel.Red;
                    totalG += pixel.Green;
                    totalB += pixel.Blue;
                    pixelCount++;
                }
            }

            if (pixelCount <= 0)
            {
                return SKColors.Black;
            }

            byte avgR = (byte)(totalR / pixelCount);
            byte avgG = (byte)(totalG / pixelCount);
            byte avgB = (byte)(totalB / pixelCount);

            return AdjustColor(new SKColor(avgR, avgG, avgB));
        }

        /// <summary>
        /// Computes the new dimensions for a given SKBitmap, and returns a scaled down version of it relative to the provided bounds.
        /// </summary>
        /// <param name="original">Input SKBitmap.</param>
        /// <param name="maxWidth">Optional max width.</param>
        /// <param name="maxHeight">Optional max height.</param>
        /// <returns>The scaled down dimensions for the SKBitmap.</returns>
        /// <exception cref="MindeeInputException">Throws if the bitmap is null.</exception>
        public static (int newWidth, int newHeight) CalculateNewDimensions(SKBitmap original, int? maxWidth = null, int? maxHeight = null)
        {
            if (original == null)
                throw new MindeeInputException("Generated bitmap could not be processed for resizing.");

            if (!maxWidth.HasValue && !maxHeight.HasValue)
                return (original.Width, original.Height);

            double widthRatio = maxWidth.HasValue ? (double)maxWidth.Value / original.Width : double.PositiveInfinity;
            double heightRatio = maxHeight.HasValue ? (double)maxHeight.Value / original.Height : double.PositiveInfinity;

            double scaleFactor = Math.Min(widthRatio, heightRatio);

            int newWidth = (int)(original.Width * scaleFactor);
            int newHeight = (int)(original.Height * scaleFactor);

            return (newWidth, newHeight);
        }

    }
}
