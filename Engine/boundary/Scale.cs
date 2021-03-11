using Newtonsoft.Json;

namespace Alakazam.Engine {
  public class Scale : Vector {
    [JsonConstructor]
    public Scale(int x, int y) : base(x, y) { }
  }
}