using System.IO;

namespace Alakazam.Engine.Settings {

  public class ApplicationSettings : SettingsObject {
    public int width = 800;
    public int height = 600;
    public int x = 0;
    public int y = 0;
    public bool fullScreen = false;
    public int layersSplitter = 280;
    public int actionsSplitter = 280;
    public bool autoSave = false;

    private string FilePath {
      get {
        var root = Paths.AppDataDirectory();
        return Path.Combine(root, "settings.json");
      }
    }

    public override void Save() {
      Save(FilePath);
    }

    public void Load() {
      var settings = base.Load<ApplicationSettings>(FilePath);
      var fields = settings.GetType().GetFields();
      foreach (var field in fields) {
        GetType().GetField(field.Name)?.SetValue(this, field.GetValue(settings));
      }
    }
  }
}