namespace ImageSharpCommunity.Formats.Pdf;

public class PdfDecoder : SpecializedImageDecoder<PdfDecoderOptions>
{
    private PdfDecoder()
    {
    }

    /// <summary>
    /// Gets the current instance.
    /// </summary>
    public static PdfDecoder Instance { get; } = new PdfDecoder();

    protected override PdfDecoderOptions CreateDefaultSpecializedOptions(DecoderOptions options) => new() { GeneralOptions = options };

    protected override Image<TPixel> Decode<TPixel>(PdfDecoderOptions options, Stream stream, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(stream, nameof(stream));

        using var pdfDocument = new PdfDocument(stream);
        var firstPageOfPdf = pdfDocument?.Pages?.FirstOrDefault();
        ArgumentNullException.ThrowIfNull(firstPageOfPdf, nameof(firstPageOfPdf));

        var (width, height) = GetDimensions(firstPageOfPdf);

        using var bitmap = new PdfiumBitmap(width, height, true);
        firstPageOfPdf.Render(bitmap, PdfLibCore.Enums.PageOrientations.Normal, PdfLibCore.Enums.RenderingFlags.LcdText);

        using var bmpStream = bitmap.AsBmpStream();
        var image = Image.Load<TPixel>(bmpStream);

        ScaleToTargetSize(options.GeneralOptions, image);

        return image;
    }

    protected override Image Decode(PdfDecoderOptions options, Stream stream, CancellationToken cancellationToken) => Decode<Rgba32>(options, stream, cancellationToken);

    protected override ImageInfo Identify(DecoderOptions options, Stream stream, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(stream, nameof(stream));

        using var pdfDocument = new PdfDocument(stream);
        var firstPageOfPdf = pdfDocument?.Pages?.FirstOrDefault();
        ArgumentNullException.ThrowIfNull(firstPageOfPdf, nameof(firstPageOfPdf));

        var (width, height) = GetDimensions(firstPageOfPdf);

        return new ImageInfo(new PixelTypeInfo(4), new(width, height), null);
    }

    private static (int width, int height) GetDimensions(PdfPage page)
    {
        ArgumentNullException.ThrowIfNull(page, nameof(page));
        return ((int)page.Width, (int)page.Height);
    }
}
