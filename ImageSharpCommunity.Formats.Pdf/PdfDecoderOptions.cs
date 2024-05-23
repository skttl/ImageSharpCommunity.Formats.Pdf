using SixLabors.ImageSharp.Formats;

namespace ImageSharpCommunity.Formats.Pdf
{
    public sealed class PdfDecoderOptions : ISpecializedDecoderOptions
    {
        /// <inheritdoc/>
        public DecoderOptions GeneralOptions { get; init; } = new();
    }
}
