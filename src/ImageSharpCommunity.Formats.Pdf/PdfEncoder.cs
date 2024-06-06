namespace ImageSharpCommunity.Formats.Pdf;

public class PdfEncoder : ImageEncoder
{
    protected override void Encode<TPixel>(Image<TPixel> image, Stream stream, CancellationToken cancellationToken)
    {
        throw new NotSupportedException("PDF encoding is not supported");
    }
}
