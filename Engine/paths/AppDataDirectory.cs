using System;
using System.IO;

namespace Alakazam.Engine {
  public partial class Paths {
    public static string AppDataDirectory() {
      var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      return Path.Combine(appData, "Alakazam");
    }
  }
}