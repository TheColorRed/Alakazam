using Newtonsoft.Json;

namespace Alakazam.Engine {
  public struct Density {
    public double x;
    public double y;

    [JsonConstructor]
    public Density(int x = 120, int y = 120) {
      this.x = x;
      this.y = y;
    }
  }
}