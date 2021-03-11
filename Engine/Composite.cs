using Alakazam.Engine;
using Alakazam.ImageMagick;
using ImageMagick;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Alakazam.Commands {
  public static class Composite {

    public static BitmapImage Compose(this Project project) {
      var width = project.size.width;
      var height = project.size.height;
      var layers = project.layers.ToList();

      using var image = new MagickImage(MagickColors.Transparent, width, height);
      image.Settings.Format = MagickFormat.Bmp;

      for (int i = project.layers.Count - 1; i >= 0; i--) {
        var layer = project.layers[i];

        // If the layer is hidden, do not draw it.
        if (layer.visible == false) continue;

        // If this is the base layer don't apply a composite.
        var blend = layer.blendType;
        var isBottom = IsBottomLayer(layer, layers);
        if (isBottom) blend = CompositeOperator.SrcOver;

        // Get the position of where the image should be drawn.
        var x = layer.transform.Position.x;
        var y = layer.transform.Position.y;
        var isAnchored = layer.transform.IsAnchored;
        var anchor = layer.transform.Anchor;

        // Set a mask on the layer if there is one.
        if (layer.LayerMask?.image is MagickImage mask) {
          image.SetWriteMask(mask);
        }

        if (isAnchored) {
          image.Composite(layer.FilteredImage, anchor, blend, Channels.RGB);
        } else {
          image.Composite(layer.FilteredImage, x, y, blend, Channels.RGB);
        }

        // Remove any masks that have been added to the layer.
        image.RemoveWriteMask();
      }

      return image.ToBitmapImage();
    }

    /// <summary>
    /// Checks to see if the current layer is the last visible layer.
    /// </summary>
    /// <param name="layer">The layer to check.</param>
    /// <param name="layers">A list of all layers.</param>
    /// <returns>True if this is the last visible layer. False otherwise.</returns>
    private static bool IsBottomLayer(Layer layer, List<Layer> layers) {
      Layer last = null;
      for (int i = 0; i < layers.Count; i++) {
        var iLayer = layers[i];
        if (iLayer.visible == true)
          last = iLayer;
      }
      return last == layer;
    }
  }
}