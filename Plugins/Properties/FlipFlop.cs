using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Properties {

  public enum Direction { Horizontal, Vertical }

  [MenuItem("Layer/Flip Flop")]
  public class FlipFlop : Property {

    public override string Name => "Flip Flop";

    public Direction Axis { get; set; } = Direction.Horizontal;

    public override void Apply(MagickImage image) {
      if (Axis == Direction.Horizontal) {
        image.Flip();
      }
      if (Axis == Direction.Vertical) {
        image.Flop();
      }
    }

    public override void Draw() {

      GUILayout.PropertyField(new SerializedProperty(this, "Axis"));

    }
  }
}