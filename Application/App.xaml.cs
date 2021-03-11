using System.IO;
using System.Reflection;
using System.Windows;
using Console = System.Console;
using Alakazam.Engine;

namespace Alakazam {
  public partial class App : Application {

    public App() {

      Plugins.Load();

    }

  }
}
