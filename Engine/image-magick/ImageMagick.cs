using System.IO;
using System.Windows.Media.Imaging;
using Alakazam.Engine;
using ImageMagick;

namespace Alakazam.ImageMagick {
  public static partial class AutoMagick {
    public static BitmapImage ToBitmapImage(this MagickImage image) {
      image.Settings.BackgroundColor = MagickColors.Transparent;
      var imageData = image.ToByteArray(MagickFormat.Bmp);
      using var ms = new MemoryStream(imageData);
      var bitmap = new BitmapImage();
      bitmap.BeginInit();
      bitmap.CacheOption = BitmapCacheOption.OnLoad;
      bitmap.StreamSource = ms;
      bitmap.EndInit();
      bitmap.Freeze();
      return bitmap;
    }

    public static BitmapImage GetImagePreview(this MagickImage image, Layer layer, Size size) {
      using var previewClone = image.Clone();
      using var checkerBoard = GetCheckerBoard(layer.Project.size, 45);

      var x = layer.transform.Position.x;
      var y = layer.transform.Position.y;
      var isAnchored = layer.transform.IsAnchored;
      var anchor = layer.transform.Anchor;

      if (isAnchored)
        checkerBoard.Composite(previewClone, anchor, CompositeOperator.SrcOver);
      else
        checkerBoard.Composite(previewClone, x, y, CompositeOperator.SrcOver);

      checkerBoard.Resize(size.width, size.height);
      var bitmap = checkerBoard.ToBitmapImage();
      return bitmap;
    }
  }
}