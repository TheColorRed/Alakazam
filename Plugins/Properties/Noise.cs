using System.Linq;
using System.Windows.Controls;
using Alakazam.Editor;
using Alakazam.Engine;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Properties {

  [MenuItem("Layer/Clouds")]
  public class PerlinNoise : Property {

    public override string Name => "Noise";

    public override void Apply(MagickImage image) {

      using var noise = new MagickImage(MagickColors.Black, image.Width, image.Height);

      noise.AddNoise(NoiseType.Random);
      noise.VirtualPixelMethod = VirtualPixelMethod.Tile;
      noise.Blur(0, 5);
      noise.AutoLevel();
      var green = noise.Separate(Channels.Green).First();

      image.Composite(green, CompositeOperator.SrcOver);

    }

    public override void Draw() {

      var btn = GUILayout.Button("Regenerate");

      btn.Click += (sender, evt) => {
        Debug.Log("Clicked!");
      };

    }
  }
}