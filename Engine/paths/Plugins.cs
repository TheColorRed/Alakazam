using System.IO;

namespace Alakazam.Engine {
  public partial class Paths {
    public static string Plugins() {
      var root = AppDataDirectory();
      return Path.Combine(root, "Plugins");
    }
  }
}