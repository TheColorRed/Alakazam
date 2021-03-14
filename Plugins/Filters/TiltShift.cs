

using System.Linq;
using Alakazam.Editor;
using Alakazam.ImageMagick;
using ImageMagick;

namespace Alakazam.Plugin {
  [MenuItem("Filters/Artistic/Tilt Shift")]
  public class TiltShift : Filter {

    public override string Name => "Tilt Shift";

    [Range(0, 100)] public double Offset { get; set; } = 50;
    [Range(0, 100)] public double Height { get; set; } = 25;

    [Range(0, 50)] public double Blur { get; set; } = 10;
    [Range(0, 100)] public double Contrast { get; set; } = 10;
    [Range(0, 1000)] public double Saturation { get; set; } = 200;
    [Range(0, 100)] public double Sharpness { get; set; } = 2;

    public override void Apply(MagickImage image) {
      var height = image.Height;
      var viewPortHeight = Height / 100.0 * height;

      var topGradientHeight = (int)((Offset / 100 * height) - (viewPortHeight / 2));
      var bottomGradientHeight = (int)(((100 - Offset) / 100 * height) - (viewPortHeight / 2));

      var mask = Mask(image.Width, image.Height, topGradientHeight, bottomGradientHeight);

      image.BrightnessContrast(new Percentage(0), new Percentage(Contrast));
      image.Modulate(new Percentage(100), new Percentage(Saturation), new Percentage(300));
      image.Sharpen(Sharpness, Sharpness);

      image.SetWriteMask(mask);

      image.Blur(0, Blur);

    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Offset"));
      GUILayout.PropertyField(new SerializedProperty(this, "Height"));

      GUILayout.PropertyField(new SerializedProperty(this, "Blur"));
      GUILayout.PropertyField(new SerializedProperty(this, "Contrast"));
      GUILayout.PropertyField(new SerializedProperty(this, "Saturation"));
      GUILayout.PropertyField(new SerializedProperty(this, "Sharpness"));
    }

    private MagickImage Mask(int width, int height, int topHeight, int bottomHeight) {
      var image = new MagickImage(MagickColors.White, width, height);

      var topImage = new MagickImage(MagickColors.Black, width, topHeight);
      var topGradient = new MagickImage("gradient:black-white", image.Width, topHeight / 3);
      topImage.Composite(topGradient, Gravity.South);

      var bottomImage = new MagickImage(MagickColors.Black, width, bottomHeight);
      var bottomGradient = new MagickImage("gradient:white-black", image.Width, bottomHeight / 3);
      bottomImage.Composite(bottomGradient, Gravity.North);

      image.Composite(topImage, Gravity.North);
      image.Composite(bottomImage, Gravity.South);

      return image;
    }
  }
}