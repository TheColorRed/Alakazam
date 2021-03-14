using System.IO;
using Alakazam.Editor;
using Alakazam.Engine;
using Alakazam.Plugin;
using ImageMagick;
using Paths = Alakazam.Engine.Paths;

namespace Alakazam.Filters {
  [MenuItem("Filters/Blur/Selective Blur")]
  public class SelectiveBlur : Filter {

    public override string Name => "Selective Blur";

    [Range(0, 100)] public double Radius { get; set; } = 0;
    [Range(0, 100)] public double Threshold { get; set; } = 0;

    public override void Apply(MagickImage image) {
      if (Radius == 0 && Threshold == 0) return;

      var mask = (MagickImage)image.Clone();
      mask.ColorSpace = ColorSpace.Gray;
      mask.ReduceNoise(10);
      mask.Threshold(new Percentage(Threshold));
      // mask.Sharpen();
      // mask.Blur(5, 5);
      mask.Negate();

      mask.Write(Path.Combine(Paths.ResourcesPath(Engine.Engine.project), "Mask.png"));

      image.SetWriteMask(mask);
      image.Blur(0, Radius);
      image.RemoveWriteMask();

      // Debug.Log(Path.Combine(Paths.ResourcesPath(Engine.Engine.project)));

      image.Write(Path.Combine(Paths.ResourcesPath(Engine.Engine.project), "custom.png"));

      // var clone = image.Clone();
      // clone.SelectiveBlur(0, Radius, new Percentage(Threshold));

      // clone.Write(Path.Combine(Paths.ResourcesPath(Engine.Engine.project), "selective.png"));
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Radius"));
      GUILayout.PropertyField(new SerializedProperty(this, "Threshold"));
    }
  }
}