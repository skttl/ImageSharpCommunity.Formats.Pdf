using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace ImageSharpCommunity.Format.Pdf
{
    public sealed class PdfConfigurationModule : IImageFormatConfigurationModule
    {
        public void Configure(Configuration configuration)
        {
            configuration.ImageFormatsManager.SetDecoder(PdfFormat.Instance, PdfDecoder.Instance);
            configuration.ImageFormatsManager.SetEncoder(PdfFormat.Instance, new PdfEncoder());
            configuration.ImageFormatsManager.AddImageFormatDetector(new PdfFormatDetector());
        }
    }
}
