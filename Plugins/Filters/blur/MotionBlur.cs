using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Filters {
  [MenuItem("Filters/Blur/Motion Blur")]
  public class MotionBlur : Filter {

    public override string Name => "Motion Blur";

    [Range(0, 1000)] public double Radius { get; set; } = 0;
    [Range(0, 360)] public double Angle { get; set; } = 0;

    public override void Apply(MagickImage image) {
      if (Radius > 0)
        image.MotionBlur(Radius, Radius / 10, Angle);
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Radius"));
      GUILayout.PropertyField(new SerializedProperty(this, "Angle"));
    }
  }
}