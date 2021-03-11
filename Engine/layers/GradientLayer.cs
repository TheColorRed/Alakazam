using System;
using System.IO;
using Alakazam.ImageMagick;
using ImageMagick;
using Newtonsoft.Json;

namespace Alakazam.Engine {
  public class LayerGradient : Layer {

    public MagickColor StartColor { get; set; } = MagickColors.Black;
    public MagickColor EndColor { get; set; } = MagickColors.White;
    public double Angle { get; set; } = 0;

    [JsonConstructor]
    public LayerGradient(string fileName) : base(fileName) { }

    public LayerGradient(Project project) : base(project) {
      var resources = Paths.ResourcesPath(project);
      fileName = Guid.NewGuid().ToString();
      var path = Path.Combine(resources, "bases", fileName);
      var width = project.size.width;
      var height = project.size.height;
      var image = AutoMagick.Gradient(StartColor, EndColor, width, height, Angle);

      Directory.CreateDirectory(Path.GetDirectoryName(path));
      image.Write(path, MagickFormat.Bmp);
    }

  }
}