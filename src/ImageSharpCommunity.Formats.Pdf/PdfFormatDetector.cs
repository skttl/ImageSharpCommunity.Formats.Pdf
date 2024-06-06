namespace ImageSharpCommunity.Formats.Pdf;

public class PdfFormatDetector : IImageFormatDetector
{
    public int HeaderSize => 16;

    public bool TryDetectFormat(ReadOnlySpan<byte> header, [NotNullWhen(true)] out IImageFormat? format)
    {
        format = IsPDFFileFormat(header) ? PdfFormat.Instance : null;
        return format != null;
    }

    private static bool IsPDFFileFormat(ReadOnlySpan<byte> header)
    {
        return header.Length >= 4 &&
               header[0] == 0x25 && // %
               header[1] == 0x50 && // P
               header[2] == 0x44 && // D
               header[3] == 0x46;  // F
    }
}
