using Alakazam.Engine;
using ImageMagick;

namespace Alakazam.ImageMagick {
  public static partial class AutoMagick {

    public static MagickImage GetCheckerBoard(this MagickImage image, int squareSize) {
      return GetCheckerBoard(image.Width, image.Height, squareSize);
    }

    public static MagickImage GetCheckerBoard(this Size size, int squareSize) {
      return GetCheckerBoard(size.width, size.height, squareSize);
    }

    private static MagickImage GetCheckerBoard(int width, int height, int squareSize) {
      if (width == 0 || height == 0 || squareSize == 0) return new MagickImage();
      var image = new MagickImage(MagickColors.Transparent, width, height);
      var light = MagickColor.FromRgb(150, 150, 150);
      var dark = MagickColor.FromRgb(100, 100, 100);

      using (var tile = new MagickImage(MagickColors.Transparent, squareSize * 2, squareSize * 2)) {
        var drawable = new Drawables()
          // Fill the background with a light color
          .FillColor(light)
          .Rectangle(0, 0, squareSize * 2, squareSize * 2)
          // Create the top left square
          .FillColor(dark)
          .Rectangle(0, 0, squareSize, squareSize)
          // Create the bottom right square
          .FillColor(dark)
          .Rectangle(squareSize, squareSize, squareSize * 2, squareSize * 2);

        tile.Draw(drawable);

        // Tile the tile on the full image
        image.Tile(tile, CompositeOperator.SrcOver);
      }
      return image;
    }
  }
}