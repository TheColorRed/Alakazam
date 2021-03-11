using Alakazam.Editor;
using Alakazam.Engine;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Properties {

  [MenuItem("Layer/Gradient")]
  public class Gradient : Property {

    public override string Name => "Gradient";

    public MagickColor Start { get; set; } = MagickColors.Purple;
    public MagickColor End { get; set; } = MagickColors.Blue;

    [Range(0, 360)] public double Angle { get; set; } = 0;

    public override void Apply(MagickImage image) {
      var start = Start.ToString();
      var end = End.ToString();

      var readSettings = new MagickReadSettings {
        Width = image.Width,
        Height = image.Height
      };

      readSettings.SetDefine("gradient:angle", Angle.ToString());
      var gradient = new MagickImage($"gradient:{start}-{end}", readSettings);

      image.Composite(gradient, CompositeOperator.SrcOver);
    }

    public override void Draw() {

      GUILayout.PropertyField(new SerializedProperty(this, "Angle"));
      GUILayout.PropertyField(new SerializedProperty(this, "Start"));
      GUILayout.PropertyField(new SerializedProperty(this, "End"));

    }
  }
}