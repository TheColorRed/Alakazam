using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Filters {
  [MenuItem("Filters/Enhance/Sharpen")]
  public class Sharpen : Filter {
    public override string Name => "Sharpen";

    [Range(0, 50)] public double Radius { get; set; } = 0;
    [Range(0, 50), Label("Amount")] public double Sigma { get; set; } = 0;

    public override void Apply(MagickImage image) {
      if (Radius > 0 || Sigma > 0)
        image.UnsharpMask(Radius, Sigma);
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Radius"));
      GUILayout.PropertyField(new SerializedProperty(this, "Sigma"));
    }
  }
}