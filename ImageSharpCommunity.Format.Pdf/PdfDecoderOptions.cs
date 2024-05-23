using SixLabors.ImageSharp.Formats;

namespace ImageSharpCommunity.Format.Pdf
{
    public sealed class PdfDecoderOptions : ISpecializedDecoderOptions
    {
        /// <inheritdoc/>
        public DecoderOptions GeneralOptions { get; init; } = new();
    }
}
