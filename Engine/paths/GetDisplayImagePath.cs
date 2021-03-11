using System.IO;

namespace Alakazam.Engine {
  public partial class Paths {
    public static string DisplayImagePath(Project project) {
      var root = ProjectRootPath(project.guid);
      return Path.Combine(root, "display.png");
    }
  }
}