using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Colors {
  [MenuItem("Colors/Invert")]
  public class Invert : Color {

    public override string Name => "Invert";

    public override void Apply(MagickImage image) {
      image.Negate();
    }
  }
}