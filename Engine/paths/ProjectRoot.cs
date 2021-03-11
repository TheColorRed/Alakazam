using System.IO;

namespace Alakazam.Engine {
  public partial class Paths {

    public static string ProjectsRoot() {
      var appData = AppDataDirectory();
      return Path.Combine(appData, "projects");
    }

    public static string ProjectRootPath(string guid) {
      return Path.Combine(ProjectsRoot(), guid);
    }

    public static string ProjectRootPath(Project project) {
      return ProjectRootPath(project.guid);
    }

    public static string ProjectFile(string guid) {
      var root = ProjectRootPath(guid);
      return Path.Combine(root, "project.json");
    }

    public static string ProjectFile(Project project) {
      return ProjectFile(project.guid);
    }
  }
}