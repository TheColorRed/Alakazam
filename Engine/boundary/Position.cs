using Newtonsoft.Json;

namespace Alakazam.Engine {
  public class Position : Vector {
    [JsonConstructor]
    public Position(int x, int y) : base(x, y) { }
  }
}