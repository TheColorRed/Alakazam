using System;
using System.IO;
using Newtonsoft.Json;

namespace Alakazam.Engine {
  public class LayerImage : Layer {

    [JsonConstructor]
    public LayerImage(string fileName) : base(fileName) { }

    public LayerImage(string filePath, Project project) : base(project) {
      var resources = Paths.ResourcesPath(project);
      fileName = Guid.NewGuid().ToString();
      var path = Path.Combine(resources, "bases", fileName);

      if (!File.Exists(path)) {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.Copy(filePath, path);
      }
    }

  }
}