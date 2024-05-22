using SixLabors.ImageSharp.Formats;
using System.Diagnostics.CodeAnalysis;

namespace ImageSharpCommunity.Format.Pdf
{
    public class PdfFormatDetector : IImageFormatDetector
    {
        public int HeaderSize => 16;

        public bool TryDetectFormat(ReadOnlySpan<byte> header, [NotNullWhen(true)] out IImageFormat? format)
        {
            format = IsSupportedFileFormat(header) ? PdfFormat.Instance : null;

            return format != null;
        }

        private bool IsSupportedFileFormat(ReadOnlySpan<byte> header)
        {
            if (header.Length >= 4)
            {
                return header[..4].SequenceEqual(new byte[]{
                    0x25,   // %
                    0x50,   // P
                    0x44,   // D
                    0x46,   // F
                });
            }

            return false;
        }
    }
}
