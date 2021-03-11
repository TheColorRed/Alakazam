using System;
using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Colors {
  [MenuItem("Colors/Threshold")]
  public class Threshold : Color {

    public Channels Channel { get; set; } = Channels.Default;
    [Range(0, 100)] public double Amount { get; set; } = 50;

    public override string Name => "Thereshold";

    public override void Apply(MagickImage image) {
      image.Threshold(new Percentage(Amount), Channel);
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Channel"));
      GUILayout.PropertyField(new SerializedProperty(this, "Amount"));
    }
  }
}