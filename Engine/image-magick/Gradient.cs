using ImageMagick;

namespace Alakazam.ImageMagick {
  public static partial class AutoMagick {

    public static MagickImage Gradient(MagickColor startColor, MagickColor endColor, int width, int height, double angle = 0) {
      var start = startColor.ToString();
      var end = endColor.ToString();

      var readSettings = new MagickReadSettings {
        Width = width,
        Height = height
      };

      readSettings.SetDefine("gradient:angle", angle.ToString());
      return new MagickImage($"gradient:{start}-{end}", readSettings);
    }

    public static MagickImage Gradient(this MagickImage image, MagickColor startColor, MagickColor endColor, double angle = 0) {
      return Gradient(startColor, endColor, image.Width, image.Height, angle);
    }
  }
}