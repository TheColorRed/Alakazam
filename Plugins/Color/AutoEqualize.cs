using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Colors {
  [MenuItem("Colors/Auto/Auto Equalize")]
  public class AutoEqualize : Color {

    public override string Name => "Auto Equalize";

    public AutoEqualize() {
      Collapsed = true;
    }

    public override void Apply(MagickImage image) {
      image.Equalize();
    }
  }
}