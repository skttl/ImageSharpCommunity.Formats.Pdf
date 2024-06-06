namespace ImageSharpCommunity.Formats.Pdf;

public sealed class PdfFormat : IImageFormat
{
    private PdfFormat()
    {
    }

    /// <summary>
    /// Gets the current instance of the <see cref="PdfFormat"/>.
    /// </summary>
    public static PdfFormat Instance { get; } = new();

    /// <summary>
    /// Gets the name of the image format.
    /// </summary>
    public string Name => "PDF";

    /// <summary>
    /// Gets the default MIME type of the image format.
    /// </summary>
    public string DefaultMimeType => "application/pdf";

    /// <summary>
    /// Gets the MIME types associated with the image format.
    /// </summary>
    public IEnumerable<string> MimeTypes { get; } = ImmutableArray.Create("application/pdf");

    /// <summary>
    /// Gets the file extensions associated with the image format.
    /// </summary>
    public IEnumerable<string> FileExtensions { get; } = ImmutableArray.Create("pdf");
}
