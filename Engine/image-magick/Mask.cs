using ImageMagick;

namespace Alakazam.ImageMagick {
  public static partial class AutoMagick {

    public static MagickImage ShadowMask(this MagickImage image, double radius = 0) {
      var clone = image.Clone();
      clone.ColorSpace = ColorSpace.Gray;
      clone.Threshold(new Percentage(45));
      if (radius > 0) clone.Blur(radius, radius / 2);
      return (MagickImage)clone;
    }

    public static MagickImage MidtoneMask(this MagickImage image, double radius = 0) {
      var shadows = ShadowMask(image);
      var highlights = HighlightMask(image);
      shadows.Composite(highlights, CompositeOperator.Difference);
      if (radius > 0) shadows.Blur(radius, radius / 2);
      return (MagickImage)shadows.Clone();
    }

    public static MagickImage HighlightMask(this MagickImage image, double radius = 0) {
      var clone = image.Clone();
      clone.ColorSpace = ColorSpace.Gray;
      clone.Threshold(new Percentage(66));
      clone.Negate();
      if (radius > 0) clone.Blur(radius, radius / 2);
      return (MagickImage)clone;
    }
  }
}