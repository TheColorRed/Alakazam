using ImageMagick;
using Alakazam.Editor;
using Alakazam.Plugin;

namespace Alakazam.Colors {
  [MenuItem(@"Colors/Brightness\/Contrast", 6)]
  public class BrightnessContrast : Color {
    public override string Name => "Brightness/Contrast";

    [Range(-100, 100)] public double Brightness { get; set; } = 0;
    [Range(-100, 100)] public double Contrast { get; set; } = 0;

    public override void Apply(MagickImage image) {
      image.BrightnessContrast(
        new Percentage(Brightness),
        new Percentage(Contrast)
      );
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Brightness"));
      GUILayout.PropertyField(new SerializedProperty(this, "Contrast"));
    }
  }
}