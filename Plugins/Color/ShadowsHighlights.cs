using Alakazam.Editor;
using Alakazam.ImageMagick;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Colors {

  [MenuItem(@"Colors/Shadows\/Midtones\/Highlights", 5)]
  public class ShadowsHighlights : Color {

    public override string Name => "Shadows/Midtones/Highlights";

    [Range(-100, 100)] public double Shadows { get; set; } = 0;
    [Range(-100, 100)] public double Midtones { get; set; } = 0;
    [Range(-100, 100)] public double Highlights { get; set; } = 0;

    public override void Apply(MagickImage image) {
      // Adjust the shadows
      if (Shadows != 0) {
        using var shadows = image.ShadowMask(25);
        image.SetWriteMask(shadows);
        image.Evaluate(Channels.Default, EvaluateOperator.Add, new Percentage(Shadows));
      }

      // Adjust the midtones
      if (Midtones != 0) {
        using var midtones = image.MidtoneMask(25);
        image.SetWriteMask(midtones);
        image.Evaluate(Channels.Default, EvaluateOperator.Add, new Percentage(Midtones));
      }

      // Adjust the highlights
      if (Highlights != 0) {
        using var highlights = image.HighlightMask(25);
        image.SetWriteMask(highlights);
        image.Evaluate(Channels.Default, EvaluateOperator.Add, new Percentage(Highlights));
      }

      // Remove the mask
      image.RemoveWriteMask();
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Shadows"));
      GUILayout.PropertyField(new SerializedProperty(this, "Midtones"));
      GUILayout.PropertyField(new SerializedProperty(this, "Highlights"));
    }

  }
}