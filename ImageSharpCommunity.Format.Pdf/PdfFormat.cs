using SixLabors.ImageSharp.Formats;

namespace ImageSharpCommunity.Format.Pdf
{
    public sealed class PdfFormat : IImageFormat
    {
        private PdfFormat()
        {
        }

        /// <summary>
        /// Gets the current instance.
        /// </summary>
        public static PdfFormat Instance { get; } = new();
        public string Name => "PDF";

        public string DefaultMimeType => "application/pdf";

        public IEnumerable<string> MimeTypes => new[] { "application/pdf" };

        public IEnumerable<string> FileExtensions => new[] { "pdf" };
    }
}
