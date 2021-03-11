using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Colors {
  [MenuItem("Colors/Temperature", 1)]
  public class Temperature : Color {

    [Range(-100, 100)] public double Warm { get; set; } = 0;
    [Range(-100, 100)] public double Cool { get; set; } = 0;

    public override string Name => "Temperature";

    public override void Apply(MagickImage image) {
      if (Warm != 0)
        image.Evaluate(Channels.Red, EvaluateOperator.Add, new Percentage(Warm));
      if (Cool != 0)
        image.Evaluate(Channels.Blue, EvaluateOperator.Add, new Percentage(Cool));
    }

    public override void Draw() {

      GUILayout.PropertyField(new SerializedProperty(this, "Warm"));
      GUILayout.PropertyField(new SerializedProperty(this, "Cool"));

    }
  }
}