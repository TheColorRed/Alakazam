using System.Linq;
using Alakazam.Engine;
using ImageMagick;

namespace Alakazam.ImageMagick {
  public static partial class AutoMagick {
    public static MagickImage Plasma(int width, int height) {
      using var plasma = new MagickImage("plasma:", width, height);

      return (MagickImage)plasma.Clone();
    }
  }
}