using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Filters {
  [MenuItem("Filters/Blur/Adaptive Blur")]
  public class AdaptiveBlur : Filter {

    public override string Name => "Adaptive Blur";

    [Range(0, 100)] public double Radius { get; set; } = 0;

    public override void Apply(MagickImage image) {
      if (Radius > 0)
        image.AdaptiveBlur(Radius);
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Radius"));
    }
  }
}