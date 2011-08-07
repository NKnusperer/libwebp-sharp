===SUMMARY===
libwebp-sharp is a simple managed warpper around
Googles WebP image decoding and encoding library libwebp.

===FEATURES===
Currently libwebp-sharp can only decode WebP image files
into the RGB, RGBA, BGR and BGRA format. YUV is currently
not implemented.
It can return a byte array with the raw image data or a
Bitmap object so you direct display the image.

===HOW TO USE===
Simply download the precompiled binaries to start.
The download also contains a dynamic build of libwebp.dll (x86)
so you don't need any additional software.

Load a WebP image into a Picturebox:
LibwebpSharp.WebPDecoder dec = new LibwebpSharp.WebPDecoder();
PictureBox1.Image = dec.DecodeBGRA(@"X:\PATH\TO\YOUR\IMAGE.webp");

Check if WebP header is valid and get image width and height:
int width; // e.g 100
int height; // e.g 100
LibwebpSharp.WebPDecoder dec = new LibwebpSharp.WebPDecoder();
bool result = dec.GetInfo(@"X:\PATH\TO\YOUR\IMAGE.webp", out width, out height); // e.g true

Get WebP decoder version:
LibwebpSharp.WebPDecoder dec = new LibwebpSharp.WebPDecoder();
string version = string version = dec.GetDecoderVersion(); // e.g. 0.1.2

===SPECIAL NOTES===
A Bitmap object use the BGR or BGRA format, so if you display the
Bitmap in a picturebox red and blue are mixed up.

===TODO===
Implement YUV decoding und the whole encoding functions.