using Newtonsoft.Json;

namespace Alakazam.Engine {
  public struct Size {
    public int width;
    public int height;

    [JsonConstructor]
    public Size(int width, int height) {
      this.width = width;
      this.height = height;
    }
  }
}