
using System;
using System.Windows.Controls;

namespace Alakazam.Controls {
  public partial class LayerControl : UserControl {

    public LayerControl() {
      DataContext = this;
      InitializeComponent();
    }
    public void OnToggleLayerVisibility(object source, EventArgs e) {
      // var btn = (Button)source;
      // btn.Closest<L
    }
  }
}