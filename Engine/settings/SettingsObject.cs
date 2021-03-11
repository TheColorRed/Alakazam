using System.IO;
using Newtonsoft.Json;

namespace Alakazam.Engine.Settings {

  public abstract class SettingsObject {

    /// <summary>
    /// Setsup location of where to save the settings.
    /// </summary>
    public virtual void Save() { }

    protected void Save(string path) {
      Save(path, this);
    }

    protected void Save(string path, object data) {
      try {
        var serializedData = JsonConvert.SerializeObject(data, new JsonSerializerSettings {
          TypeNameHandling = TypeNameHandling.All
        });
        File.WriteAllText(path, serializedData);
      } catch (JsonException e) {
        Debug.Log(e);
      }
    }

    public virtual T Load<T>(string path) where T : new() {
      try {
        var text = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(text, new JsonSerializerSettings {
          TypeNameHandling = TypeNameHandling.All
        });
      } catch (JsonException e) {
        Debug.Log(e);
        return new T();
      }
    }

  }
}