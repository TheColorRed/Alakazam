using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Filters {
  [MenuItem("Filters/Enhance/Noise Reduction")]
  public class NoiseReduction : Filter {
    public override string Name => "Noise Reduction";

    [Range(0, 32)] public int Strength { get; set; } = 0;

    public override void Apply(MagickImage image) {
      image.ReduceNoise(Strength);
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Strength"));
    }
  }
}