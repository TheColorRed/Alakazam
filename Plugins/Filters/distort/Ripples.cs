using System.Linq;
using Alakazam.Editor;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Filters {
  [MenuItem("Filters/Distort/Ripples")]
  public class Ripples : Filter {
    public override string Name => "Ripples";

    [Range(0, 50)] public double Power { get; set; } = 0;

    public override void Apply(MagickImage image) {
      var w = 5;
      var ww = image.Width;
      var hh = image.Height;
      var hmr = 0;
      var gradient = new MagickImage("gradient:", image.Width, image.Height);
      gradient.ColorSpace = ColorSpace.RGB;
      gradient.Negate();
      // gradient.Fx($"-pow(({hmr}-j)/{hh},{Power})*0.5*sin(2*pi*u*{hh}/{w})+0.5", Channels.Green);
      gradient.Fx($"-pow(({hh}-j)/{hh},{Power})*0.5*sin(2*pi*u)+0.5", Channels.Green);
      var green = gradient.Separate(Channels.Green).First();

      image.Composite(green, CompositeOperator.Displace);
    }

    public override void Draw() {
      GUILayout.PropertyField(new SerializedProperty(this, "Power"));
    }
  }
}