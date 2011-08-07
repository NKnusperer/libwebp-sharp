using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LibwebpSharp.Native
{
    public static class WebPEncoder
    {
        /// <summary>
        /// Return the decoder's version number
        /// </summary>
        /// <returns>Hexadecimal using 8bits for each of major/minor/revision. E.g: v2.5.7 is 0x020507</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        public static extern int WebPGetEncoderVersion();
    }
}