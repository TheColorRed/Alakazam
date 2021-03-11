using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Engine {
  public class Transform : Action {
    public override string Name => "Transform";

    public bool IsAnchored { get; set; } = false;
    public Gravity Anchor { get; set; } = Gravity.Northwest;
    public Position Position { get; set; } = new Position(0, 0);
    public Scale Scale { get; set; } = new Scale(100, 100);
    public double Rotation { get; set; } = 0;

  }
}