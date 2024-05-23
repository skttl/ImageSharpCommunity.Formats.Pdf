using SixLabors.ImageSharp.Formats;
using System.Diagnostics.CodeAnalysis;

namespace ImageSharpCommunity.Formats.Pdf
{
    public class PdfFormatDetector : IImageFormatDetector
    {
        public int HeaderSize => 16;

        public bool TryDetectFormat(ReadOnlySpan<byte> header, [NotNullWhen(true)] out IImageFormat? format)
        {
            format = IsPDFFileFormat(header) ? PdfFormat.Instance : null;

            return format != null;
        }

        private bool IsPDFFileFormat(ReadOnlySpan<byte> header)
        {
            if (header.Length >= 4)
            {
                var first4 = header[..4];

                var shouldMatch = new byte[]{
                    0x25,   // %
                    0x50,   // P
                    0x44,   // D
                    0x46,   // F
                };

                return first4.SequenceEqual(shouldMatch);
            }

            return false;
        }
    }
}
