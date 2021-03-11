using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Colors {
  [MenuItem("Colors/White Balance")]
  public class WhiteBalance : Color {

    public override string Name => "White Balance";

    [Range(-100, 100)] public double Vibrance { get; set; } = 0;

    public override void Apply(MagickImage image) {
      image.WhiteBalance(new Percentage(Vibrance));
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Vibrance"));
    }
  }
}