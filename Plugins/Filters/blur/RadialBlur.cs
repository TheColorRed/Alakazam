using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Filters {
  [MenuItem("Filters/Blur/Radial Blur")]
  public class RadialBlur : Filter {

    public override string Name => "Radial Blur";

    [Range(0, 360)] public double Angle { get; set; } = 0;

    public override void Apply(MagickImage image) {
      if (Angle > 0)
        image.RotationalBlur(Angle);
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Angle"));
    }
  }
}