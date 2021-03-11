using System;
using System.IO;
using System.Linq;
using Alakazam.ImageMagick;
using ImageMagick;
using Newtonsoft.Json;

namespace Alakazam.Engine {
  public class LayerNoise : Layer {

    public bool IsColor { get; set; } = false;

    [JsonConstructor]
    public LayerNoise(string fileName) : base(fileName) { }

    public LayerNoise(Project project) : base(project) {
      fileName = Guid.NewGuid().ToString();
      GenerateImage();
    }

    public void GenerateImage() {
      var resources = Paths.ResourcesPath(Project);
      var path = Path.Combine(resources, "bases", fileName);
      var image = AutoMagick.PerlinNoise(Project.size.width, Project.size.height);

      if (IsColor == false) {
        image = (MagickImage)image.Separate(Channels.Green).First();
      }

      Directory.CreateDirectory(Path.GetDirectoryName(path));
      image.Write(path, MagickFormat.Bmp);
      imageOriginal = null;
    }

  }
}