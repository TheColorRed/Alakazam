using System.IO;

namespace Alakazam.Engine {
  public partial class Paths {
    public static string ResourcesPath(Project project) {
      return ResourcesPath(project.guid);
    }

    public static string ResourcesPath(string guid) {
      var root = ProjectRootPath(guid);
      return Path.Combine(root, "resources");
    }
  }
}