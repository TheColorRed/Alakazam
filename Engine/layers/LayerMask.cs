using System.IO;
using ImageMagick;
using Newtonsoft.Json;

namespace Alakazam.Engine {
  public class LayerMask {

    [JsonProperty] private readonly string fileName;

    [JsonIgnore] public MagickImage Image { get; set; }

    [JsonConstructor]
    public LayerMask(string fileName) => this.fileName = fileName;

    public void Load(Project project) {
      var resources = Paths.ResourcesPath(project);
      var path = Path.Combine(resources, "masks", fileName);

      Image = new MagickImage(path);
    }

    public void Save(Project project) {
      if (Image != null) {
        var resources = Paths.ResourcesPath(project);
        var path = Path.Combine(resources, "masks", fileName);

        Image.Write(path, MagickFormat.Bmp);
      }
    }

    public void Delete(Project project) {
      var resources = Paths.ResourcesPath(project);
      var filePath = Path.Combine(resources, "masks", fileName);
      if (File.Exists(filePath)) File.Delete(filePath);
    }
  }
}