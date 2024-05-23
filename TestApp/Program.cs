using ImageSharpCommunity.Formats.Pdf;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

Directory.CreateDirectory("output");

var filenames = new[] {
            "sample.pdf"
        };

foreach (var filename in filenames)
{
    File.Copy(Path.Combine("sample-files", filename), Path.Combine("output", filename), true);
    ConvertImage(Path.Combine("sample-files", filename), Path.Combine("output", Path.GetFileNameWithoutExtension(filename) + "." + DateTime.Now.Ticks + ".png"));
}


static void ConvertImage(string inputPath, string outputPath)
{
    Console.WriteLine($"Converting {inputPath} to {outputPath})");

    var config = Configuration.Default.Clone();
    config.Configure(new PdfConfigurationModule());

    //config.PreferContiguousImageBuffers = true;

    using var image = Image.Load(inputPath);

    image.Mutate(x => x.Resize(100, 100));
    image.SaveAsPng(outputPath);
}