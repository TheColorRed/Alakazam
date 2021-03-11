using Alakazam.Editor;
using Alakazam.ImageMagick;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Colors {
  enum Range { Shadows, Midtones, Highlights }
  [MenuItem("Colors/Color Balance", 0)]
  public class ColorBalance : Color {

    public override string Name => "Color Balance";

    private Range Range { get; set; } = Range.Midtones;

    [Label("Cyan"), Range(-100, 100)] public double ShadowCyan { get; set; } = 0;
    [Label("Magenta"), Range(-100, 100)] public double ShadowMagenta { get; set; } = 0;
    [Label("Yellow"), Range(-100, 100)] public double ShadowYellow { get; set; } = 0;

    [Label("Cyan"), Range(-100, 100)] public double MidtoneCyan { get; set; } = 0;
    [Label("Magenta"), Range(-100, 100)] public double MidtoneMagenta { get; set; } = 0;
    [Label("Yellow"), Range(-100, 100)] public double MidtoneYellow { get; set; } = 0;

    [Label("Cyan"), Range(-100, 100)] public double HighlightCyan { get; set; } = 0;
    [Label("Magenta"), Range(-100, 100)] public double HighlightMagenta { get; set; } = 0;
    [Label("Yellow"), Range(-100, 100)] public double HighlightYellow { get; set; } = 0;

    public override void Apply(MagickImage image) {
      using var shadowMask = image.ShadowMask(25);
      using var midtoneMask = image.MidtoneMask(25);
      using var highlightMask = image.HighlightMask(25);

      image.SetWriteMask(shadowMask);
      image.Evaluate(Channels.Cyan, EvaluateOperator.Add, new Percentage(ShadowCyan));
      image.Evaluate(Channels.Yellow, EvaluateOperator.Add, new Percentage(ShadowYellow));
      image.Evaluate(Channels.Magenta, EvaluateOperator.Add, new Percentage(ShadowMagenta));

      image.SetWriteMask(midtoneMask);
      image.Evaluate(Channels.Cyan, EvaluateOperator.Add, new Percentage(MidtoneCyan));
      image.Evaluate(Channels.Magenta, EvaluateOperator.Add, new Percentage(MidtoneMagenta));
      image.Evaluate(Channels.Yellow, EvaluateOperator.Add, new Percentage(MidtoneYellow));

      image.SetWriteMask(highlightMask);
      image.Evaluate(Channels.Cyan, EvaluateOperator.Add, new Percentage(HighlightCyan));
      image.Evaluate(Channels.Magenta, EvaluateOperator.Add, new Percentage(HighlightMagenta));
      image.Evaluate(Channels.Yellow, EvaluateOperator.Add, new Percentage(HighlightYellow));

      image.RemoveWriteMask();
    }

    public override void Draw() {

      GUILayout.PropertyField(new SerializedProperty(this, "Range"));

      if (Range == Range.Shadows) {
        GUILayout.PropertyField(new SerializedProperty(this, "ShadowCyan"));
        GUILayout.PropertyField(new SerializedProperty(this, "ShadowMagenta"));
        GUILayout.PropertyField(new SerializedProperty(this, "ShadowYellow"));
      } else if (Range == Range.Midtones) {
        GUILayout.PropertyField(new SerializedProperty(this, "MidtoneCyan"));
        GUILayout.PropertyField(new SerializedProperty(this, "MidtoneMagenta"));
        GUILayout.PropertyField(new SerializedProperty(this, "MidtoneYellow"));
      } else if (Range == Range.Highlights) {
        GUILayout.PropertyField(new SerializedProperty(this, "HighlightCyan"));
        GUILayout.PropertyField(new SerializedProperty(this, "HighlightMagenta"));
        GUILayout.PropertyField(new SerializedProperty(this, "HighlightYellow"));
      }

    }
  }
}