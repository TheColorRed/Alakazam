using System.Linq;
using Alakazam.Engine;
using ImageMagick;

namespace Alakazam.ImageMagick {
  public static partial class AutoMagick {
    public static MagickImage PerlinNoise(int width, int height) {
      using var noise1 = new MagickImage(MagickColors.Black, width / 160, height / 160);
      using var noise2 = new MagickImage(MagickColors.Black, width / 80, height / 80);
      using var noise3 = new MagickImage(MagickColors.Black, width / 40, height / 40);
      using var noise4 = new MagickImage(MagickColors.Black, width / 20, height / 20);
      using var noise5 = new MagickImage(MagickColors.Black, width / 10, height / 10);
      using var noise6 = new MagickImage(MagickColors.Black, width / 1, height / 1);

      noise1.AddNoise(NoiseType.Random);
      noise2.AddNoise(NoiseType.Random);
      noise3.AddNoise(NoiseType.Random);
      noise4.AddNoise(NoiseType.Random);
      noise5.AddNoise(NoiseType.Random);

      noise1.VirtualPixelMethod = VirtualPixelMethod.Tile;
      noise2.VirtualPixelMethod = VirtualPixelMethod.Tile;
      noise3.VirtualPixelMethod = VirtualPixelMethod.Tile;
      noise4.VirtualPixelMethod = VirtualPixelMethod.Tile;
      noise5.VirtualPixelMethod = VirtualPixelMethod.Tile;
      noise6.VirtualPixelMethod = VirtualPixelMethod.Tile;

      var geometry = new MagickGeometry(width, height) {
        FillArea = true
      };

      noise1.Evaluate(Channels.Default, EvaluateOperator.Multiply, 0.5);
      noise1.Evaluate(Channels.Default, EvaluateOperator.Add, 0.25);
      noise1.Resize(geometry);

      noise2.Evaluate(Channels.Default, EvaluateOperator.Multiply, 0.25);
      noise2.Evaluate(Channels.Default, EvaluateOperator.Add, 0.375);
      noise2.Resize(geometry);

      noise3.Evaluate(Channels.Default, EvaluateOperator.Multiply, 0.125);
      noise3.Evaluate(Channels.Default, EvaluateOperator.Add, 0.4375);
      noise3.Resize(geometry);

      noise4.Evaluate(Channels.Default, EvaluateOperator.Multiply, 0.0625);
      noise4.Evaluate(Channels.Default, EvaluateOperator.Add, 0.46875);
      noise4.Resize(geometry);

      noise5.Evaluate(Channels.Default, EvaluateOperator.Multiply, 0.03125);
      noise5.Evaluate(Channels.Default, EvaluateOperator.Add, 0.484375);
      noise5.Resize(geometry);

      noise6.Evaluate(Channels.Default, EvaluateOperator.Multiply, 0.015625);
      noise6.Evaluate(Channels.Default, EvaluateOperator.Add, 0.49921875);
      noise6.Resize(geometry);

      noise1.Composite(noise2, CompositeOperator.Plus);
      noise1.Composite(noise3, CompositeOperator.Plus);
      noise1.Composite(noise4, CompositeOperator.Plus);
      noise1.Composite(noise5, CompositeOperator.Plus);
      noise1.Composite(noise6, CompositeOperator.Plus);
      noise1.AutoLevel();

      return (MagickImage)noise1.Clone();
    }
  }
}