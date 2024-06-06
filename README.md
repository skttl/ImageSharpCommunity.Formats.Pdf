# ImageSharpCommunity.Formats.Pdf

[![Downloads](https://img.shields.io/nuget/dt/ImageSharpCommunity.Formats.Pdf?color=cc9900)](https://www.nuget.org/packages/ImageSharpCommunity.Formats.Pdf/)
[![NuGet](https://img.shields.io/nuget/vpre/ImageSharpCommunity.Formats.Pdf?color=0273B3)](https://www.nuget.org/packages/ImageSharpCommunity.Formats.Pdf)
[![GitHub license](https://img.shields.io/github/license/skttl/ImageSharpCommunity.Formats.Pdf?color=8AB803)](https://github.com/skttl/ImageSharpCommunity.Formats.Pdf/blob/main/LICENSE)

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
using SixLabors.ImageSharp.Formats.Png;
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

## Usage in Umbraco

To use in Umbraco, you must inject the configuration in an appropriate manor, eg. in a Composer.

```
using Umbraco.Cms.Core.Composing;

namespace MyUmbracoProject;

public class PdfFormatComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        var config = SixLabors.ImageSharp.Configuration.Default.Clone();
        config.Configure(new ImageSharpCommunity.Formats.Pdf.PdfConfigurationModule());
    }
}

```

This will be picked up by Umbraco when starting the website, and adds the necessary configuration.

## License
[MIT](LICENSE)