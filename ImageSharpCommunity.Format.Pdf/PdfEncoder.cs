using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace ImageSharpCommunity.Format.Pdf
{
    public class PdfEncoder : ImageEncoder
    {
        protected override void Encode<TPixel>(Image<TPixel> image, Stream stream, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
