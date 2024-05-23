# ImageSharpCommunity.Formats.Pdf
Image decoder for PDF files for [ImageSharp](https://docs.sixlabors.com/articles/imagesharp/index.html) based on [PdfLibCore](https://github.com/jbaarssen/PdfLibCore)

## Install
via [NuGet](https://www.nuget.org/packages/ImageSharpCommunity.Formats.Pdf):
```
PM> Install-Package ImageSharp.Community.Formats.Pdf
```

## Usage

```C#
using System.IO;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using ImageSharpCommunity.Formats.Pdf;

// Create custom configuration with PDF decoder
var configuration = new Configuration(
    new PdfConfigurationModule());

using var inputStream = File.OpenRead("/path/to/document.pdf");
using var image = Image.Load(inputStream);

// Resize
image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2)); 

// Save image
image.SaveAsPng("document.png");
```
More info <https://docs.sixlabors.com/articles/imagesharp/configuration.html>

## License
[MIT](LICENSE)