using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Filters {
  [MenuItem("Filters/Blur/Selective Blur")]
  public class SelectiveBlur : Filter {

    public override string Name => "Selective Blur";

    [Range(0, 100)] public double Radius { get; set; } = 0;
    [Range(0, 100)] public double Threshold { get; set; } = 0;

    public override void Apply(MagickImage image) {
      if (Radius > 0 && Threshold > 0)
        image.SelectiveBlur(Radius, Radius / 10, new Percentage(Threshold));
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Radius"));
      GUILayout.PropertyField(new SerializedProperty(this, "Threshold"));
    }
  }
}