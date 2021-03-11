using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Colors {
  [MenuItem(@"Colors/Hue\/Saturation", 2)]
  public class HueSaturation : Color {

    public override string Name => "Hue/Saturation";

    [Range(0, 300)] public double Hue { get; set; } = 300;
    [Range(0, 1000)] public double Saturation { get; set; } = 100;
    [Range(-100, 100)] public double Lightness { get; set; } = 0;


    public override void Apply(MagickImage image) {

      double lightness = NewRange(Lightness, -100, 100, 0, 200);

      image.Modulate(new Percentage(lightness), new Percentage(Saturation), new Percentage(Hue));

    }

    public override void Draw() {

      GUILayout.PropertyField(new SerializedProperty(this, "Hue"));
      GUILayout.PropertyField(new SerializedProperty(this, "Saturation"));
      GUILayout.PropertyField(new SerializedProperty(this, "Lightness"));

    }

    double NewRange(double value, double oldMin, double oldMax, double newMin, double newMax) {
      var OldRange = oldMax - oldMin;
      var NewRange = newMax - newMin;
      return ((value - oldMin) * NewRange / OldRange) + newMin;
    }
  }
}