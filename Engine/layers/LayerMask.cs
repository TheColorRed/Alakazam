using System.IO;
using ImageMagick;
using Newtonsoft.Json;

namespace Alakazam.Engine {
  public class LayerMask {

    [JsonProperty] private readonly string fileName;

    private MagickImage mask;
    [JsonIgnore]
    public MagickImage Image {
      get {
        if (mask == null) mask = Load(Engine.project);
        return mask;
      }
      set => mask = value;
    }

    [JsonConstructor] public LayerMask(string fileName) => this.fileName = fileName;

    public MagickImage Load(Project project) {
      var resources = Paths.ResourcesPath(project);
      var path = Path.Combine(resources, "masks", fileName);

      return new MagickImage(path);
    }

    public void Save(Project project) {
      if (Image != null) {
        var resources = Paths.ResourcesPath(project);
        var path = Path.Combine(resources, "masks", fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(path));
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