using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing.Imaging;
using System.Reflection;

namespace ImageSharpCommunity.Format.Pdf
{
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


            using var rasterizer = new GhostscriptRasterizer();
            rasterizer.Open(stream, GetGhostscriptVersion(), true);

            var firstPageAsImage = rasterizer.GetPage(200, 1);

            using var memoryStream = new MemoryStream();
            firstPageAsImage.Save(memoryStream, ImageFormat.Png);
            memoryStream.Position = 0;

            var image = PngDecoder.Instance.Decode<TPixel>(options.GeneralOptions, memoryStream);

            var binPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

            ScaleToTargetSize(options.GeneralOptions, image);

            return image;
        }

        protected override Image Decode(PdfDecoderOptions options, Stream stream, CancellationToken cancellationToken) => Decode<Rgba32>(options, stream, cancellationToken);

        protected override ImageInfo Identify(DecoderOptions options, Stream stream, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            using var rasterizer = new GhostscriptRasterizer();
            rasterizer.Open(stream, GetGhostscriptVersion(), true);
            var firstPageAsImage = rasterizer.GetPage(200, 1);



            return new ImageInfo(new PixelTypeInfo(4), new(firstPageAsImage.Width, firstPageAsImage.Height), null);
        }

        private GhostscriptVersionInfo GetGhostscriptVersion()
        {
            var binPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            var gsDllPath = Path.Combine(binPath ?? "", Environment.Is64BitProcess ? "gsdll64.dll" : "gsdll32.dll");
            var version = new GhostscriptVersionInfo(new Version(10, 02, 1), gsDllPath, string.Empty, GhostscriptLicense.GPL);

            return version;
        }
    }
}
