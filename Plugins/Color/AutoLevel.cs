using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Colors {
  [MenuItem("Colors/Auto/Auto Level")]
  public class AutoLevel : Color {

    public override string Name => "Auto Level";

    public AutoLevel() {
      Collapsed = true;
    }

    public override void Apply(MagickImage image) {
      image.AutoLevel();
    }
  }
}