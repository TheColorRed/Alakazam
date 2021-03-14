using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Engine {
  public class Transform : Action {
    public override string Name => "Transform";

    [Tooltip("Whether or not this layer should be anchored.")]
    public bool IsAnchored { get; set; } = false;

    [Tooltip("This sets where the layer should be anchored in the final image.")]
    public Gravity Anchor { get; set; } = Gravity.Northwest;

    [Tooltip("This sets the exact pixel position of the layer in the final image.")]
    public Position Position { get; set; } = new Position(0, 0);

    [Tooltip("This sets the scale of the image in the final image.")]
    public Scale Scale { get; set; } = new Scale(100, 100);

    [Tooltip("This sets the rotation of the layer in degrees in the final image.")]
    public double Rotation { get; set; } = 0;

  }
}