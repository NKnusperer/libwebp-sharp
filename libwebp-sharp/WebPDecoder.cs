using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace LibwebpSharp
{
    public class WebPDecoder
    {
        private enum decodeType
        {
            RGB,
            RGBA,
            BGR,
            BGRA,
            YUV
        };

        /// <summary>
        /// The decoder's version number
        /// </summary>
        /// <returns>The version as major.minor.revision</returns>
        public string GetDecoderVersion()
        {
            int version = Native.WebPDecoder.WebPGetDecoderVersion();
            return String.Format("{0}.{1}.{2}", (version >> 16) & 0xff, (version >> 8) & 0xff, version & 0xff);
        }

        /// <summary>
        /// Validate the WebP image header and retrieve the image height and width
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returnsthe height of the WebP image</param>
        /// <returns>True if the WebP image header is valid, otherwise false</returns>
        public bool GetInfo(string path, out int imgWidth, out int imgHeight)
        {
            bool retValue = false;
            int width = 0;
            int height = 0;
            IntPtr pnt = IntPtr.Zero;

            try
            {
                byte[] data = Utilities.CopyFileToManagedArray(path);
                pnt = Utilities.CopyDataToUnmanagedMemory(data);
                int ret = Native.WebPDecoder.WebPGetInfo(pnt, (uint)data.Length, ref width, ref height);
                if (ret == 1)
                {
                    retValue = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // Free the unmanaged memory.
                Marshal.FreeHGlobal(pnt);
            }

            imgWidth = width;
            imgHeight = height;
            return retValue;
        }

        /// <summary>
        /// Decode the WebP image into a RGB Bitmap
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <returns>A Bitmap object with the decoded WebP image.
        /// Note that a Bitmap object use the BGR format, so if you display the Bitmap in a picturebox red and blue are mixed up</returns>
        public Bitmap DecodeRGB(string path)
        {
            return decode(path, decodeType.RGB, PixelFormat.Format24bppRgb);
        }

        /// <summary>
        /// Decode the WebP image into a RGBA Bitmap
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <returns>A Bitmap object with the decoded WebP image.
        /// Note that a Bitmap object use the ABGR format, so if you display the Bitmap in a picturebox red and blue are mixed up</returns>
        public Bitmap DecodeRGBA(string path)
        {
            return decode(path, decodeType.RGBA, PixelFormat.Format32bppArgb);
        }

        /// <summary>
        /// Decode the WebP image into a BGR Bitmap
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <returns>A Bitmap object with the decoded WebP image</returns>
        public Bitmap DecodeBGR(string path)
        {
            return decode(path, decodeType.BGR, PixelFormat.Format24bppRgb);
        }

        /// <summary>
        /// Decode the WebP image into a BGRA Bitmap
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <returns>A Bitmap object with the decoded WebP image</returns>
        public Bitmap DecodeBGRA(string path)
        {
            return decode(path, decodeType.BGRA, PixelFormat.Format32bppArgb);
        }

        /// <summary>
        /// Decode the WebP image file into raw RGB image data
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returns the height of the WebP image</param>
        /// <returns>A byte array containing the raw decoded image data</returns>
        public byte[] DecodeRGB(string path, out int imgWidth, out int imgHeight)
        {
            return decode(path, decodeType.RGB, PixelFormat.Format24bppRgb, out imgWidth, out imgHeight);
        }

        /// <summary>
        /// Decode the WebP image file into raw RGBA image data
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returns the height of the WebP image</param>
        /// <returns>A byte array containing the raw decoded image data</returns>
        public byte[] DecodeRGBA(string path, out int imgWidth, out int imgHeight)
        {
            return decode(path, decodeType.RGBA, PixelFormat.Format32bppArgb, out imgWidth, out imgHeight);
        }

        /// <summary>
        /// Decode the WebP image file into raw BGR image data
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returns the height of the WebP image</param>
        /// <returns>A byte array containing the raw decoded image data</returns>
        public byte[] DecodeBGR(string path, out int imgWidth, out int imgHeight)
        {
            return decode(path, decodeType.BGR, PixelFormat.Format24bppRgb, out imgWidth, out imgHeight);
        }

        /// <summary>
        /// Decode the WebP image file into raw BGRA image data
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returns the height of the WebP image</param>
        /// <returns>A byte array containing the raw decoded image data</returns>
        public byte[] DecodeBGRA(string path, out int imgWidth, out int imgHeight)
        {
            return decode(path, decodeType.BGRA, PixelFormat.Format32bppArgb, out imgWidth, out imgHeight);
        }

        /// <summary>
        /// Internal convert method to get a Bitmap from a WebP image file
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="type">The color type you want to convert to</param>
        /// <param name="format">The PixelFormat the Bitmap should use</param>
        /// <returns></returns>
        private Bitmap decode(string path, decodeType type, PixelFormat format)
        {
            int width = 0;
            int height = 0;
            byte[] data = decode(path, type, format, out width, out height);
            return Utilities.ConvertDataToBitmap(data, width, height, format);
        }

        /// <summary>
        /// Internal convert method to get a byte array from a WebP image file
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="type">The color type you want to convert to</param>
        /// <param name="format">The PixelFormat you want to use</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returns the height of the WebP image</param>
        /// <returns></returns>
        private byte[] decode(string path, decodeType type, PixelFormat format, out int imgWidth, out int imgHeight)
        {
            int width = 0;
            int height = 0;
            IntPtr data = IntPtr.Zero;
            IntPtr output_buffer = IntPtr.Zero;
            IntPtr result = IntPtr.Zero;

            try
            {
                // Load data
                byte[] managedData = Utilities.CopyFileToManagedArray(path);

                // Copy data to unmanaged memory
                data = Utilities.CopyDataToUnmanagedMemory(managedData);

                // Get image width and height
                int ret = Native.WebPDecoder.WebPGetInfo(data, (uint)managedData.Length, ref width, ref height);

                // Get image data lenght
                UInt32 data_size = (UInt32)managedData.Length;

                // Calculate bitmap size for decoded WebP image
                int output_buffer_size = Utilities.CalculateBitmapSize(width, height, format);

                // Allocate unmanaged memory to decoded WebP image
                output_buffer = Marshal.AllocHGlobal(output_buffer_size);

                // Calculate distance between scanlines
                int output_stride = (width * Image.GetPixelFormatSize(format)) / 8;

                // Convert image
                switch (type)
                {
                    case decodeType.RGB:
                        result = Native.WebPDecoder.WebPDecodeRGBInto(data, data_size, output_buffer, output_buffer_size, output_stride);
                        break;
                    case decodeType.RGBA:
                        result = Native.WebPDecoder.WebPDecodeRGBAInto(data, data_size, output_buffer, output_buffer_size, output_stride);
                        break;
                    case decodeType.BGR:
                        result = Native.WebPDecoder.WebPDecodeBGRInto(data, data_size, output_buffer, output_buffer_size, output_stride);
                        break;
                    case decodeType.BGRA:
                        result = Native.WebPDecoder.WebPDecodeBGRAInto(data, data_size, output_buffer, output_buffer_size, output_stride);
                        break;
                }

                // Set out values
                imgWidth = width;
                imgHeight = height;

                // Copy data back to managed memory and return
                return Utilities.GetDataFromUnmanagedMemory(result, output_buffer_size);
            }
            catch
            {
                throw;
            }
            finally
            {
                // Free unmanaged memory
                Marshal.FreeHGlobal(data);
                Marshal.FreeHGlobal(output_buffer);
            }
        }
    }
}
