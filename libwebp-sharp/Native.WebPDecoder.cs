using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LibwebpSharp.Native
{
	public static class WebPDecoder
	{
        /// <summary>
        /// Return the decoder's version number
        /// </summary>
        /// <returns>Hexadecimal using 8bits for each of major/minor/revision. E.g: v2.5.7 is 0x020507</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        public static extern int WebPGetDecoderVersion();

        /// <summary>
        /// This function will validate the WebP image header and retrieve the image height and width. Pointers *width and *height can be passed NULL if deemed irrelevant
        /// </summary>
        /// <param name="data">Pointer to WebP image data</param>
        /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
        /// <param name="width">The range is limited currently from 1 to 16383</param>
        /// <param name="height">The range is limited currently from 1 to 16383</param>
        /// <returns>1 if success, otherwise error code returned in the case of (a) formatting error(s).</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        public static extern int WebPGetInfo(IntPtr data, UInt32 data_size, ref int width, ref int height);

        /// <summary>
        ///  Decodes WEBP images pointed to by *data and returns RGB samples into a pre-allocated buffer
        /// </summary>
        /// <param name="data">Pointer to WebP image data</param>
        /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
        /// <param name="output_buffer">Pointer to decoded WebP image</param>
        /// <param name="output_buffer_size">Size of allocated buffer</param>
        /// <param name="output_stride">Specifies the distance between scanlines</param>
        /// <returns>output_buffer if function succeeds; NULL otherwise</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        public static extern IntPtr WebPDecodeRGBInto(IntPtr data, UInt32 data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        /// <summary>
        ///  Decodes WEBP images pointed to by *data and returns RGBA samples into a pre-allocated buffer
        /// </summary>
        /// <param name="data">Pointer to WebP image data</param>
        /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
        /// <param name="output_buffer">Pointer to decoded WebP image</param>
        /// <param name="output_buffer_size">Size of allocated buffer</param>
        /// <param name="output_stride">Specifies the distance between scanlines</param>
        /// <returns>output_buffer if function succeeds; NULL otherwise</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        public static extern IntPtr WebPDecodeRGBAInto(IntPtr data, UInt32 data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        /// <summary>
        ///  Decodes WEBP images pointed to by *data and returns BGR samples into a pre-allocated buffer
        /// </summary>
        /// <param name="data">Pointer to WebP image data</param>
        /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
        /// <param name="output_buffer">Pointer to decoded WebP image</param>
        /// <param name="output_buffer_size">Size of allocated buffer</param>
        /// <param name="output_stride">Specifies the distance between scanlines</param>
        /// <returns>output_buffer if function succeeds; NULL otherwise</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        public static extern IntPtr WebPDecodeBGRInto(IntPtr data, UInt32 data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        /// <summary>
        ///  Decodes WEBP images pointed to by *data and returns BGRA samples into a pre-allocated buffer
        /// </summary>
        /// <param name="data">Pointer to WebP image data</param>
        /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
        /// <param name="output_buffer">Pointer to decoded WebP image</param>
        /// <param name="output_buffer_size">Size of allocated buffer</param>
        /// <param name="output_stride">Specifies the distance between scanlines</param>
        /// <returns>output_buffer if function succeeds; NULL otherwise</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        public static extern IntPtr WebPDecodeBGRAInto(IntPtr data, UInt32 data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);
	}
}