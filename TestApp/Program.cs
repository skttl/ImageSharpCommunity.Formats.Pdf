
using ImageSharpCommunity.Format.Pdf;
using SixLabors.ImageSharp;

Directory.CreateDirectory("output");

var filenames = new[] {
            "sample.pdf"
        };

foreach (var filename in filenames)
{
    File.Copy(Path.Combine("sample-files", filename), Path.Combine("output", filename), true);
    ConvertImage(Path.Combine("sample-files", filename), Path.Combine("output", filename + ".jpg"));
}


static void ConvertImage(string inputPath, string outputPath)
{
    Console.WriteLine($"Converting {inputPath} to {outputPath})");

    var config = Configuration.Default.Clone();
    config.Configure(new PdfConfigurationModule());

    //config.PreferContiguousImageBuffers = true;

    using var image = Image.Load(inputPath);

    using var ms = File.Create(outputPath);
    image.SaveAsJpeg(ms);
}