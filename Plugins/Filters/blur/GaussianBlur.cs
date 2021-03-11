using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Filters {
  [MenuItem("Filters/Blur/Gaussian Blur")]
  public class GaussianBlur : Filter {

    public override string Name => "Gaussian Blur";

    [Range(0, 100), Step(0.05)] public double Radius { get; set; } = 0;

    public override void Apply(MagickImage image) {
      if (Radius > 0)
        image.Blur(0, Radius);
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Radius"));
    }
  }
}