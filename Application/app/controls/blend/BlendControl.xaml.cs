using System.Windows.Controls;
using System;
using System.Linq;
using ImageMagick;
using Alakazam.Engine.Events;
using Alakazam.Engine;

namespace Alakazam.Controls {

  public struct BlendMode {
    public string name;
    public CompositeOperator blendType;

    public string Name {
      get { return name; }
    }

    public BlendMode(string name, CompositeOperator blendType) {
      this.name = name;
      this.blendType = blendType;
    }

    public BlendMode(string name) {
      this.name = name;
      this.blendType = CompositeOperator.No;
    }
  }

  public partial class BlendControl : UserControl {

    public Layer SelectedLayer {
      get {
        return MainWindow.project.selectedLayer;
      }
    }

    public BlendMode[] blendTypes = new BlendMode[] {
      new BlendMode("Normal", CompositeOperator.SrcOver),
      new BlendMode("Dissolve", CompositeOperator.Dissolve),
      new BlendMode("-"),
      new BlendMode("Darken", CompositeOperator.Darken),
      new BlendMode("Multiply", CompositeOperator.Multiply),
      new BlendMode("Color Burn", CompositeOperator.ColorBurn),
      new BlendMode("Linear Burn", CompositeOperator.LinearBurn),
      new BlendMode("-"),
      new BlendMode("Lighten", CompositeOperator.Lighten),
      new BlendMode("Screen", CompositeOperator.Screen),
      new BlendMode("Color Dodge", CompositeOperator.ColorDodge),
      new BlendMode("Linear Dodge (Add)", CompositeOperator.LinearDodge),
      new BlendMode("-"),
      new BlendMode("Overlay", CompositeOperator.Overlay),
      new BlendMode("Soft Light", CompositeOperator.SoftLight),
      new BlendMode("Hard Light", CompositeOperator.HardLight),
      new BlendMode("Vivid Light", CompositeOperator.VividLight),
      new BlendMode("Linear Light", CompositeOperator.LinearLight),
      new BlendMode("Pin Light", CompositeOperator.PinLight),
      new BlendMode("Hard Mix", CompositeOperator.HardMix),
      new BlendMode("-"),
      new BlendMode("Difference", CompositeOperator.Difference),
      new BlendMode("Negate", CompositeOperator.Negate),
      new BlendMode("Exclusion", CompositeOperator.Exclusion),
      new BlendMode("Subtract", CompositeOperator.MinusSrc),
      new BlendMode("Divide", CompositeOperator.DivideSrc),
      new BlendMode("-"),
      new BlendMode("Hue", CompositeOperator.Hue),
      new BlendMode("Saturate", CompositeOperator.Saturate),
      new BlendMode("Colorize", CompositeOperator.Colorize),
      new BlendMode("Luminosity", CompositeOperator.Luminize),
      new BlendMode("-"),
      new BlendMode("Reflect", CompositeOperator.Reflect),
      new BlendMode("Freeze", CompositeOperator.Freeze),
      new BlendMode("Stereo", CompositeOperator.Stereo),
      new BlendMode("Threshold", CompositeOperator.Threshold),
      new BlendMode("Displace", CompositeOperator.Displace),
      new BlendMode("Intensity", CompositeOperator.Intensity),
    };

    public CompositeOperator SelectedBlend {
      get { return SelectedLayer.blendType; }
    }

    public BlendControl() {
      DataContext = this;
      InitializeComponent();
      blendModes.ItemsSource = blendTypes;

      // EventBus.LayerSelectionInitialized += OnLayerSelectionInitialized;
      EventBus.LayerSelectionChanged += OnLayerSelectionChanged;
      EventBus.ProjectInitialized += OnProjectInitalized;
    }

    public void OnLayerSelectionInitialized(object sender, EventArgs e) {
      blendModes.SelectedIndex = GetIndex(SelectedLayer.blendType);
    }

    public void OnProjectInitalized(object sender, EventArgs e) {
      blendModes.SelectedIndex = GetIndex(SelectedLayer.blendType);
    }

    public void OnSelectionChanged(object sender, EventArgs e) {
      var comboBox = (ComboBox)sender;
      var selectedItem = (BlendMode)comboBox.SelectedItem;
      if (selectedItem.blendType != CompositeOperator.No) {
        SelectedLayer.blendType = selectedItem.blendType;
        EventBus.OnLayerBlendChanged(this);
      }
    }

    private int GetSelectedIndex() {
      return blendTypes.ToList()
        .FindIndex(itm => itm.blendType == SelectedLayer.blendType);
    }

    private int GetIndex(CompositeOperator composite) {
      return blendTypes.ToList().FindIndex(itm => itm.blendType == composite);
    }

    protected void OnLayerSelectionChanged(object sender, EventArgs e) {
      if (IsVisible) {
        blendModes.SelectedIndex = GetSelectedIndex();
      }
    }
  }
}