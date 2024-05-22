using PdfLibCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.PixelFormats;
using Image = SixLabors.ImageSharp.Image;

namespace ImageSharpCommunity.Format.Pdf
{
    public class PdfDecoder : SpecializedImageDecoder<BmpDecoderOptions>
    {
        private PdfDecoder()
        {
        }

        /// <summary>
        /// Gets the current instance.
        /// </summary>
        public static PdfDecoder Instance { get; } = new PdfDecoder();

        protected override BmpDecoderOptions CreateDefaultSpecializedOptions(DecoderOptions options) => new() { GeneralOptions = options };

        protected override Image<TPixel> Decode<TPixel>(BmpDecoderOptions options, Stream stream, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            using var pdfDocument = new PdfDocument(stream);
            var page = pdfDocument?.Pages?.FirstOrDefault();

            TryGetWidthAndHeight(page, out var width, out var height);

            ArgumentNullException.ThrowIfNull(page, nameof(page));

            using var bitmap = new PdfiumBitmap(width, height, true);
            page.Render(bitmap, PdfLibCore.Enums.PageOrientations.Normal, PdfLibCore.Enums.RenderingFlags.LcdText);

            var image = Image.Load<TPixel>(bitmap.AsBmpStream());

            ScaleToTargetSize(options.GeneralOptions, image);

            return image;
        }

        protected override Image Decode(BmpDecoderOptions options, Stream stream, CancellationToken cancellationToken) => Decode<Rgba32>(options, stream, cancellationToken);

        protected override ImageInfo Identify(DecoderOptions options, Stream stream, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            using var pdfDocument = new PdfDocument(stream);
            var page = pdfDocument?.Pages?.FirstOrDefault();
            TryGetWidthAndHeight(page, out int width, out int height);

            return new ImageInfo(new PixelTypeInfo(4), new(width, height), null);
        }

        private bool TryGetWidthAndHeight(PdfPage? page, out int width, out int height)
        {
            ArgumentNullException.ThrowIfNull(page, nameof(page));

            var w = page.Size.Width;
            w = w != 0 ? w / 72 * 144D : 0;

            var h = page.Size.Height;
            h = h != 0 ? h / 72 * 144D : 0;

            width = (int)w;
            height = (int)h;

            return true;
        }
    }
}
