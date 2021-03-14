using System.Windows;
using Alakazam.Engine;

namespace Alakazam {
  public partial class App : Application {

    public App() {

      Plugins.Load();

      Engine.Engine.LoadProject("47e64963-c59b-48a6-a6be-295a7c188881");

    }

  }
}
