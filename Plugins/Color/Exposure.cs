using System;
using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Colors {
  [MenuItem("Colors/Exposure", 4)]
  public class BlackExposure : Color {

    public override string Name => "Exposure";

    [Range(0, 100), Step(0.5)] public double BlackLevel { get; set; } = 0;
    [Range(0, 100), Step(0.5)] public double Exposure { get; set; } = 0;


    public override void Apply(MagickImage image) {
      if (BlackLevel > 0)
        image.Level(new Percentage(BlackLevel), new Percentage(100));
      if (Exposure > 0)
        image.SigmoidalContrast(true, Exposure, new Percentage(0));
    }

    public override void Draw() {

      GUILayout.PropertyField(new SerializedProperty(this, "BlackLevel"));
      GUILayout.PropertyField(new SerializedProperty(this, "Exposure"));

    }
  }
}