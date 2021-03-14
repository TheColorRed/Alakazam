using Alakazam.Engine.Settings;

namespace Alakazam.Engine {
  public class Engine {

    public static Project project;

    public static void LoadProject(string guid) {
      project = new ProjectSettings().Load(guid);
    }
  }
}