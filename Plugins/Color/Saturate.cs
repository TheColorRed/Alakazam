using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Colors {
  [MenuItem("Colors/Saturate", 3)]
  public class Saturate : Color {

    [Range(0, 10), Step(0.1)] public double Saturation { get; set; } = 1;

    public override string Name => "Saturation";

    public override void Apply(MagickImage image) {
      image.Modulate(new Percentage(100), new Percentage(Saturation * 100), new Percentage(100));
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Saturation"));
    }
  }
}