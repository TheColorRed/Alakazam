using System;
using System.Windows.Controls;
using Alakazam.Engine;
using Alakazam.Engine.Events;

namespace Alakazam.Controls {
  public partial class LayerActionButtonsControl : UserControl {

    private readonly Project project = MainWindow.project;

    public LayerActionButtonsControl() {
      DataContext = this;
      InitializeComponent();
    }

    public void OnNewLayer(object sender, EventArgs evt) {
      project.AddEmptyLayer();
    }

    public void OnMoveLayerUp(object sender, EventArgs evt) {
      var currentIndex = project.layers.IndexOf(project.selectedLayer);
      if (currentIndex > 0) {
        project.layers.Move(currentIndex, --currentIndex);
        EventBus.OnLayerPositionChanged(this);
      }
    }

    public void OnMoveLayerDown(object sender, EventArgs evt) {
      var currentIndex = project.layers.IndexOf(project.selectedLayer);
      if (currentIndex != project.layers.Count - 1) {
        project.layers.Move(currentIndex, ++currentIndex);
        EventBus.OnLayerPositionChanged(this);
      }
    }

    public void OnDeleteLayer(object sender, EventArgs evt) {
      project.selectedLayer.Delete();
    }

  }
}