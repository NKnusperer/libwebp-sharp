using System;
using System.Collections.Generic;
using System.Text;

namespace LibwebpSharp
{
    public class WebPEncoder
    {
        /// <summary>
        /// The encoder's version number
        /// </summary>
        /// <returns>The version as major.minor.revision</returns>
        public string GetEncoderVersion()
        {
            int version = Native.WebPEncoder.WebPGetEncoderVersion();
            return String.Format("{0}.{1}.{2}", (version >> 16) & 0xff, (version >> 8) & 0xff, version & 0xff);
        }
    }
}