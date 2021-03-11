using Alakazam.Plugin;
using Newtonsoft.Json;

namespace Alakazam.Engine {
  public struct Command {
    public string name;
    public Action action;

    [JsonConstructor]
    public Command(string displayName, Action action) {
      name = displayName;
      this.action = action;
    }
  }
}